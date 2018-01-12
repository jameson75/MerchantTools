using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Data;
using System.Globalization;
using System.Collections;
using CipherPark.Ebay.Api.Shopping;

namespace CipherPark.TriggerOrange.Core.ApplicationServices
{
    public abstract class TableWriter
    {
        private List<ColumnHeader> _headers = new List<ColumnHeader>();

        /// <summary>
        /// The datasource which will be serialized to a CSV formatted output.
        /// </summary>
        public object DataSource { get; set; }

        /// <summary>
        /// Determines whether CSV column headers should be written to output.
        /// </summary>
        public bool HeadersVisible { get; set; }

        /// <summary>
        /// Gets the encoding of the stream.
        /// </summary>
        public List<ColumnHeader> Headers { get { return _headers; } }

        /// <summary>
        /// Constructs headers from the specified data source. If the datasource is a DataTable, it's column names are used.
        /// If the datasource is a generic-type list, the property names of the list's generic type are used.
        /// If the datasource is a non-generic-type list, the property names of the list's first element are used.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// An InvalidOperationException is thrown if the datasource is an empty, non-generic list and exception is thrown.
        /// </exception>
        protected void GenerateHeaders()
        {
            _headers.Clear();
            if (DataSource != null)
            {
                if (DataSource is DataTable)
                {
                    DataTable table = (DataTable)DataSource;
                    foreach (DataColumn column in table.Columns)
                        _headers.Add(new ColumnHeader() { Caption = column.Caption, FieldName = column.ColumnName });
                }
                else if (DataSource is IList)
                {
                    Type dataSourceType = DataSource.GetType();
                    Type elementType = null;
                    if (dataSourceType.IsGenericType)
                        elementType = dataSourceType.GetGenericArguments()[0];
                    else if (((IList)DataSource).Count > 0)
                        elementType = ((IList)DataSource)[0].GetType();
                    else
                        throw new InvalidOperationException("Cannot generate headers from an empty, non-generic list.");
                    System.Reflection.PropertyInfo[] piArray = elementType.GetProperties();
                    for (int i = 0; i < piArray.Length; i++)
                        if (piArray[i].IsPropertySerializable())
                            _headers.Add(new ColumnHeader() { Caption = piArray[i].Name, FieldName = piArray[i].Name });
                }
            }
        }

        public static DataTable FlattenToDataTable<T>(IEnumerable<T> dataSource)
        {            
            Action<Type, Stack<PropertyInfo>, List<DataColumn>> ExtractDataColumns = null;
            ExtractDataColumns = (type, lineage, results) =>
            {
                if (lineage.ToArray().Skip(1).Any(x => x.PropertyType == type))
                    throw new InvalidOperationException("Flattening objects with circular references not supported.");

                foreach(PropertyInfo pi in type.GetProperties())
                {              
                    if( pi.IsPropertySerializable())
                    {
                        lineage.Push(pi);
                        if (pi.PropertyType.IsScalar())
                        {
                            //NOTE: We choose "." as the separator because we know that's an illegal 
                            //character for a property name.
                            string columnName = string.Join(".", lineage.Select(x=>x.Name).ToArray().Reverse());
                            results.Add(new DataColumn(columnName, pi.PropertyType));
                        }
                        else
                            ExtractDataColumns(pi.PropertyType, lineage, results);
                        lineage.Pop();
                    }
                }                               
            };

            Func<object, PropertyInfo[], string, object> GetRowValue = null;
            GetRowValue = (data, dataProperties, columnPath) =>
            {
                try
                {
                    string propertyName = columnPath.Contains(".") ? columnPath.Substring(0, columnPath.IndexOf(".")) : columnPath;
                    var pi = dataProperties.First(x => x.Name == propertyName);
                    if (!columnPath.Contains("."))
                        return pi.GetValue(data);
                    else
                    {
                        object subData = pi.GetValue(data);
                        string subPath = columnPath.Substring(columnPath.IndexOf(".") + 1);
                        PropertyInfo[] subDataProperties = pi.PropertyType.GetProperties(); //ie: subData.GetType().GetProperties()
                        return subData == null ? null : GetRowValue(subData, subDataProperties, subPath);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error", ex);
                }
            };

            DataTable table = new DataTable();
            Type typeT = typeof(T);
            PropertyInfo[] typeTProprties = typeT.GetProperties();
            List<DataColumn> columns = new List<DataColumn>();
            Stack<PropertyInfo> lineage_ = new Stack<PropertyInfo>();
            ExtractDataColumns(typeT, lineage_, columns);
            table.Columns.AddRange(columns.ToArray());
            foreach(T data in dataSource)
            {
                DataRow row = table.NewRow();
                row.BeginEdit();
                foreach (DataColumn column in table.Columns)
                    row[column] = GetRowValue(data, typeTProprties, column.ColumnName) ?? DBNull.Value;
                row.EndEdit();
                table.Rows.Add(row);    
            }          

            table.AcceptChanges();

            return table;
        }       
    }

    public class ColumnHeader
    {
        public string Caption { get; set; }
        public string ColumnFormat { get; set; }
        public string FieldName { get; set; }
        public Func<string, string> Formatter { get; set; }
        public Type FieldType { get; set; }
    }

    public static class PropertyInfoExtension
    {
        public static bool IsPropertySerializable(this System.Reflection.PropertyInfo pi)
        {
            return (pi.CanRead &&
                   (pi.PropertyType == typeof(string) || 
                   !typeof(IEnumerable).IsAssignableFrom(pi.PropertyType)));                   
        }       
    }

    public static class TypeExtension
    {
        public static bool IsScalar(this Type type)
        {
            return type.IsPrimitive ||
                   type == typeof(string) ||
                   type == typeof(DateTime) ||
                   (type.IsNullable() && type.NullableUnderlyingType().IsScalar());
        }

        public static bool IsNullable(this Type type)
        {
            return type.NullableUnderlyingType() != null;
        }

        public static Type NullableUnderlyingType(this Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }
    }
}

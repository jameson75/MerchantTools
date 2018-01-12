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

////////////////////////////////////////////////////////////////////
// Developer: Eugene J. Adams
// Utility class that outputs a stream in CSV format.
////////////////////////////////////////////////////////////////////

namespace CipherPark.TriggerOrange.Core.ApplicationServices
{
    /// <summary>
    /// Serializes data to a stream in CSV format.
    /// </summary>
    public class CSVWriter : TableWriter, IDisposable
    {
        private StreamWriter _streamWriter = null;
        private int _currentColumn = 0;
         
        
        /// <summary>
        /// The spearator character/string for each field in the the CSV output. The default is the current culture's list separator. (ie: ",").
        /// </summary>
        public string Separator { get; set; }  


        /// <summary>
        /// Gets the encoding of the stream.
        /// </summary>
        public Encoding Encoding { get { return _streamWriter.Encoding; } }


        /// <summary>
        /// Creates a CSVWriter associated with an output stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public CSVWriter(Stream stream, Encoding encoding = null)
        {
            _streamWriter = (encoding == null) ? new StreamWriter(stream) : new StreamWriter(stream, encoding);
            HeadersVisible = true;
            Separator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        }

        /// <summary>
        /// Creates a CSVWriter associated with an output file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        /// <param name="fileMode"></param>
        public CSVWriter(string fileName, Encoding encoding = null, FileMode fileMode = FileMode.Create)
            : this(new FileStream(fileName, fileMode), encoding)
        { }

        /// <summary>
        /// Perisits the datasource to the file or stream associated with this class.
        /// </summary>
        /// <param name="generateHeaders">If true, headers are automatically generated from the datasource, flagged as visible and written to output.</param>
        public void Write(bool generateHeaders = false)
        {
            if (generateHeaders)
            {
                GenerateHeaders();
                HeadersVisible = true;
            }

            if (DataSource != null)
            {
                if (DataSource is DataTable)
                {
                    if (HeadersVisible)
                        WriteHeaders();
                    WriteTable((DataTable)DataSource);
                }
                else if (DataSource is IList)
                {
                    if (HeadersVisible)
                        WriteHeaders();
                    WriteList((IList)DataSource);
                }
                else
                    throw new InvalidOperationException("DataSource was not of type DataTable nor IList.");
            }

            Flush();
        }

        /// <summary>
        /// Persists the CSV headers to output.
        /// </summary>
        private void WriteHeaders()
        {
            if (Headers.Count > 0)
            {               
                BeginLine();
                foreach (ColumnHeader header in Headers)
                    WriteData(header.Caption);
                EndLine();
            }
        }

        /// <summary>
        /// Persists each object in a list as a CSV row.
        /// </summary>
        /// <param name="list"></param>
        private void WriteList(IList list)
        {
            foreach (object o in list)
            {
                BeginLine();               
                WriteObject(o);
                EndLine();
            }
        }

        /// <summary>
        /// Persists the WriteObject 
        /// </summary>
        /// <param name="o"></param>
        private void WriteObject(object o)
        {
            System.Reflection.PropertyInfo[] piArray = o.GetType().GetProperties();
            if (Headers.Count > 0)
            {
                for (int i = 0; i < Headers.Count; i++)
                {
                    System.Reflection.PropertyInfo prop = piArray.SingleOrDefault(p => p.Name == Headers[i].FieldName);

                    if (prop == null)
                        throw new InvalidOperationException(string.Format("Property '{0}' not found for header '{1}'", Headers[i].FieldName, Headers[i].Caption));

                    if (prop.IsPropertySerializable())
                    {
                        object val = prop.GetValue(o);
                        if (val != null)
                        {
                            string format = null;

                            if (Headers[i].Formatter != null)
                                format = Headers[i].Formatter(val.ToString());

                            else if (Headers[i].ColumnFormat != null)
                                format = Headers[i].ColumnFormat;

                            if (format != null && Headers[i].FieldType == null)
                                throw new InvalidOperationException(string.Format("Column {0} has a specified format but no associated field type.", Headers[i].FieldName));

                            WriteData(val.ToString(), format, Headers[i].FieldType);
                        }
                        else
                            WriteData(string.Empty);
                    }
                }
            }
            else
            {
                for (int i = 0; i < piArray.Length; i++)
                {
                    if (piArray[i].IsPropertySerializable())
                    {
                        object val = piArray[i].GetValue(o);
                        if (val != null)
                            WriteData(val.ToString());
                        else
                            WriteData(string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Causes the underlying buffer of the internal writer to be written to the output stream and clears the buffer.
        /// </summary>        
        public void Flush()
        {
            _streamWriter.Flush();
        }

        private void WriteTable(DataTable dataTable)
        {            
            foreach (DataRow row in dataTable.Rows)
            {
                BeginLine();
                WriteDataRow(row);
                EndLine();
            }
        }

        private void WriteDataRow(DataRow row)
        {
            if (Headers.Count > 0)
            {
                for (int i = 0; i < Headers.Count; i++)
                {
                    int columnIndex = row.Table.Columns.IndexOf(Headers[i].FieldName);
                    if (columnIndex == -1)
                        throw new InvalidOperationException(string.Format("Column '{0}' not found for header '{1}'", Headers[i].FieldName, Headers[i].Caption));
                    object val = row.ItemArray[columnIndex];
                    if (val != null)
                    {
                        string format = null;

                        if (Headers[i].Formatter != null)
                            format = Headers[i].Formatter(val.ToString());

                        else if (Headers[i].ColumnFormat != null)
                            format = Headers[i].ColumnFormat;

                        if(format != null && Headers[i].FieldType == null)
                            throw new InvalidOperationException(string.Format("Column {0} has a specified format but no associated field type.", Headers[i].FieldName));

                        WriteData(val.ToString(), format, Headers[i].FieldType);                        
                    }
                    else
                        WriteData(string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    object val = row.ItemArray[i];
                    if (val != null)
                        WriteData(val.ToString());
                    else
                        WriteData(String.Empty);
                }
            }
        }

        private void WriteData(string data, string format = null, Type conversionType = null, bool writeRaw = false)
        {
            string output = data;           

            if (!writeRaw)
            {
                if( output.Contains("\"") ||
                    output.Contains(Separator) ||
                    output.Contains("\n") ||
                    output.Contains("\r") ||
                    output.StartsWith(" ") ||
                    output.EndsWith(" "))
                output = string.Format("\"{0}\"", data.Replace("\"", "\"\""));
            }            

            if (_currentColumn != 0)
                _streamWriter.Write(Separator);

            if (format != null)            
                _streamWriter.Write(string.Format(string.Format("{{0:{0}}}", format), Convert.ChangeType(output, conversionType)));         
            else
                _streamWriter.Write(output);

            _currentColumn++;
        }

        private void BeginLine()
        {
            _currentColumn = 0;
        }

        private void EndLine()
        {
            _streamWriter.Write(Environment.NewLine);
        }
      
        public void Close()
        {
            _streamWriter.Close();
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
        }
    }
}

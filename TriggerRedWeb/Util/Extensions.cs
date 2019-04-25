using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace CipherPark.TriggerRed.Web.Models
{
    public static class LinqExtension
    {
        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            return source.GroupBy(keySelector).Select(x => x.First());
        }

        public static string SelectedOrFirstValue(this IEnumerable<SelectListItem> list)
        {
            var selected = list.FirstOrDefault(i => i.Selected == true);
            if (selected != null)
                return selected.Value;
            else
            {
                if (list.Any())
                    return list.First().Value;
                else
                    return null;
            }
        }
    }

    public static class TypeExtension
    {
        public static bool IsNullableType(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

    public static class DateTimeExtension
    {
        private static readonly DateTime _UnixEpoc = new DateTime(1970, 1, 1);

        public static long ToUnixMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - _UnixEpoc).TotalMilliseconds;
        }        
    }

    public static class AsHtmlSafeExtension
    {
        /// <summary>       
        /// This method peforms a deep search for string-type properties and/or elements and html encodes each string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <remarks>
        /// Ignores structs.
        /// Ignores non-typed collections.
        /// Ignores nullable types.
        /// </remarks>
        public static List<T> AsHtmlSafe<T>(this List<T> list) where T : class
        {
            return (List<T>)((IEnumerable<T>)list).AsHtmlSafe();
        }

        /// <summary>       
        /// This method peforms a deep search for string-type properties and/or elements and html encodes each string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <remarks>
        /// Ignores structs.
        /// Ignores non-typed collections.
        /// Ignores nullable types.
        /// </remarks>
        public static IEnumerable<T> AsHtmlSafe<T>(this IEnumerable<T> enumerable) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var ignorePropertyLookup = CreateIgnorePropertyLookup(properties);
            IList<string> list = enumerable as IList<string>;
            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    var item = list[i];
                    if (item != null)
                    {
                        string token = item.ToString();
                        if (!string.IsNullOrWhiteSpace(token))
                            list[i] = HttpUtility.HtmlEncode(token);
                    }
                }
            }
            else if (!type.IsNullableType() && type != typeof(string))
            {
                foreach (T item in enumerable)
                    _MakeHtmlSafe<T>(properties, ignorePropertyLookup, item);
            }
            return enumerable;
        }

     
        private static T _AsHtmlSafe<T>(T model) where T : class
        {
            var properties = model.GetType().GetProperties();
            var ignorePropertyLookup = CreateIgnorePropertyLookup(properties);
            _MakeHtmlSafe(properties, ignorePropertyLookup, model);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectProperties"></param>
        /// <param name="obj"></param>
        private static void _MakeHtmlSafe<T>(PropertyInfo[] objectProperties, IDictionary<PropertyInfo, bool> ignorePropertyLookup, T obj) where T : class
        {
            for (int i = 0; i < objectProperties.Length; i++)
            {
                var prop = objectProperties[i];
                if (prop.CanRead && !ignorePropertyLookup[prop])
                {
                    object value = prop.GetValue(obj);
                    if (value != null)
                    {
                        if (prop.CanWrite && prop.PropertyType == typeof(string))
                        {
                            string sValue = value.ToString();
                            if (!string.IsNullOrWhiteSpace(sValue))
                                prop.SetValue(obj, HttpUtility.HtmlEncode(sValue));
                        }
                        else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType))
                        {
                            //****************************************************************************
                            //NOTE: We only handle homogeneous collections (typed collections and arrays)
                            //****************************************************************************
                            if ((prop.PropertyType.IsGenericType && prop.PropertyType.GenericTypeArguments[0].IsClass) ||
                                (prop.PropertyType.IsArray && prop.PropertyType.GetElementType().IsClass))
                            {
                                dynamic dValue = value;
                                AsHtmlSafeExtension.AsHtmlSafe(dValue);
                            }
                        }
                        else if (prop.PropertyType.IsClass && !prop.PropertyType.IsNullableType())
                        {
                            var valuePropeties = prop.PropertyType.GetProperties();
                            var valueAttributeLookup = CreateIgnorePropertyLookup(valuePropeties);
                            _MakeHtmlSafe(valuePropeties, valueAttributeLookup, value);
                        }
                    }
                }
            }
        }

        

        private static IDictionary<PropertyInfo, bool> CreateIgnorePropertyLookup(IEnumerable<PropertyInfo> properties)
        {
            Dictionary<PropertyInfo, bool> lookup = new Dictionary<PropertyInfo, bool>();
            foreach (PropertyInfo prop in properties)
                lookup.Add(prop, prop.GetCustomAttribute<SafeHtmlIgnoreAttribute>() != null);
            return lookup;
        }
    }

    public class SafeHtmlIgnoreAttribute : Attribute
    { }

    public static class TextExtensions
    {
        public static string Ellipse(this string value, int maxChars)
        {
            const string ellipses = "...";
            return value.Length <= maxChars ? value : value.Substring(0, maxChars - ellipses.Length) + ellipses;
        }
    }
}
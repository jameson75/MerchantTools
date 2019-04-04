using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace CipherPark.TriggerRed.Web.Models
{
    public static class EntityToModel
    {       
        public static T Convert<T>(object entity) where T : new()
        {
            Type entityType = entity.GetType();
            T model = new T();
            Type modelType = model.GetType();
            PropertyInfo[] modelProperties = modelType.GetProperties();
            for(int i = 0; i <modelProperties.Length; i++)
            {
                PropertyInfo modelProperty = modelProperties[i];
                var ignoreAttribute = modelProperty.GetCustomAttribute<EntityToModelDataMappingIgnoreAttribute>();
                if (ignoreAttribute == null && modelProperty.CanWrite)
                {
                    string modelPropertyName = null;
                    EntityToModelMappingOptions options = EntityToModelMappingOptions.None;
                    var mappingAttribute = modelProperty.GetCustomAttribute<EntityToModelMappingAttribute>();
                    if (mappingAttribute != null)
                    {
                        modelPropertyName = mappingAttribute.PropertyName;
                        options = mappingAttribute.Options;
                    }
                    else                    
                        modelPropertyName = modelProperty.Name;
                    //object value = entityType.GetProperty(modelPropertyName).GetValue(entity);                   
                    object value = entityType.GetDeepPropertyValue(modelPropertyName, entity);
                    modelProperty.SetValue(model, SmartCast(modelProperty.PropertyType, value, options));
                }
            }
            return model;
        }      

        /// <summary>
        /// Handles casting of nullable types and dates. Also html encodes all string types.
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static object SmartCast(Type destinationType, object value, EntityToModelMappingOptions options)
        {
            if (value == null)
            {
                if (destinationType.IsValueType && !destinationType.IsNullableType())
                    return Activator.CreateInstance(destinationType);
                else
                    return null;
            }
            else
            {
                object interpretedValue = null;
                if (options.HasFlag(EntityToModelMappingOptions.JavascriptDate))
                {                                        
                    DateTime dt = (DateTime)value;
                    long jsDateTime = dt.ToUnixMilliseconds();
                    interpretedValue = System.Convert.ChangeType(jsDateTime, destinationType);
                }
                else
                    interpretedValue = value;
                Type conversionType = destinationType.IsNullableType() ? destinationType.GetGenericArguments()[0] : destinationType;
                return System.Convert.ChangeType(interpretedValue, conversionType);             
            }
        }          
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class EntityToModelMappingAttribute : Attribute
    {
        public EntityToModelMappingAttribute()
        { }

        public EntityToModelMappingAttribute(string property, EntityToModelMappingOptions options = EntityToModelMappingOptions.None)
        {
            PropertyName = property;
            Options = options;
        }

        public string PropertyName { get; set; }
        public EntityToModelMappingOptions Options { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class EntityToModelDataMappingIgnoreAttribute : Attribute
    {
        
    }    

    [Flags]
    public enum EntityToModelMappingOptions
    {
        None = 0,
        JavascriptDate = 1,
    }

    public static class TypeExtension2
    {
        public static object GetDeepPropertyValue(this Type entityType, string propName, object entity)
        {
            object container = entity;
            Type containerType = entityType;
            string[] nestedProperties = propName.Split('.');
            object value = null;
            for (int i = 0; i < nestedProperties.Length; i++)
            {
                value = containerType.GetProperty(nestedProperties[i]).GetValue(container);
                if (value == null)
                    break;
                container = value;
                containerType = value.GetType();
            }
            return value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api
{
    public abstract class EbayClientBase
    {        
        public string AppId { get; set; }        

        protected static string SerializeToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                serializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        protected static T DeserializeFromXml<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(xml))
            {
                object result = serializer.Deserialize(textReader);
                return (T)result;
           }
        }     
    }   

    public enum EbayServiceEnvironment
    {
        Sandbox,
        Production
    }

    public static class EbayServiceDataFormat
    {
        public const string XML = "XML";
        public const string JSON = "JSON";
        public const string SOAP = "SOAP";
    }

    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}

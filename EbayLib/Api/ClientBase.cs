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
                try
                {
                    object result = serializer.Deserialize(textReader);
                    return (T)result;
                }
                catch 
                {
                    throw new InvalidOperationException("Failed to deserialize the following xml response: " + xml);
                }               
           }
        }     
    }

    public class ErrorMessage
    {
        [XmlElement("error")]
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        [XmlElement("category")]
        public ErrorCategory Category { get; set; }

        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlElement("errorId")]
        public long ErrorId { get; set; }

        [XmlElement("exceptionId")]
        public string ExceptionId { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("parameter")]
        public List<ErrorParameter> Parameters { get; set; }

        [XmlElement("severity")]
        public ErrorSeverity Severity { get; set; }

        [XmlElement("subdomain")]
        public string Subdomain { get; set; }
    }

    public class ErrorParameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public enum ErrorSeverity
    {
        Error,
        Warning
    }

    public enum ErrorCategory
    {
        Application,
        Request,
        System
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

    public static class WebRequestExtensions
    {
        private const uint DefaultRetries = 5;

        public static WebResponse GetResponseWithRetry(this HttpWebRequest webRequest, uint nRetries = DefaultRetries)
        {
            uint nFails = 0;
            while(true)
            {
                try { return webRequest.GetResponse(); }
                catch (Exception ex)
                {
                    nFails++;
                    if (nFails > nRetries)
                        throw new InvalidOperationException($"Web request failed after {nRetries} retries.", ex);
                }
            }
        }
    }
}

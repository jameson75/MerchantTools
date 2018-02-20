using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace CipherPark.Ebay.Api.Finding
{
    public abstract class FindingStandardResponse
    {          
        [XmlElement("ack")]
        public AckValue Ack { get; set; }

        [XmlElement("errorMessage")]
        public ErrorMessage ErrorMessage { get; set; }        
        
        [XmlElement("timestamp")]
        public DateTime TimeStamp { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }
    }

    public class PaginationInput
    {
        public const int PageFetchLimit = 100;
        [XmlElement("entriesPerPage")]
        public int EntriesPerPage { get; set; }
        [XmlElement("pageNumber")]
        public int PageNumber { get; set; }
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

    public enum AckValue
    {
        Failure,
        PartialFailure,
        Success,
        Warning
    }

}

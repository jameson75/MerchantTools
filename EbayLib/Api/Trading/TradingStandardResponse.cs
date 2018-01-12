using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Trading
{
    public abstract class TradingStandardResponse
    {        
        public AckValue Ack { get; set; } 
        [XmlElement("Errors")]    
        public ErrorSummary[] Errors { get; set; }       
        public DateTime Timestamp { get; set; } 
        public string Version { get; set; }
        public string CorrelationID { get; set; }
        public string Build { get; set; }
    }

    public class Pagination
    {
        public int EntriesPerPage { get; set; }
        public int PageNumber { get; set; }
    }

    public static class DetailLevelCodeType
    {
        public const string ItemReturnAttributes = "ItemReturnAttributes";
        public const string ItemReturnCategories = "ItemReturnCategories";
        public const string ItemReturnDescription = "ItemReturnDescription";
        public const string ReturnAll = "ReturnAll";
        public const string ReturnHeaders = "ReturnHeaders";
        public const string ReturnMessages = "ReturnMessages";
        public const string ReturnSummary = "ReturnSummary";
    }

    public enum AckValue
    {
        Failure,
        PartialFailure,
        Success,
        Warning
    }

    public enum ErrorCategory
    {
        Application,
        Request,
        System
    }

    public class ErrorParameter
    {
        [XmlAttribute("ParamID")]
        public string ParamID { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public enum ErrorSeverity
    {
        Error,
        Warning
    }

    public class ErrorSummary
    {
        public string ShortMessage { get; set; }
        public string LongMessage { get; set; }
        public string ErrorCode { get; set; }
        public string SeverityCode { get; set; }
        public string ErrorClassification { get; set; }
        public ErrorParameter[] ErrorParameters { get; set; }
    }
}

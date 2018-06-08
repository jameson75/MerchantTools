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

   

    public enum AckValue
    {
        Failure,
        PartialFailure,
        Success,
        Warning
    }

}

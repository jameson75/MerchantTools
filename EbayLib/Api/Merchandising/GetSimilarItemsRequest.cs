using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Merchandising
{
    [XmlRoot(ElementName = "getSimilarItemsRequest", Namespace = "http://www.ebay.com/marketplace/services")]
    public class GetSimilarItemsRequest : MerchandisingStandardRequest
    {
        public const int MaxResultsUpperLimit = 50;
        public const int DefaultMaxResults = 20;

        public GetSimilarItemsRequest()
        {
            MaxResults = DefaultMaxResults;
        }

        [XmlElement("categoryId")]
        public string[] CategoryIds { get; set; }

        [XmlElement("categoryIdExclude")]
        public string[] CategoryIdExcludes { get; set; }

        [XmlElement("endTimeFrom")]
        public DateTime EndTimeFrom { get; set; }

        [XmlElement("endTimeTo")]
        public DateTime EndTimeTo { get; set; }

        [XmlElement("itemId")]
        public string ItemId { get; set; }

        [XmlElement("listingType")]
        public ListingCodeType ListingType { get; set; }

        [XmlElement("maxResults")]
        public int MaxResults { get; set; }

        public override string OperationName {  get { return "getSimilarItems"; } }       
    }
}

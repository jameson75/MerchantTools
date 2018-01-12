using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Merchandising
{
    public abstract class MerchandisingStandardRequest : IEbayRequest
    {        
        [XmlElement("affiliate")]
        public Affiliate Affiliate { get; set; }      

        public abstract string OperationName { get; }
    }

    [XmlRoot(ElementName = "getMostWatchedItemsRequest", Namespace = "http://www.ebay.com/marketplace/services")]
    public class GetMostWatchedItemsRequest : MerchandisingStandardRequest
    {
        public const int MaxResultsUpperLimit = 50;
        public const int DefaultMaxResults = 20;

        public GetMostWatchedItemsRequest()
        {
            MaxResults = DefaultMaxResults;
        }

        [XmlElement("categoryId")]
        public string CategoryId { get; set; }

        [XmlElement("maxResults")]
        public int MaxResults { get; set; }

        public override string OperationName { get { return "getMostWatchedItems"; } }
    }

    public class Affiliate
    {
        [XmlElement("customId")]
        public string CustomId { get; set; }
        [XmlElement("networkId")]
        public string NetworkId { get; set; }
        [XmlElement("trackingId")]
        public string TrackingId { get; set; }
    }
}

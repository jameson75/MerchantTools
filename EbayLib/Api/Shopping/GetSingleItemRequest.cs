using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace CipherPark.Ebay.Api.Shopping
{
    [XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GetSingleItemRequest : IEbayRequest
    {
        public string IncludeSelector { get; set; }
        public string ItemID { get; set; }
        public string VariationSKU { get; set; }
        public string MessageID { get; set; }
        public string OperationName => "GetSingleItem";
    }
}

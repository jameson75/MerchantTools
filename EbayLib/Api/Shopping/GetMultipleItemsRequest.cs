using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Shopping
{
    [XmlRoot(Namespace ="urn:ebay:apis:eBLBaseComponents")]  
    public class GetMultipleItemsRequest : IEbayRequest
    {
        public const int MaxItemsLength = 20;

        [XmlElement("ItemID")]
        public string[] ItemIDs { get; set; }
        public string IncludeSelector { get; set; }
        public string OperationName { get { return "GetMultipleItems"; } }
    }
}

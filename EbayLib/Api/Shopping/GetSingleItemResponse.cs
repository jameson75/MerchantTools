using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Shopping
{
    [XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GetSingleItemResponse : ShoppingStandardResponse
    {
        public SimpleItem Item { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Merchandising
{
    [XmlRoot(ElementName = "getSimilarItemsResponse", Namespace = "http://www.ebay.com/marketplace/services")]
    public class GetSimilarItemsResponse : MerchandisingStandardResponse
    {
        [XmlElement("itemRecommendations")]
        public ItemRecommendations ItemRecommendations { get; set; }
    }
}

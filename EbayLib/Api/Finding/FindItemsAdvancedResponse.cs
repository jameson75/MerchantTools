using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Finding
{
    [XmlRoot(ElementName = "findItemsAdvancedResponse", Namespace = "http://www.ebay.com/marketplace/search/v1/services")]
    public class FindItemsAdvancedResponse : FindingStandardResponse
    { }
}

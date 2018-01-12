using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Finding
{
    [XmlRoot(ElementName = "findItemsByKeywordsRequest", Namespace = "http://www.ebay.com/marketplace/search/v1/services")]
    public class FindItemsByCategoryRequest : FindItemsBaseRequest
    {        
        [XmlElement("categoryId")]
        public string CategoryId { get; set; }
        [XmlElement("outputSelector")]
        public OutputSelectorType OutputSelector { get; set; }

        public override string OperationName { get { return "findItemsByCategory"; } }       
    }    
}

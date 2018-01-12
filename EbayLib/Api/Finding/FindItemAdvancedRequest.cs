using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Finding
{
    [XmlRoot(ElementName = "findAdvancedRequest", Namespace = "http://www.ebay.com/marketplace/search/v1/services")]
    public class FindItemAdvancedRequest : FindItemsBaseRequest
    {
        [XmlElement("aspectFilter")]
        public AspectFilter AspectFilter { get; set; }
        [XmlElement("categoryId")]
        public string CategoryId { get; set; }
        [XmlElement("descriptionSearch")]
        public bool DescriptionSearch { get; set; }
        [XmlElement("itemFilter")]
        public ItemFilter ItemFilter { get; set; }
        [XmlElement("keywords")]
        public string Keywords { get; set; }
        [XmlElement("outputSelector")]
        public OutputSelectorType OutputSelector { get; set; }
        public override string OperationName { get { return "findItemsByCategory"; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace CipherPark.Ebay.Api.Finding
{
    public abstract class FindItemsBaseResponse : FindingStandardResponse
    {
        [XmlElement("aspectHistogramContainer")]
        public AspectHistogramContainer AspectHistogramContainer { get; set; }
        [XmlElement("categoryHistogramContainer")]
        public CategoryHistogramContainer CategoryHistogramContainer { get; set; }
        [XmlElement("conditionHistogramContainer")]
        public ConditionHistogramContainer ConditionHistogramContainer { get; set; }     
        [XmlElement("itemSearchURL")]
        public string ItemSearchURL { get; set; }
        [XmlElement("paginationOutput")]
        public PaginationOutput PaginationOutput { get; set; }
        [XmlElement("searchResult")]
        public SearchResult SearchResult { get; set; }       
    }
}

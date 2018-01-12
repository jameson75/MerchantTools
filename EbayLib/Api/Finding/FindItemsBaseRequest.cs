using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Finding
{
    public abstract class FindItemsBaseRequest : IEbayRequest
    {
        [XmlElement("apsectFilter")]
        public List<AspectFilter> AspectFilters { get; set; }
        [XmlElement("itemFilter")]
        public List<ItemFilter> ItemFilters { get; set; }        
        [XmlElement("affiliate")]
        public Affiliate Affiliate { get; set; }
        [XmlElement("buyerPostalCode")]
        public string BuyerPostalCode { get; set; }
        [XmlElement("paginationInput")]
        public PaginationInput PaginationInput { get; set; }
        [XmlElement("sortOrder")]
        public SortOrderType SortOrder { get; set; }
                        
        public abstract string OperationName { get; }
    }
}

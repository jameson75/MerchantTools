using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Trading
{
    [XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GetCategoriesResponse : TradingStandardResponse
    {
        [XmlArray("CategoryArray")]
        [XmlArrayItem("Category")]
        public List<Category> CategoryArray { get; set; }
        public int CategoryCount { get; set; }
        public string CategoryVersion { get; set; }
        public double MinimumReservePrice { get; set; }
        public bool ReduceReserveAllowed { get; set; }
        public bool ReservePriceAllowed { get; set; }
        public DateTime UpdateTime { get; set; }        
    }

    public class Category
    {
        public bool AutoPayEnabled { get; set; }
        public bool B2BVATEnabled { get; set; }
        public bool BestOfferEnabled { get; set; }
        public string CategoryID { get; set; }
        public int CategoryLevel { get; set; }
        public string CategoryName { get; set; }
        [XmlElement("CategoryParentID")]
        public string[] CategoryParentIDs { get; set; }
        public bool Expired { get; set; }
        public bool LeafCategory { get; set; }
        public bool LSD { get; set; }
        public bool ORPA { get; set; }
        public bool ORRA { get; set; }
        public bool Virtual { get; set; }
    }
}

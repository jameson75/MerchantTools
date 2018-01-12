using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Finding
{
    [XmlRoot(ElementName = "findItemsByKeywordsRequest", Namespace = "http://www.ebay.com/marketplace/search/v1/services")]
    public class FindItemsByKeywordsRequest : FindItemsBaseRequest
    {
        [XmlElement("keywords")]
        public string Keywords { get; set; }
        [XmlElement("outputSelector")]
        public OutputSelectorType OutputSelector { get; set; }
        public override string OperationName { get { return "findItemsByKeywords"; } }
    }

    public class AspectFilter
    {
        [XmlElement("aspectName")]
        public string AspectName { get; set; }
        [XmlElement("aspectValueName")]
        public List<string> AspectValueNames { get; set; }        
    }

    public class ItemFilter
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("paramName")]
        public string ParamName { get; set; }
        [XmlElement("paramValue")]
        public string ParamValue { get; set; }
        [XmlElement("value")]
        public List<string> Values { get; set; }
    }

    public enum OutputSelectorType
    {
        AspectHistogram,
        CategoryHistogram,
        ConditionHistogram,
        GalleryInfo,
        PictureURLLarge,
        PictureURLSuperSize,
        SellerInfo,
        StoreInfo,
        UnitPriceInfo
    }

    public class Affiliate
    {
        [XmlElement("customId")]
        public string CustomId { get; set; }
        [XmlElement("geoTargeting")]
        public string GeoTargeting { get; set; }
        [XmlElement("networkId")]
        public string NetworkId { get; set; }
        [XmlElement("trackingId")]
        public string TrackingId { get; set; }
    }

    

    public enum SortOrderType
    {
        BestMatch,
        BidCountFewest,
        BidCountMost,
        CountryAscending,
        CountryDescending,
        CurrentPriceHighest,
        DistanceNearest,
        EndTimeSoonest,
        PricePlusShippingHighest,
        PricePlusShippingLowest,
        StartTimeNewest
    }
}

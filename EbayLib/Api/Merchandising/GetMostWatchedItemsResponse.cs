using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Merchandising
{
    public class MerchandisingStandardResponse
    {
        [XmlElement("ack")]
        public string Ack { get; set; }
        [XmlElement("errorMessage")]
        public ErrorMessage ErrorMessage;
        [XmlElement("timestamp")]
        public string Timestamp { get; set; }
        [XmlElement("version")]
        public string Version { get; set; }
    }

    public class ErrorMessage
    {
        [XmlElement("errorData")]
        public ErrorData[] Error { get; set; }
    }

    public class ErrorData
    {
        [XmlElement("category")]
        public ErrorCategory Category { get; set; }

        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlElement("errorId")]
        public long ErrorId { get; set; }

        [XmlElement("exceptionId")]
        public string ExceptionId { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("parameter")]
        public List<ErrorParameter> Parameters { get; set; }

        [XmlElement("severity")]
        public ErrorSeverity Severity { get; set; }

        [XmlElement("subdomain")]
        public string Subdomain { get; set; }
    }

    public enum ErrorSeverity
    {
        Error,
        Warning
    }

    public class ErrorParameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public enum ErrorCategory
    {
        Application,
        Request,
        System
    }

    [XmlRoot(ElementName="getMostWatchedItemsResponse", Namespace = "http://www.ebay.com/marketplace/services")]
    public class GetMostWatchedItemsResponse : MerchandisingStandardResponse
    {
        [XmlElement("itemRecommendations")]
        public ItemRecommendations ItemRecommendations { get; set; }
    }

    public class ItemRecommendations
    {
        [XmlElement("item")]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        [XmlElement("bidCount")]
        public int BidCount { get; set; }
        [XmlElement("buyItNowPrice")]
        public Amount BuyItNowPrice { get; set; }
        [XmlElement("country")]
        public string Country { get; set; }
        [XmlElement("currentPrice")]
        public Amount CurrentPrice { get; set; }
        [XmlElement("discountPrice")]
        public DiscountPriceInfo DiscountPrice { get; set; }
        [XmlElement("globalId")]
        public string GlobalId { get; set; }
        [XmlElement("imageURL")]
        public string ImageURL { get; set; }
        [XmlElement("itemId")]
        public string ItemId { get; set; }
        [XmlElement("originalPrice")]
        public Amount OriginalPrice { get; set; }
        [XmlElement("primaryCategoryId")]
        public string PrimaryCategoryId { get; set; }
        [XmlElement("primaryCategoryName")]
        public string PrimaryCategoryName { get; set; }
        [XmlElement("shippingType")]
        public string ShippingType { get; set; }
        [XmlElement("shippingCost")]
        public Amount ShippingCost { get; set; }
        [XmlElement("subtitle")]
        public string Subtitle { get; set; }
        [XmlElement("timeLeft")]
        public string TimeLeft { get; set; }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("viewItemURL")]
        public string ViewItemURL { get; set; }
        [XmlElement("watchCount")]
        public int WatchCount { get; set; }
    }

    public class DiscountPriceInfo
    {
        [XmlElement("originalRetailPrice")]
        public Amount OriginalRetailPrice { get; set; }
        [XmlElement("priceTreatment")]
        public PriceTreatment PriceTreatment { get; set; }
        [XmlElement("soldOffEbay")]
        public bool SoldOffEbay { get; set; }
        [XmlElement("soldOnEbay")]
        public bool SoldOnEbay { get; set; }     
    }

    public class Amount
    {
        [XmlAttribute("currencyId")]
        public string CurrencyId { get; set; }
        [XmlText]
        public double Value { get; set; }
    }

    public enum PriceTreatment
    {
        MAP,
        NONE,
        STP
    }
}

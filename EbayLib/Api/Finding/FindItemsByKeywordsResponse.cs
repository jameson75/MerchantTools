using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Finding
{
    [XmlRoot(ElementName = "findItemsByKeywordsResponse", Namespace = "http://www.ebay.com/marketplace/search/v1/services")]
    public class FindItemsByKeywordsResponse : FindItemsBaseResponse
    { }

    public class AspectHistogramContainer
    {
        [XmlElement("aspect")]
        public List<Aspect> Aspects { get; set; }
    }

    public class Aspect
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("valueHistogram")]
        public List<ValueHistogram> ValueHistograms { get; set; }
    }

    public class ValueHistogram
    {
        [XmlAttribute("valueName")]
        public string ValueName { get; set; }

        [XmlElement("count")]
        public long Count { get; set; }
    }

    public class CategoryHistogramContainer
    {
        [XmlElement("categoryHistogram")]
        public List<CategoryHistogram> CategoryHistogram { get; set; }        
    }

    public class CategoryHistogram
    {
        [XmlElement("categoryId")]
        public string CategoryId { get; set; }

        [XmlElement("category")]
        public Type PropertyName { get; set; }

        [XmlElement("childCategoryHistogram")]
        public List<CategoryHistogram> ChildCategoryHistograms { get; set; }

        [XmlElement("count")]
        public long Count { get; set; }
    }

    public class ConditionHistogramContainer
    {
        [XmlElement("conditionHistogram")]
        public List<ConditionHistogram> ConditionHistograms { get; set; }
    }

    public class ConditionHistogram
    {
        [XmlElement("condition")]
        public Condition Condition { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }
    }

    public class Condition
    {
        [XmlElement("conditionDisplayName")]
        public string ConditionDisplayName { get; set; }

        [XmlElement("conditionId")]
        public int ConditionId { get; set; }
    }    

    public class PaginationOutput
    {
        [XmlElement("entriesPerPage")]
        public int EntriesPerPage { get; set; }

        [XmlElement("pageNumber")]
        public int PageNumber { get; set; }

        [XmlElement("totalEntries")]
        public int TotalEntries { get; set; }

        [XmlElement("totalPages")]
        public int TotalPages { get; set; }
    }

    public class SearchResult
    {
        [XmlElement("item")]
        public List<SearchItem> Items { get; set; }
    }

    public class SearchItem
    {
        [XmlElement("attribute")]
        List<SearchItemAttribute> Attributes { get; set; }

        [XmlElement("autoPay")]
        public bool AutoPay { get; set; }

        [XmlElement("charityId")]
        public string CharityId { get; set; }

        [XmlElement("compatibility")]
        public int Compatibility { get; set; }

        [XmlElement("pageNumber")]
        public Condition Condition { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("discountPriceInfo")]
        public DiscountPriceInfo DiscountPriceInfo { get; set; }

        [XmlElement("distance")]
        public Distance Distance { get; set; }

        [XmlElement("eekStatus")]
        public List<string> EEKStatusCollection { get; set; }

        [XmlElement("galleryInfoContainer")]
        public GalleryInfoContainer GalleryInfoContainer { get; set; }

        [XmlElement("galleryPlusPictureURL")]
        public string GalleryPlusPictureURL { get; set; }

        [XmlElement("galleryURL")]
        public string GalleryURL { get; set; }

        [XmlElement("globalId")]
        public string GlobalId { get; set; }

        [XmlElement("isMultiVariationListing")]
        public bool IsMultiVariationListing { get; set; }

        [XmlElement("itemId")]
        public string ItemId { get; set; }

        [XmlElement("listingInfo")]
        public ListingInfo ListingInfo { get; set; }

        [XmlElement("location")]
        public string Location { get; set; }

        [XmlElement("paymentMethod")]
        public List<string> PaymentMethods { get; set; }

        [XmlElement("pictureURL")]
        public string PictureURL { get; set; }

        [XmlElement("pictureURLSuperSize")]
        public string PictureURLSuperSize { get; set; }

        [XmlElement("primaryCategory")]
        public Category PrimaryCategory { get; set; }

        [XmlElement("productId")]
        public string ProductId { get; set; }

        [XmlElement("returnsAccepted")]
        public bool ReturnsAccepted { get; set; }

        [XmlElement("secondaryCategory")]
        public Category SecondaryCategory { get; set; }

        [XmlElement("sellerInfo")]
        public SellerInfo SellerInfo { get; set; }

        [XmlElement("sellingStatus")]
        public SellingStatus SellingStatus { get; set; }

        [XmlElement("shippingInfo")]
        public ShippingInfo ShippingInfo { get; set; }

        [XmlElement("storeInfo")]
        public StoreFront StoreInfo { get; set; }

        [XmlElement("subtitle")]
        public string Subtitle { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("topRatedListing")]
        public bool TopRatedListing { get; set; }

        [XmlElement("unitPrice")]
        public UnitPriceInfo UnitPrice { get; set; }

        [XmlElement("viewItemURL")]
        public string ViewItemURL { get; set; }
    }

    public class SearchItemAttribute
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }
    }

    public class DiscountPriceInfo
    {
        [XmlElement("minimumAdvertisedPriceExposure")]
        public MapExposureEnum MinimumAdvertisedPriceExposure { get; set; }

        [XmlElement("originalRetailPrice")]
        public Amount OriginalRetailPrice { get; set; }

        [XmlElement("pricingTreatment")]
        public PriceTreatmentEnum PricingTreatment { get; set; }

        [XmlElement("soldOffEbay")]
        public bool SoldOffEbay { get; set; }

        [XmlElement("soldOnEbay")]
        public bool SoldOnEbay { get; set; }
    }

    public enum MapExposureEnum
    {
        DuringCheckout,
        PreCheckout
    }

    public class Amount
    {
        [XmlAttribute("currencyId")]
        public string CurrencyId { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    public enum PriceTreatmentEnum
    {
        /// <summary>
        /// MAP stands for Minimum Advertised Price.
        /// </summary>
        MAP,
        /// <summary>
        /// STP stands for Strike-Through Pricing.
        /// </summary>
        STP      
    }

    public class Distance
    {
        [XmlAttribute("unit")]
        public string Unit { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    public class GalleryInfoContainer
    {
        [XmlElement("galleryUrl")]
        public List<GalleryURL> GalleryURLs { get; set; }
    }

    public class GalleryURL
    {
        [XmlAttribute("gallerySize")]
        public GallerySizeEnum GallerySize { get; set; }

        [XmlText]
        public string Uri { get; set; }
    }

    public enum GallerySizeEnum
    {
        Large,
        Medium,
        Small       
    }

    public class ListingInfo
    {
        [XmlElement("bestOfferEnabled")]
        public bool BestOfferEnabled { get; set; }

        [XmlElement("buyItNowAvailable")]
        public bool BuyItNowAvailable { get; set; }

        [XmlElement("buyItNowPrice")]
        public Amount BuyItNowPrice { get; set; }

        [XmlElement("convertedBuyItNowPrice")]
        public Amount ConvertedBuyItNowPrice { get; set; }

        [XmlElement("endTime")]
        public DateTime EndTime { get; set; }

        [XmlElement("gift")]
        public bool Gift { get; set; }

        [XmlElement("listingType")]
        public string ListingType { get; set; }

        [XmlElement("startTime")]
        public DateTime StartTime { get; set; }
    }

    public class Category
    {
        [XmlElement("categoryId")]
        public string Id { get; set; }

        [XmlElement("categoryName")]
        public string Name { get; set; }
    }

    public class SellerInfo
    {
        [XmlElement("feedbackRatingStar")]
        public string FeedbackRatingStar { get; set; }

        [XmlElement("feedbackScore")]
        public long FeedbackScore{ get; set; }

        [XmlElement("positiveFeedbackPercent")]
        public Amount PositiveFeedbackPercent { get; set; }

        [XmlElement("sellerUserName")]
        public string SellerUserName { get; set; }

        [XmlElement("topRatedSeller")]
        public bool TopRatedSeller { get; set; }
    }

    public class SellingStatus
    {
        [XmlElement("bidCount")]
        public int BidCount { get; set; }

        [XmlElement("convertedCurrentPrice")]
        public Amount ConvertedCurrentPrice { get; set; }

        [XmlElement("currentPrice")]
        public Amount CurrentPrice { get; set; }

        [XmlElement("sellingState")]
        public string SellingState { get; set; }

        [XmlElement("timeLeft")]
        public string Duration { get; set; }
    }

    public class ShippingInfo
    {
        [XmlElement("expiditedShipping")]
        public bool ExpiditedShipping { get; set; }

        [XmlElement("handlingTime")]
        public int HandlingTime { get; set; }

        [XmlElement("oneDayShippingAvailable")]
        public bool OneDayShippingAvailable { get; set; }

        [XmlElement("shippingServiceCost")]
        public Amount ShippingServiceCost { get; set; }

        [XmlElement("shippingType")]
        public string ShippingType { get; set; }

        [XmlElement("shipToLocations")]
        public List<string> ShipToLocations { get; set; }
    }

    public class StoreFront
    {
        [XmlElement("storeName")]
        public string Name { get; set; }

        [XmlElement("storeURL")]
        public string Url { get; set; }
    }

    public class UnitPriceInfo
    {
        [XmlElement("quantity")]
        public double Quantity { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }  
}

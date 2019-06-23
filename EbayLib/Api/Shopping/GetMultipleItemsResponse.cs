using System;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Shopping
{
    public abstract class ShoppingStandardResponse
    {
        public string Ack { get; set; }
        public string CorrelationID { get; set; }
        [XmlElement("Errors")]
        public ErrorSummary[] Errors { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Version { get; set; }
    }

    public enum AckValue
    {
        Failure,
        PartialFailure,
        Success,
        Warning
    }

    public enum ErrorCategory
    {
        Application,
        Request,
        System
    }

    public class ErrorParameter
    {
        [XmlAttribute("ParamID")]
        public string ParamID { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public enum ErrorSeverity
    {
        Error,
        Warning
    }

    public class ErrorSummary
    {
        public string ShortMessage { get; set; }
        public string LongMessage { get; set; }
        public string ErrorCode { get; set; }
        public string SeverityCode { get; set; }
        public string ErrorClassification { get; set; }
        public ErrorParameter[] ErrorParameters { get; set; }
    }

    [XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GetMultipleItemsResponse : ShoppingStandardResponse
    {
        [XmlElement("Item")]
        public SimpleItem[] Items { get; set; }
    }
   
    public class SimpleItem
    {        
        public bool AutoPay { get; set; }
        public bool AvailableForPickupDropOff { get; set; }
        public int BidCount { get; set; }
        public BusinessSellerDetails BusinessSellerDetails { get; set; }
        public Charity Charity { get; set; }
        public string ConditionDescription { get; set; }
        public string ConditionDisplayedName { get; set; }
        public int ConditionID { get; set; }
        public double ConvertedCurrentPrice { get; set; }
        public string Country { get; set; }
        public double CurrentPrice { get; set; }
        public string Description { get; set; }
        public DiscountPriceInfo DiscountPriceInfo { get; set; }
        [XmlElement("eBayNowAvailable")]
        public bool EBayNowAvailable { get; set; }
        [XmlElement("eBayNowAvailalble")]
        public bool EBayNowEligible { get; set; }
        public bool EligibleForPickupDropOff { get; set; }
        public DateTime EndTime { get; set; }
        public string ExcludeShipToLocation { get; set; }
        public string GalleryURL { get; set; }
        public bool GlobalShipping { get; set; }
        public int HandlingTIme { get; set; }
        public SimpleUser HighBider { get; set; }
        public long HitCount { get; set; }
        public bool IgnoreQuantity { get; set; }
        public bool IntegratedMerchantCreditCardEnabled
        {
            get; set;
        }
        public string ItemID { get; set; }
        public NameValueListArray ItemSpecifics { get; set; }
        public string ListingType { get; set; }
        public string Location { get; set; }
        public int LotSize { get; set; }
        public double MinimumToBid { get; set; }
        public bool NewBestOffer { get; set; }
        [XmlElement("PaymentAllowedSite")]
        public string[] PaymentAllowedSite { get; set; }
        [XmlElement("PaymentMethods")]
        public string[] PaymentMethods { get; set; }
        [XmlElement("PictureURL")]
        public string[] PictureURL { get; set; }
        public string PostalCode { get; set; }
        public string PrimaryCategoryID { get; set; }
        public string PrimaryCategoryIDPath { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public string QuantityAvailableHit { get; set; }
        public QuantityInfo QuantityInfo { get; set; }
        public int QuantitySold { get; set; }   
        public int QuantitySoldByPickupInStore { get; set; }
        public int QuantityThreshold { get; set; }
        public bool ReserveMet { get; set; }
        public ReturnPolicy ReturnPolicy { get; set; }
        public string SecondaryCategoryID { get; set; }
        public string SecondaryCategoryIDPath { get; set; }
        public string SecondaryCategoryName { get; set; }
        public SimpleUser Seller { get; set; }     
        public ShippingCostSummary ShippingCostSummary { get; set; }
        public string ShipToLocations { get; set; }
        public string Site { get; set; }
        public string SKU { get; set; }
        public DateTime StartTime { get; set; }
        public StoreFront StoreFront { get; set; }
        public string Subtitle { get; set; }
        public string TimeLeft { get; set; }
        public string Title { get; set; }
        public bool TopRatedListing { get; set; }
        public UnitInfo UnitInfo { get; set; }
        public Variations Variations { get; set; }       
        public bool VhrAvailable { get; set; }
        public string VhrUrl { get; set; }
        public string ViewItemURLForNaturalSearch { get; set; }
        public int WatchCount { get; set; }
    }      

    public class Variations
    {

    }

    public class UnitInfo
    {

    }

    public class StoreFront
    {

    }

    public class ReturnPolicy
    {

    }

    public class ShippingCostSummary
    {

    }

    public class QuantityInfo
    {

    }

    public class NameValueListArray
    {

    }

    public class SimpleUser
    {
        public string AboutMeURL { get; set; }
        public string FeedbackDetailsURL { get; set; }
        public string FeedbackPrivate { get; set; }
        public string FeedbackRatingStar { get; set; }
        public int FeedbackScore { get; set; }
        public string MyWorldLargeImage { get; set; }
        public string MyWorldSmallImage { get; set; }
        public string MyWorldURL { get; set; }
        public string NewUser { get; set; }
        public string PositiveFeedbackPercent { get; set; }
        public string RegistrationDate { get; set; }
        public string RegistrationSite { get; set; }
        public string ReviewsAndGuidesURL { get; set; }
        public string SellerBusinessType { get; set; }
        public string SellerItemsURL { get; set; }
        public string SellerLevel { get; set; }
        public string Status { get; set; }
        public string StoreName { get; set; }
        public string StoreURL { get; set; }
        public string TopRatedSeller { get; set; }
        public string UserAnonymized { get; set; }
        public string UserID { get; set; }
    }

    public class BusinessSellerDetails
    {
        public string AdditionalContactInformation { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string LegalInvoice { get; set; }
        public string TermsAndConditions { get; set; }
        public string TradeRegistrationNumber { get; set; }
        public VATDetails VATDetails { get; set; }       
    }

    public class DiscountPriceInfo
    {

    }

    public class VATDetails
    {
        public string VATID { get; set; }
        public string VATPercent { get; set; }
        public string VATSite { get; set; }
    }

    public class Address
    {
        public string AddressID { get; set; }
        public string CityName { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string County { get; set; }
        public string ExternalAddressID { get; set; }
        public string FirstName { get; set; }
        public string InternationalName { get; set; }
        public string InternationalStateAndCity { get; set; }
        public string InternationalStreet { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
    }

    public class Charity
    {
        public string CharityID { get; set; }
        public string CharityName { get; set; }
        public string CharityNumber { get; set; }
        public string DonationPercent { get; set; }
        public string LogoURL { get; set; }
        public string Status { get; set; }
    }
}

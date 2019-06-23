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
    public class GetItemTransactionsResponse
    {
        public bool HasMoreTransactions { get; set; }
        public ItemType Item { get; set; }
        public int PageNumber { get; set; }
        public PaginationResultType PaginationResult { get; set; }
        public bool PayPalPreferred { get; set; }
        public int ReturnedTransactionCountActual { get; set; }
        [XmlArray("TransactionArray")]
        [XmlArrayItem("Transaction")]
        public List<TransactionType> TransactionArray { get; set; }
    }

    public class TransactionType
    {
        public AmountType ActualHandlingCost { get; set; }
        public AmountType ActualShippingCost { get; set; }
        public AmountType AdjustmentAmount { get; set; }
        public AmountType AmountPaid { get; set; }
        public bool BestOfferSale { get; set; }
        public UserType Buyer { get; set; }
        public ItemType Item { get; set; }
        public string BuyerCheckoutMessage { get; set; }
        public AmountType BuyerGuaranteePrice { get; set; }
        public BuyerPackageEnclosuresType BuyerPackageEnclosures { get; set; }
        public string CartID { get; set; }
        public OrderType ContainingOrder { get; set; }
        public AmountType ConvertedAdjustmentAmount { get; set; }
        public AmountType ConvertedAmountPaid { get; set; }
        public AmountType ConvertedTransactionPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DepositType { get; set; }
        public DigitalDeliverySelectedType DigitalDeliverySelected { get; set; }
        [XmlElement("eBayPlusTransaction")]
        public bool EbayPlusTransaction { get; set; }
        public string ExtendedOrderID { get; set; }
        [XmlElement("ExternalTransaction")]
        public ExternalTransactionType[] ExternalTransaction { get; set; }
        public AmountType FinalValueFee { get; set; }
        public bool Gift { get; set; }
        public GiftSummaryType GiftSummary { get; set; }
        public bool IntangibleItem { get; set; }
        public string InventoryReservationID { get; set; }
        public DateTime InvoiceSentTime { get; set; }
        public bool IsMultiLegShipping { get; set; }
        public ListingCheckoutRedirectPreferenceType ListingCheckoutRedirectPreference { get; set; }
        public string LogisticsPlanType { get; set; }
        public PaymentInformationType MonetaryDetails { get; set; }
        public MultiLegShippingDetailsType MultiLegShippingDetails { get; set; }
        public string OrderLineItemID { get; set; }
        public DateTime PaidTime { get; set; }
        public PaymentHoldDetailType PaymentHoldDetails { get; set; }
        public string PayPalEmailAddress { get; set; }
        public PickupDetailsType PickupDetails { get; set; }
        public PickupMethodSelectedType PickupMethodSelected { get; set; }
        public TransactionPlatformCodeType Platform { get; set; }
        public int QuantityPurchased { get; set; }
        //TODO: Continue Implementing.        
    }

    public class PaymentHoldDetailType
    {
        //TODO: Continue Implementing
    }

    public class PickupDetailsType
    {
        //TODO: Continue Implementing
    }

    public class PickupMethodSelectedType
    {
        //TODO: Continue Implementing
    }

    public class PaginationResultType
    {
        public int TotalNumberOfEntries { get; set; }
        public int TotalNumberOfPages { get; set; }
    }

    public class ItemType
    {
        public string ApplicationData { get; set; }
        public bool AutoPay { get; set; }
        public BuyerProtectionType BuyerProtection { get; set; }
        public CharityType Charity { get; set; }
        public CurrencyCodeType Currency { get; set; }
        public bool IntegratedMerchantCreditCardEnabled { get; set; }
        public InventoryTrackingMethodType InventoryTrackingMethod { get; set; }
        public string ItemID { get; set; }
        public ListingDetailsType ListingDetails { get; set; }
        public ListingCodeType ListingType { get; set; }
        public int LotSize { get; set; }
        [XmlElement("PaymentMethods")]
        public string[] PaymentMethods { get; set; }
        public bool PrivateListing { get; set; }
        public int Quantity { get; set; }
        public UserType Seller { get; set; }
        public SellingStatusType SellingStatus { get; set; }
        public string Site { get; set; }
        public string SKU { get; set; }
        public AmountType StartPrice { get; set; }
        public string Title { get; set; }
        public List<Variation> Variations { get; set; }
    }

    public class Variation
    {
        public int Quantity { get; set; }
        public SellingStatusType SellingStatus { get; set; }
        public string SKU { get; set; }
        public AmountType StartPrice { get; set; }
        [XmlElement("VariationSpecifics")]
        public VariationSpecifics[] VariationSpecifics { get; set; }
    }

    public class VariationSpecifics
    {
        public NameValueList[] NameValueList { get; set; }
    }

    public class NameValueList
    {
        [XmlElement("Name")]
        public string[] Name { get; set; }
        [XmlElement("Value")]
        public string[] Value { get; set; }
    }

    public class UserType
    {
        public bool AboutMePage { get; set; }
        [XmlElement("eBayGoodStanding")]
        public bool EbayGoodStanding { get; set; }
        public string EIASToken { get; set; }
        public string Email { get; set; }
        public bool FeedbackPrivate { get; set; }
        public int FeedbackScore { get; set; }
        public bool IDVerified { get; set; }
        public bool NewUser { get; set; }
        public float PositiveFeedbackPercent { get; set; }
        public DateTime RegistrationDate { get; set; }
        public SellerType SellerInfo { get; set; }
        public string Site { get; set; }
        public string Status { get; set; }
        public string UserFirstName { get; set; }
        public string UserID { get; set; }
        public string UserIDChanged { get; set; }
        public DateTime UserIDLastChanged { get; set; }
        public string UserLastName { get; set; }
        public string VATStatus { get; set; }
        public BuyerType BuyerInfo { get; set; }        
    }

    public class OrderType
    {
        public long OrderLineItemCount { get; set; }
        /*TODO: Continute Implementing*/
    }

    public class DigitalDeliverySelectedType
    { /*TODO: Continute Implementing*/}

    public class ExternalTransactionType
    { /*TODO: Continute Implementing*/}

    public class GiftSummaryType
    { /*TODO: Continute Implementing*/}

    public class ListingCheckoutRedirectPreferenceType
    { /*TODO: Continute Implementing*/}

    public class PaymentInformationType
    { /*TODO: Continute Implementing*/}

    public class MultiLegShippingDetailsType
    { /*TODO: Continute Implementing*/}

    public class BuyerPackageEnclosuresType
    { /*TODO: Continute Implementing*/}

    public class BuyerType
    {
        [XmlElement("BuyTaxIdentifier")]
        public TaxIdentifierType[] BuyerTaxIdentifier { get; set; }
        public AddressType ShippingAddress { get; set; }
    }

    public class AddressType
    {
        [XmlElement("AddressAttribute")]
        public EntityAttribute[] AddressAttribute { get; set; }
        public string AddressID { get; set; }
        public string AddressUsage { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string ExternalAddressID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string StateOrProvince { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set;  }       
    }

    public class TaxIdentifierType
    {
        [XmlElement("TaxIdentifierAttributeType")]
        public EntityAttribute[] TaxIdentifierAttributeType { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
    }

    public class EntityAttribute
    {
        [XmlAttribute("type")]
        public string CodeType { get; set; }
        [XmlText]
        public string Type { get; set; }
    }

    public class SellingStatusType
    {
        public int BidCount { get; set; }
        public AmountType ConvertedCurrentPrice { get; set; }
        public AmountType FinalValueFee { get; set; }
        public string ListingStatus { get; set; }
        public int QuantitySold { get; set; }
    }

    public class SellerType
    {
        public bool AllowPaymentEdit { get; set; }
        public bool CheckoutEnabled { get; set; }
        public bool CIPBankAccount { get; set; }
        public bool GoodStanding { get; set; }
        public bool QualifiesForB2BVAT { get; set; }
        public bool SafePaymentExempt { get; set; }
        public SellerLevelCodeType SellerLevel { get; set; }
        public bool StoreOwner { get; set; }
        public string StoreURL { get; set; }
        public bool TopRatedSeller { get; set; }      
    }

    public enum SellerLevelCodeType
    {
        Bronze,
        CustomCode,
        Gold,
        None,
        Platinum,
        Silver,
        Titanium
    }    

    public class ListingDetailsType
    {
        public bool Adult { get; set; }
        public bool BindingAuction { get; set; }
        public bool CheckoutEnabled { get; set; }
        public AmountType ConvertedBuyItNowPrice { get; set; }
        public AmountType ConvertedReservePrice { get; set; }
        public AmountType ConvertedStartPrice { get; set; }
        public DateTime EndTime { get; set; }
        public bool HasPublicMessage { get; set; }
        public bool HasReservePrice { get; set; }
        public bool HasUnansweredQuestions { get; set; }
        public AmountType MiniumBestOfferPrice { get; set; }
        public string RelistedItemID { get; set; }
        public DateTime StartTime { get; set; }
        public string TCROriginalItemID { get; set; }
        public string ViewItemURL { get; set; }
        public string ViewItemURLForNaturalSearch { get; set; }       
    }

    public class AmountType
    {
        [XmlAttribute("currencyID")]
        public string CurrencyID { get; set; }
        [XmlText]
        public double Value { get; set; }
    }

    public enum InventoryTrackingMethodType
    {
        CustomCode,
        ItemID,
        SKU
    }

    public enum BuyerProtectionType
    {
        CustomCode,
        ItemEligible,
        ItemIneligible,
        ItemMarkedEligible,
        ItemMarkedIneligible,
        NoCoverage
    }

    public class CharityType
    {
        public bool CharityListing { get; set; }
    }

    public enum CurrencyCodeType
    {
        ADP, // value is a 3-digit code for an Andorran peseta, a currency used in Andorra.
        AED, // value is a 3-digit code for an United Arab Emirates dirham, a currency used in the United Arab Emirates.
        AFA, // value is a 3-digit code for an Afghan afghani, a currency used in Afghanistan.
        ALL, // value is a 3-digit code for an Albanian lek, a currency used in Albania.
        AMD, // value is a 3-digit code for an Armenian dram, a currency used in Armenia.
        ANG, // value is a 3-digit code for a Netherlands Antillean guilder, a currency used in Curacao and Sint Maarten.
        AOA, // value is a 3-digit code for an Angolan kwanza, a currency used in Angola.
        ARS, // value is a 3-digit code for an Argentine peso, a currency used in Argentina.
        ATS, // value is a 3-digit code for an Austrian schilling, a currency previously used in Austria.This currency has been replaced by the Euro (3-digit code: EUR).
        AUD, // value is a 3-digit code for an Australia dollar, a currency used in Australia.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Australia site (Site ID 15).
        AWG, // value is a 3-digit code for an Aruban florin, a currency used in Aruba.
        AZM, // value is a 3-digit code for an Azerbaijani manat, a currency used in Azerbaijan.
        BAM, // value is a 3-digit code for a Bosnia and Herzegovina convertible mark, a currency used in Bosnia and Herzegovina.
        BBD, // value is a 3-digit code for a Barbados dollar, a currency used in Barbados.
        BDT, // value is a 3-digit code for a Bangladeshi taka, a currency used in Bangladesh.
        BGL, // value is a 3-digit code for the old Bulgarian lev, a currency previously used in Bulgaria.This currency has been replaced by the new Bulgarian lev(3-digit code: BGN).
        BGN, // value is a 3-digit code for a Bulgarian lev, a currency used in Bulgaria.This currency replaced the old Bulgarian lev(3-digit code: BGL).
        BHD, // value is a 3-digit code for a Bahraini dinar, a currency used in the Bahrain.
        BIF, // value is a 3-digit code for a Burundian franc, a currency used in Burundi.
        BMD, // value is a 3-digit code for a Bermudian dollar, a currency used in Bermuda.
        BND, // value is a 3-digit code for a Brunei dollar, a currency used in Brunei and Singapore.
        BOB, // value is a 3-digit code for a Boliviano, a currency used in Bolivia.
        BOV, // value is a 3-digit code for a Bolivian Mvdol, a currency used in Bolivia.
        BRL, // value is a 3-digit code for a Brazilian real, a currency used in Brazil.
        BSD, // value is a 3-digit code for a Bahamian dollar, a currency used in the Bahamas.
        BTN, // value is a 3-digit code for a Bhutanese ngultrum, a currency used in Bhutan.
        BWP, // value is a 3-digit code for a Botswana pula, a currency used in Botswana.
        BYR, // value is a 3-digit code for a Belarusian ruble, a currency used in Belarus.
        BZD, // value is a 3-digit code for a Belize dollar, a currency used in Belize.
        CAD, // value is a 3-digit code for a Canadian dollar, a currency used in Canada.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Canada site (Site ID 2). Note that items listed on the Canada site can also specify 'USD'.
        CDF, // value is a 3-digit code for a Congolese franc, a currency used in Democratic Republic of Congo.
        CHF, // Swiss Franc. For eBay, you can only specify, // currency for listings you submit to the Switzerland site (site ID 193).
        CLF, // value is a 3-digit code for a Unidad de Fomento, a currency used in Chile.
        CLP, // value is a 3-digit code for a Chilean peso, a currency used in Chile.
        CNY, // value is a 3-digit code for a Chinese yuan(also known as the renminbi), a currency used in China.
        COP, // value is a 3-digit code for a Columbian peso, a currency used in Columbia.
        CRC, // value is a 3-digit code for a Costa Rican colon, a currency used in Costa Rica.
        CUP, // value is a 3-digit code for a Cuban peso, a currency used in Cuba.
             //CustomCode Reserved for internal or future use.
        CVE, // value is a 3-digit code for a Cape Verde escudo, a currency used in Cape Verde.
        CYP, // value is a 3-digit code for a Cypriot pound, a currency previously used in Cyprus.This currency has been replaced by the Euro (3-digit code: EUR).
        CZK, // value is a 3-digit code for a Czech koruna, a currency used in the Czech Republic.
        DJF, // value is a 3-digit code for a Djiboutian franc, a currency used in Djibouti.
        DKK, // value is a 3-digit code for a Danish krone, a currency used in Denmark, the Faroe Islands, and Greenland.
        DOP, // value is a 3-digit code for a Dominican peso, a currency used in the Dominican Republic.
        DZD, // value is a 3-digit code for an Algerian dinar, a currency used in Algeria.
        ECS, // value is a 3-digit code for an Ecuadorian sucre, a currency previously used in Ecuador.This currency has been replaced by the US Dollar (3-digit code: USD).
        ECV, // value is an old 3-digit code for a Cape Verde escudo, a currency used in Cape Verde.The 'ECV' code has been replaced by 'CVE'.
        EEK, // value is a 3-digit code for an Estonian kroon, a currency previously used in Estonia.This currency has been replaced by the Euro (3-digit code: EUR).
        EGP, // value is a 3-digit code for an Egyptian pound, a currency used in Egypt.
        ERN, // value is a 3-digit code for an Eritrean nakfa, a currency used in Eritrea.
        ETB, // value is a 3-digit code for an Ethiopian birr, a currency used in Ethiopia.
        EUR, // value is a 3-digit code for a EURO, a currency used in Andorra, Austria, Belgium, Cyprus, Estonia, Finland, France, Germany, Greece, Ireland, Italy, Kosovo, Luxembourg, Malta, Monaco, Montenegro, Netherlands, Portugal, San Marino, Slovakia, Slovenia, Spain, and Vatican City.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the following sites: Austria (Site ID 16), Belgium_French(Site ID 23), France(Site ID 71), Germany(Site ID 77), Italy(Site ID 101), Belgium_Dutch(Site ID 123), Netherlands(Site ID 146), Spain(Site ID 186), and Ireland(Site ID 205).
        FJD, // value is a 3-digit code for a Fiji dollar, a currency used in Fiji.
        FKP, // value is a 3-digit code for a Falkland Islands pound, a currency used in the Falkland Islands.
        GBP, // value is a 3-digit code for a Pound sterling, a currency used in the United Kingdom and British Crown dependencies.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay UK site (Site ID 3).
        GEL, // value is a 3-digit code for a Georgian Iari, a currency used in the country of Georgia.
        GHC, // value is an old 3-digit code for a Ghanaian cedi, a currency used in Ghana.The 'GHC' code has been replaced by 'GHS'.
        GIP, // value is a 3-digit code for a Gibraltar pound, a currency used in Gibraltar.
        GMD, // value is a 3-digit code for a Gambian dalasi, a currency used in Gambia.
        GNF, // value is a 3-digit code for a Guinean franc, a currency used in Guinea.
        GTQ, // value is a 3-digit code for a Guatemalan quetzal, a currency used in Guatemala.
        GWP, // value is a 3-digit code for a Guinea-Bissau peso, a currency previously used in Guinea-Bissau.This currency has been replaced by the West African CFA franc (3-digit code: XOF).
        GYD, // value is a 3-digit code for a Guyanese dollar, a currency used in Guyana.
        HKD, // value is a 3-digit code for a Hong Kong dollar, a currency used in Hong Kong and Macau.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Hong Kong site (Site ID 201).
        HNL, // value is a 3-digit code for a Honduran lempira, a currency used in Honduras.
        HRK, // value is a 3-digit code for a Croatian kuna, a currency used in Croatia.
        HTG, // value is a 3-digit code for a Haitian gourde, a currency used in Haiti.
        HUF, // value is a 3-digit code for a Hungarian forint, a currency used in Hungary.
        IDR, // value is a 3-digit code for an Indonesian rupiah, a currency used in Indonesia.
        ILS, // value is a 3-digit code for an Israeli new shekel, a currency used in Israel and in the Palestinian territories.
        INR, // value is a 3-digit code for an Indian rupee, a currency used in India.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay India site (Site ID 203).
        IQD, // value is a 3-digit code for an Iraqi dinar, a currency used in Iraq.
        IRR, // value is a 3-digit code for an Iranian rial, a currency used in Iran.
        ISK, // value is a 3-digit code for an Icelandic krona, a currency used in Iceland.
        JMD, // value is a 3-digit code for a Jamaican dollar, a currency used in Jamaica.
        JOD, // value is a 3-digit code for a Jordanian dinar, a currency used in Jordan.
        JPY, // value is a 3-digit code for a Japanese yen, a currency used in Japan.
        KES, // value is a 3-digit code for a Kenyan shilling, a currency used in Kenya.
        KGS, // value is a 3-digit code for a Kyrgzstani som, a currency used in Kyrgystan.
        KHR, // value is a 3-digit code for a Cambodian riel, a currency used in Cambodia.
        KMF, // value is a 3-digit code for a Comoro franc, a currency used in Comoros.
        KPW, // value is a 3-digit code for a North Korean won, a currency used in North Korea.
        KRW, // value is a 3-digit code for a South Korean won, a currency used in South Korea.
        KWD, // value is a 3-digit code for a Kuwaiti dollar, a currency used in Kuwait.
        KYD, // value is a 3-digit code for a Cayman Islands dollar, a currency used in the Cayman Islands.
        KZT, // value is a 3-digit code for a Kazahhstani tenge, a currency used in Kazakhstan.
        LAK, // value is a 3-digit code for a Lao kip, a currency used in Laos.
        LBP, // value is a 3-digit code for a Lebanese pound, a currency used in Lebanon.
        LKR, // value is a 3-digit code for a Sri Lankan rupee, a currency used in Sri Lanka.
        LRD, // value is a 3-digit code for a Liberian dollar, a currency used in Liberia.
        LSL, // value is a 3-digit code for a Lesotho loti, a currency used in Lesotho.
        LTL, // value is a 3-digit code for a Lithuanian litas, a currency used in Lithuania.
        LVL, // value is a 3-digit code for a Latvian lats, a currency used in Latvia.
        LYD, // value is a 3-digit code for a Libyan dinar, a currency used in Libya.
        MAD, // value is a 3-digit code for a Moroccan dirham, a currency used in Morocco.
        MDL, // value is a 3-digit code for a Moldovan leu, a currency used in Moldova.
        MGF, // value is a 3-digit code for a Malagay franc, a currency previously used in Madagascar.This currency has been replaced by the Malagasy ariary (3-digit code: MGA).
        MKD, // value is a 3-digit code for a Macedonian denar, a currency used in Macedonia.
        MMK, // value is a 3-digit code for a Myanma kyat, a currency used in Myanmar.
        MNT, // value is a 3-digit code for a Mongolian tugrik, a currency used in Mongolia.
        MOP, // value is a 3-digit code for a Macanese pataca, a currency used in Macao.
        MRO, // value is a 3-digit code for a Mauritanian ouguiya, a currency used in Mauritania.
        MTL, // value is a 3-digit code for a Maltese lira, a currency previously used in Malta.This currency has been replaced by the Euro(3-digit code: EUR).
        MUR, // value is a 3-digit code for a Mauritian rupee, a currency used in Mauritius.
        MVR, // value is a 3-digit code for a Maldivian rufiyaaa, a currency used in the Maldives.
        MWK, // value is a 3-digit code for a Malawian kwacha, a currency used in Malawi.
        MXN, // value is a 3-digit code for a Mexican peso, a currency used in Mexico.
        MXV, // value is a 3-digit funds code for a Mexican peso, a currency used in Mexico.
        MYR, // value is a 3-digit code for a Malaysian Ringgit, a currency used in Malaysia.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Malaysia site (Site ID 207).
        MZM, // value is an old 3-digit code for a Mozambican metical, a currency used in Mozambique.The 'MZM' code has been replaced by 'MZN'.
        NAD, // value is a 3-digit code for a Namibian dollar, a currency used in Namibia.
        NGN, // value is a 3-digit code for a Nigerian naira, a currency used in Nigeria.
        NIO, // value is a 3-digit code for a Nicaraguan cordoba, a currency used in Nicaragua.
        NOK, // value is a 3-digit code for a Norwegian kron, a currency used in Norway, Svalbard, Jan Mayen, Bouvet Island, Queen Maud Land, and Peter I Island.
        NPR, // value is a 3-digit code for a Nepalese rupee, a currency used in Nepal.
        NZD, // value is a 3-digit code for a New Zealand dollar, a currency used in the Cook Islands, New Zealand, Niue, Pitcairn, and Tokelau, Ross Dependency.
        OMR, // value is a 3-digit code for an Omani rial, a currency used in Oman.
        PAB, // value is a 3-digit code for a Panamanian balboa, a currency used in Panama.
        PEN, // value is a 3-digit code for a Peruvian nuevo sol, a currency used in Peru.
        PGK, // value is a 3-digit code for a Papua New Guinea kina, a currency used in Papua New Guinea.
        PHP, // value is a 3-digit code for a Philippine peso, a currency used in the Philippines., // is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Philippines site (Site ID 211).
        PKR, // value is a 3-digit code for a Pakistani rupee, a currency used in Pakistan.
        PLN, // value is a 3-digit code for a Polish zloty, a currency used in Poland.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Poland site (Site ID 212).
        PYG, // value is a 3-digit code for a Paraguayan guarani, a currency used in Paraguay.
        QAR, // value is a 3-digit code for a Qatari riyal, a currency used in Qatar.
        ROL, // value is a 3-digit code for the old Romanian leu, a currency previously used in Romania.This currency has been replaced by the Romanian new leu(3-digit code: RON).
        RUB, // value is a 3-digit code for a Russian rouble, a currency used in Russia, Abkhazia, and South Ossetia.This value replace the old 3-digit code for the Russian rouble, 'RUR'.
        RUR, // value is the old 3-digit code for a Russian rouble, a currency used in Russia.This value was replaced by the new 3-digit code for the Russian rouble, 'RUB'.
        RWF, // value is a 3-digit code for a Rwandan franc, a currency used in Rwanda.
        SAR, // value is a 3-digit code for a Saudi riyal, a currency used in Saudi Arabia.
        SBD, // value is a 3-digit code for a Solomon Islands dollar, a currency used in the Solomon Islands.
        SCR, // value is a 3-digit code for a Seychelles rupee, a currency used in Seychelles.
        SDD, // value is the 3-digit code for a Sudanese dinar, a currency previously used in Sudan.The Sudanese dinar was replaced by the Sudanese pound, which has a 3-digit code of 'SDG'.
        SEK, // value is a 3-digit code for a Swedish krona, a currency used in Swedn.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Sweden site (Site ID 218).
        SGD, // value is a 3-digit code for a Singapore dollar, a currency used in Singapore and Brunei.This is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay Singapore site (Site ID 216).
        SHP, // value is a 3-digit code for a Saint Helena pound, a currency used in Saint Helena.
        SIT, // value is a 3-digit code for a Slovenian tolar, a currency previously used in Slovenia.This currency has been replaced by the Euro (3-digit code: EUR).
        SKK, // value is a 3-digit code for a Slovak koruna, a currency previously used in Slovakia.This currency has been replaced by the Euro(3-digit code: EUR).
        SLL, // value is a 3-digit code for a Sierra Leonean leone, a currency used in Sierra Leone.
        SOS, // value is a 3-digit code for a Somali shilling, a currency used in Somalia.
        SRG, // value is the 3-digit code for a Suriname guilder, a currency previously used in Suriname.The Surinam guilder was replaced by the Surinamese dollar, which has a 3-digit code of 'SRD'.
        STD, // value is a 3-digit code for a Sao Tome and Principe dobra, a currency used in Sao Tome and Principe.
        SVC, // value is a 3-digit code for a Salvadoran colon, a currency previously used in El Salvador., // currency has been replaced by the US Dollar (3-digit code: USD).
        SYP, // value is a 3-digit code for a Syrian pound, a currency used in Syria.
        SZL, // value is a 3-digit code for a Swazi lilangeni, a currency used in Swaziland.
        THB, // value is a 3-digit code for a Thai baht, a currency used in Thailand.
        TJS, // value is a 3-digit code for a Tajikistani somoni, a currency used in Tajikistan.
        TMM, // value is the old 3-digit code for a Turkmenistani manat, a currency used in Turkmenistan.This value was replaced by the new 3-digit code for the Turkmenistani manat, 'TMT'.
        TND, // value is a 3-digit code for a Tunisian dinar, a currency used in Tunisia.
        TOP, // value is a 3-digit code for a Tongan pa'anga, a currency used in Tonga.
        TPE, // value is a 3-digit code for a Portuguese Timorese escudo, a currency previously used in Portuguese Timor.
        TRL, // value is the old 3-digit code for a Turkish lira, a currency used in Turkey and Northern Cyprus., // value was replaced by the new 3-digit code for the Turkish lira, 'TRY'.
        TTD, // value is a 3-digit code for a Trinidad and Tobago dollar, a currency used in Trinidad and Tobago.
        TWD, // value is a 3-digit code for the New Taiwan dollar, a currency used in Taiwan.
        TZS, // value is a 3-digit code for a Tanzanian shilling, a currency used in Tanzania.
        UAH, // value is a 3-digit code for a Ukrainian hryvnia, a currency used in the Ukraine.
        UGX, // value is a 3-digit code for a Ugandan shilling, a currency used in Uganda.
        USD, // value is a 3-digit code for a US dollar, a currency used in the United States, America Samoa, Barbados, Bermuda, British Indian Ocean Territory, British Virgin Islands, Caribbean Netherlands, Ecuador, El Salvador, Guam, Haiti, Marshall Islands, Federated States of Micronesia, Northern Mariana Islands, Palau, Panama, Puerto Rico, Timor-Leste, Turks and Caicos Islands, US Virgin Islands, and Zimbabwe., // is the value that should be passed in the Item.Currency field by the seller when listing an item on the eBay US or US eBay Motors site (Site ID 0). 'USD' can also be specified as the Item.Currency on the eBay Canada site (Site ID 2).
        USN, // value is a 3-digit code for a next-day transaction involving US dollars.
        USS, // value is a 3-digit code for a same-day transaction involving US dollars.
        UYU, // value is a 3-digit code for a Uruguayan peso, a currency used in Uruguay.
        UZS, // value is a 3-digit code for a Uzbekistan som, a currency used in the Uzbekistan.
        VEB, // value is a 3-digit code for a Venezuelan bolivar, a currency previously used in Venezuela.The Venezuela bolivar was replaced by the Venezuelan bolivar fuerte, which has a 3-digit code of 'VEF'.
        VND, // value is a 3-digit code for a Vietnamese dong, a currency used in Vietnam.
        VUV, // value is a 3-digit code for a Vanuatu vatu, a currency used in Vanuatu.
        WST, // value is a 3-digit code for a Samoan tala, a currency used in Samoa.
        XAF, // value is a 3-digit code for a Central African CFA franc, a currency used in Cameroon, Central African Republic, Republic of the Congo, Chad, Equatorial Guinea, and Gabon.
        XCD, // value is a 3-digit code for an Easy Caribbean dollar, a currency used in Anguilla, Antigua and Barbuda, Dominica, Grenada, Montserrat, Saint Kitts and Nevis, Saint Lucia, and Saint Vincent and the Grenadines.
        XOF, // value is a 3-digit code for a West African CFA franc, a currency used in Benin, Burkina Faso, Cote d'Ivoire, Guinea-Bissau, Mali, Niger, Senegal, and Togo.
        XPF, // value is a 3-digit code for a CFP franc, a currency used in French Polynesia, New Caledonia, and Wallis and Futuna.
        YER, // value is a 3-digit code for a Yemeni rial, a currency used in Yemen.
        YUM, // value is a 3-digit code for a Yugoslav dinar, a currency previously used in Yugoslavia.The Yugoslav dinar was replaced by the Serbian dinar, which has a 3- digit code of 'RSD'.
        ZAR, // value is a 3-digit code for a South African rand, a currency used in South Africa.
        ZMK, // value is the old 3-digit code for a Zambian kwacha, a currency used in Zambia.The 'ZMK' code has been replaced by 'ZMW'.
        ZWD // value is the old 3-digit code for a Zimbabwean dollar, a currency previously used in Zimbabwe.The US dollar replaced the Zimbabwean dollar as the official currency in Zimbabwe.
    }
}

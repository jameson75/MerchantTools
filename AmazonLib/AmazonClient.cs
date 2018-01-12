using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Amazon.Api
{
    public class AmazonClient
    {
        private long? _lastCallTime = null;

        public const string service = "AWSECommerceService";

        public string AWSSecretAccessKey { get; set; }

        public ulong Throttle { get; set; }

        private void BuildEndPointUrl(StringBuilder requestUrlBuilder)
        {
            //Append endpoint (url).
            const string endPoint = "http://webservices.amazon.com/onca/xml";
            requestUrlBuilder.Append($"{endPoint}?");

            //Append url parameter Service                        
            requestUrlBuilder.Append($"Service={service}");
        }

        private void BuildAdvertisingApiUrl(StringBuilder requestUrlBuilder, AdvertisingApiRequest request)
        {
            BuildEndPointUrl(requestUrlBuilder);

            //Append url parameter AWSAccessKeyId
            if (request.AWSAccessKeyId == null)
                throw new InvalidOperationException("AWSAccessKeyId is a required parameter and was not specified");
            requestUrlBuilder.Append($"&AWSAccessKeyId={request.AWSAccessKeyId}");

            //Append url parameter AssociateTag
            if (request.AssociateTag == null)
                throw new InvalidOperationException("AssociateTag is a required parameter and was not specified");
            requestUrlBuilder.Append($"&AssociateTag={request.AssociateTag}");

            //Append url parameter Operation
            if (request.Operation == null)
                throw new InvalidOperationException("Operation is a required parameter and was not specified.");
            requestUrlBuilder.Append($"&Operation={request.Operation}");

            //Append url parameter MerchantId (Optional)
            if (request.MerchantId != null)
                requestUrlBuilder.Append($"&MerchantId={request.MerchantId}");

            //Append url parameter ResponseGroup (Optional)
            if (request.ResponseGroups != null)
                requestUrlBuilder.Append($"&ResponseGroup={UrlEncode(string.Join(",", request.ResponseGroups))}");

            //Append url parameter Version (Optional)
            if (request.Version != null)
                requestUrlBuilder.Append($"&Version={request.Version}");            
        }

        private void AppendSignature(StringBuilder requestUrlBuilder)
        {
            if (this.AWSSecretAccessKey == null)
                throw new InvalidOperationException("AWSSecretAccessKey not was not specified.");

            //********************************************************************************************************
            //We generate the signature parameter value using the technique describe by the amazon guide in the docs.
            //See Guide: http://docs.aws.amazon.com/AWSECommerceService/latest/DG/rest-signature.html         
            //********************************************************************************************************

            //Guide Step 1
            //Append url parameter Timestamp
            requestUrlBuilder.Append($"&Timestamp={UrlEncode(DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"))}");

            //Guide Step 2 is already complete (all parameters should already be url encoded).
            //NOTE: Only parameters which potentially contain invalid url characters were explicitly URL Encoded.

            //Guide Steps 3, 4 and 5.
            string canonical = string.Join("&", requestUrlBuilder.ToString().Split(new[] { '?' })[1].Split(new[] { '&' }).OrderBy(e => string.Join(".", Encoding.Default.GetBytes(e).Select(b => ((int)b).ToString("{000}")).ToArray())).ToArray());

            //Guide Steps 6 and 7.
            string stringToSign = $"GET\nwebservices.amazon.com\n/onca/xml\n{canonical}";

            //Guide Step 8.
            byte[] key = Encoding.Default.GetBytes(this.AWSSecretAccessKey);
            HMACSHA256 hMac = new HMACSHA256(key);
            byte[] hash = hMac.ComputeHash(Encoding.Default.GetBytes(stringToSign));
            string signature = Convert.ToBase64String(hash);
            
            //Guide Step 9.
            requestUrlBuilder.Append($"&Signature={UrlEncode(signature)}");
        }

        public ItemSearchResponse ItemSearch(ItemSearchRequest request)
        {
            StringBuilder requestUrlBuilder = new StringBuilder();

            //Append url and parameters related to all advertising api requests.
            BuildAdvertisingApiUrl(requestUrlBuilder, request);

            //Append url parameter SearchIndex
            if (request.SearchIndex == null && request.BrowseNode == null)
                throw new InvalidOperationException("SearchIndex not specified");

            if( request.SearchIndex != null)
                requestUrlBuilder.Append($"&SearchIndex={request.SearchIndex}");

            //Append url parameter Keywords (Optional)
            if (request.Keywords != null)
                requestUrlBuilder.Append($"&Keywords={UrlEncode(string.Join(" ", request.Keywords))}");

            //Append url parameter Actor (Optional)
            if (request.Actor != null)
                requestUrlBuilder.Append($"&Actor={UrlEncode(request.Actor)}");

            //Append url parameter Artist (Optional)
            if (request.Artist != null)
                requestUrlBuilder.Append($"&Artist={UrlEncode(request.Artist)}");

            //Append url parameter AudienceRating (Optional)
            if (request.AudienceRating != null)
                requestUrlBuilder.Append($"&AudienceRating={request.AudienceRating}");

            //Append url parameter Author (Optional)
            if (request.Author != null)
                requestUrlBuilder.Append($"&Author={UrlEncode(request.Author)}");

            //Append url parameter Availability (Optional)
            if (request.Availability != null)
                requestUrlBuilder.Append($"&Availability={request.Availability}");

            //Append url parameter Brand (Optional)
            if (request.Brand != null)
                requestUrlBuilder.Append($"&Brand={UrlEncode(request.Brand)}");

            //Append url parameter BrowseNode (Optional)
            if (request.BrowseNode != null)
                requestUrlBuilder.Append($"&BrowseNode={request.BrowseNode}");

            //Append url parameter Composer (Optional)
            if (request.Composer != null)
                requestUrlBuilder.Append($"&Composer={UrlEncode(request.Composer)}");

            //Append url parameter Conductor (Optional)
            if (request.Conductor != null)
                requestUrlBuilder.Append($"&Conductor={UrlEncode(request.Conductor)}");

            //Append url parameter Director (Optional)
            if (request.Director != null)
                requestUrlBuilder.Append($"&Director={UrlEncode(request.Director)}");

            //Append url parameter IncludeReviewsSummary (Optional)
            if (request.IncludeReviewsSummary != null)
                requestUrlBuilder.Append($"&IncludeReviewsSummary={request.IncludeReviewsSummary}");

            //Append url parameter ItemPage (Optional)
            if (request.ItemPage != null)
                requestUrlBuilder.Append($"&ItemPage={request.ItemPage}");

            //Append url parameter Manufacturer (Optional)
            if (request.Manufacturer != null)
                requestUrlBuilder.Append($"&Manufacturer={UrlEncode(request.Manufacturer)}");

            //Append url parameter MaximumPrice (Optional)
            if (request.MaximumPrice != null)
                requestUrlBuilder.Append($"&MaximumPrice={request.MaximumPrice}");

            //Append url parameter MinimumPrice (Optional)
            if (request.MinimumPrice != null)
                requestUrlBuilder.Append($"&MinimumPrice={request.MinimumPrice}");

            //Append url parameter MinPercentageOff (Optional)
            if (request.MinPercentageOff != null)
                requestUrlBuilder.Append($"&MinPercentageOff={request.MinPercentageOff}");

            //Append url parameter Orchestra (Optional)
            if (request.Orchestra != null)
                requestUrlBuilder.Append($"&Orchestra={UrlEncode(request.Orchestra)}");

            //Append url parameter Power (Optional)
            if (request.Power != null)
                requestUrlBuilder.Append($"&Power={request.Power}");

            //Append url parameter Publisher (Optional)
            if (request.Publisher != null)
                requestUrlBuilder.Append($"&Publisher={UrlEncode(request.Publisher)}");

            //Append url parameter RelatedItemPage (Optional)
            if (request.RelatedItemPage != null)
                requestUrlBuilder.Append($"&RelatedItemPage={request.RelatedItemPage}");

            //Append url parameter RelationshipType (Optional)
            if (request.RelationshipType != null)
                requestUrlBuilder.Append($"&RelationshipType={request.RelationshipType}");

            //Append url parameter Sort (Optional)
            if (request.Sort != null)
                requestUrlBuilder.Append($"&Sort={request.Sort}");

            //Append url parameter Title (Optional)
            if (request.Title != null)
                requestUrlBuilder.Append($"&Title={UrlEncode(request.Title)}");

            //Append url parameter TruncateReviewsAt (Optional)
            if (request.TruncateReviewsAt != null)
                requestUrlBuilder.Append($"&TruncateReviewsAt={request.TruncateReviewsAt}");

            //Append url parameter VariationPage (Optional)
            if (request.VariationPage != null)
                requestUrlBuilder.Append($"&VariationPage={request.VariationPage}");

            //Append url parameter Signature  
            AppendSignature(requestUrlBuilder);

            //Send Request
            string response = SendRequest(requestUrlBuilder.ToString());

            //Return deserialized response
            return DeserializeFromXml<ItemSearchResponse>(response);
        }

        public BrowseNodeLookupResponse BrowseNodeLookup(BrowseNodeLookupRequest request)
        {
            StringBuilder requestUrlBuilder = new StringBuilder();

            //Append url and parameters related to all advertising api requests.
            BuildAdvertisingApiUrl(requestUrlBuilder, request);

            //Append url parameter BrowseNodeId
            if (request.BrowseNodeId != null)
                requestUrlBuilder.Append($"&BrowseNodeId={request.BrowseNodeId}");

            //Append url parameter Signature
            AppendSignature(requestUrlBuilder);

            //Send Request
            string response = SendRequest(requestUrlBuilder.ToString());

            //Return deserialized response
            return DeserializeFromXml<BrowseNodeLookupResponse>(response);
        }

        public ItemLookupResponse ItemLookup(ItemLookupRequest request)
        {
            StringBuilder requestUrlBuilder = new StringBuilder();

            //Append url and parameters related to all advertising api requests.
            BuildAdvertisingApiUrl(requestUrlBuilder, request);

            //Append url parameter Condition
            if (request.Condition != null)
                requestUrlBuilder.Append($"&Condition={request.Condition}");

            //Append url parameter IdType
            if (request.IdType != null)
                requestUrlBuilder.Append($"&IdType={request.IdType}");

            //Append url parameter IncludeReviewsSummary
            if (request.IncludeReviewsSummary != null)
                requestUrlBuilder.Append($"&IncludeReviewsSummary={request.IncludeReviewsSummary}");

            //Append url parameter ItemId
            if (request.ItemId != null)
                requestUrlBuilder.Append($"&ItemId={UrlEncode(request.ItemId)}");

            //Append url parameter MerchantId
            if (request.MerchantId != null)
                requestUrlBuilder.Append($"&MerchantId={request.MerchantId}");

            //Append url parameter RelatedItemPage
            if (request.RelatedItemPage != null)
                requestUrlBuilder.Append($"&RelatedItemPage={request.RelatedItemPage}");

            //Append url parameter SearchIndex
            if (request.SearchIndex != null)
                requestUrlBuilder.Append($"&SearchIndex={request.SearchIndex}");

            //Append url parameter TruncateReviewsAt
            if (request.TruncateReviewsAt != null)
                requestUrlBuilder.Append($"&TruncateReviewsAt={request.TruncateReviewsAt}");

            //Append url parameter VariationPage
            if (request.VariationPage != null)
                requestUrlBuilder.Append($"&VariationPage={request.VariationPage}");

            //Append url parameter Signature
            AppendSignature(requestUrlBuilder);

            //Send Request
            string response = SendRequest(requestUrlBuilder.ToString());

            //Return deserialized response
            return DeserializeFromXml<ItemLookupResponse>(response);
        }

        protected string SendRequest(string url)
        {
            ThrottleCall();
                     
            string responseData = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = System.Net.Http.HttpMethod.Get.Method;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                responseData = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                responseStream.Close();
                responseStream.Dispose();
            }

            catch (WebException webEx)
            {
                Stream responseStream = webEx.Response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                responseData = reader.ReadToEnd();
            }

            return responseData;
        }

        protected static T DeserializeFromXml<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader textReader = XmlReader.Create(new StringReader(xml), new XmlReaderSettings
            {              
                CloseInput = true,
                DtdProcessing = DtdProcessing.Ignore, //We found some DTD's causing parsing errors, so we're ignoring it.
            }))
            {
                try
                {
                    object result = serializer.Deserialize(textReader);
                    return (T)result;
                }
                catch(Exception ex)
                {
                    string message = "Error occured deserializing response\n" + xml;
                    throw new InvalidOperationException(message, ex);                        
                }
            }
        }

        private static string UrlEncode(string expr)
        {
            return WebUtility.UrlEncode(expr).Replace("+", "%20").Replace("!", "%21");
        }

        private void ThrottleCall()
        {
            if(Throttle > 0)
            {
                long waitTime = _lastCallTime == null ? 0 : Math.Max(0, (long)Throttle - (Environment.TickCount - _lastCallTime.Value));
                if (waitTime > 0)
                    System.Threading.Thread.Sleep((int)waitTime);
            }
            _lastCallTime = Environment.TickCount;
        }
    }

    public abstract class AdvertisingApiRequest
    {
        #region Required
        public string AWSAccessKeyId { get; set; }
        public string AssociateTag { get; set; }
        public abstract string Operation { get; }
        #endregion

        #region Optional
        public string MerchantId { get; set; }
        public string[] ResponseGroups { get; set; }
        public string Version { get; set; }
        #endregion
    }
    
    public class ItemLookupRequest : AdvertisingApiRequest
    {
        public string Condition { get; set; }
        public string IdType { get; set; }
        public string IncludeReviewsSummary { get; set; }
        public string ItemId { get; set; }
        public string RelatedItemPage { get; set; }
        public string RelationshipType { get; set; }
        public string SearchIndex { get; set; }
        public string TruncateReviewsAt { get; set; }
        public string VariationPage { get; set; }
        public override string Operation { get { return "ItemLookup"; } }
    }

    [XmlRoot("ItemLookupResponse", Namespace = "http://webservices.amazon.com/AWSECommerceService/2011-08-01")]
    public class ItemLookupResponse
    {
        public OperationRequestEcho OperationRequest { get; set; }
        public ListedItems Items { get; set; }
    }

    //http://docs.aws.amazon.com/AWSECommerceService/latest/DG/RequiredItemSearchParameters.html
    public class ItemSearchRequest : AdvertisingApiRequest
    {
        public string Actor { get; set; }
        public string Artist { get; set; }
        public string AudienceRating { get; set; }
        public string Author { get; set; }
        public string Availability { get; set; }
        public string Brand { get; set; }
        public string BrowseNode { get; set; }
        public string Composer { get; set; }
        public string Conductor { get; set; }
        public string Director { get; set; }
        public string IncludeReviewsSummary { get; set; }
        public string ItemPage { get; set; }
        public string Manufacturer { get; set; }
        public string MaximumPrice { get; set; }
        public string MinimumPrice { get; set; }
        public string MinPercentageOff { get; set; }
        public string Orchestra { get; set; }
        public string Power { get; set; }
        public string Publisher { get; set; }
        public string RelatedItemPage { get; set; }
        public string RelationshipType { get; set; }
        public string Sort { get; set; }
        public string Title { get; set; }
        public string TruncateReviewsAt { get; set; }
        public string VariationPage { get; set; }
        public string SearchIndex { get; set; }
        public string[] Keywords { get; set; }
        public string Condition { get; set; }
        public override string Operation { get { return "ItemSearch"; } }
    }

    public class BrowseNodeLookupRequest : AdvertisingApiRequest
    {
        public string BrowseNodeId { get; set; }
        public override string Operation { get { return "BrowseNodeLookup"; } }
    }

    [XmlRoot("BrowseNodeLookupResponse", Namespace = "http://webservices.amazon.com/AWSECommerceService/2011-08-01")]
    public class BrowseNodeLookupResponse
    {
        public OperationRequestEcho OperationRequest { get; set; }
        public BrowseNodes BrowseNodes { get; set; }
    }

    public class BrowseNodes
    {
        [XmlElement("BrowseNode")]
        public BrowseNode[] BrowseNodeCollection { get; set; }
    }

    public class BrowseNode
    {
        public string BrowseNodeId { get; set; }
        public string Name { get; set; }
        public bool IsCategoryRoot { get; set; }
        public List<BrowseNode> Children { get; set; }
        public List<BrowseNode> Ancestors { get; set; }
        public List<TopSeller> TopSellers { get; set; }
    }

    public class TopSeller
    {
        public string ASIN { get; set; }
        public string Title { get; set; }
    }

    [XmlRoot("ItemSearchResponse", Namespace="http://webservices.amazon.com/AWSECommerceService/2011-08-01")]
    public class ItemSearchResponse
    {
       public OperationRequestEcho OperationRequest { get; set; }        
       public ListedItems Items { get; set; }
    }

    public class ListedItems
    {
        public SearchItemsRequestEcho Request { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }        
        public List<SearchIndex> SearchResultsMap { get; set; }       
        [XmlElement("Item")]
        public ListedItem[] ItemCollection { get; set; }
    }

    public class SearchIndex
    {
        public string IndexName { get; set; }
        public int Results { get; set; }
        public int Pages { get; set; }
        public int RelevanceRank { get; set; }
        [XmlElement("ASIN")]
        public string[] ASINCollection { get; set; }
    }

    public class SearchItemsRequestEcho
    {
        public string IsValid { get; set; }
        public SearchItemsRequestEchoDetails ItemSearchRequest { get; set; }
    }

    public class SearchItemsRequestEchoDetails
    {
        public string Keywords { get; set; }
        public string[] ResponseGroup { get; set; }
        public string SearchIndex { get; set; }      
        public string AWSAccessKeyId { get; set; }
    }

    public class ListedItem
    {
        public string ASIN { get; set; }
        public string ParentASIN { get; set; }
        public string DetailPageURL { get; set; }
        public ItemAttributes ItemAttributes { get; set; }       
        public OfferSummary OfferSummary { get; set; } 
        public Offers Offers { get; set; }
        public Seller Seller { get; set; }
        public long SalesRank { get; set; }
        public ItemImage SmallImage { get; set; }
        public ItemImage MediumImage { get; set; }
        public ItemImage LargeImage { get; set; }
        public List<ImageSet> ImageSets { get; set; }
    }

    public class Offers
    {
        public int TotalOffers { get; set; }
        public int TotalOfferPages { get; set; }
        public string MoreOffersUrl { get; set; }
        [XmlElement("Offer")]
        public List<Offer> OfferCollection { get; set; }
    }

    public class OfferListing
    {
        public string OfferListingId { get; set; }
        public ItemPrice Price { get; set; }
        public ItemPrice AmountSaved { get; set; }
        public double PercentageSaved { get; set; }
        public string Availability { get; set; }
        public AvailabilityAttributes AvailabilityAttributes { get; set; }
        public bool IsEligibleForSuperSaverShipping { get; set; }
        public bool IsEligibleForPrime { get; set; }
    }

    public class AvailabilityAttributes
    {
        public string AvailabilityType { get; set; }
        public int MinimumHours { get; set; }
        public int MaximumHours { get; set; }
    }

    public class Offer
    {
        public OfferAttributes OfferAttributes { get; set; } 
        public OfferListing OfferListing { get; set; }    
    }

    public class OfferAttributes
    {
        public string Condition { get; set; }
    }

    public class OfferSummary
    {
        public ItemPrice LowestNewPrice { get; set; }
        public ItemPrice LowestUsedPrice { get; set; }
        public int TotalNew { get; set; }
        public int TotalUsed { get; set; }
        public int TotalCollectible { get; set; }
        public int TotalRefurbished { get; set; }
    }

    public class ItemPrice
    {
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }             
        public string FormattedPrice { get; set; }
    }

    public class Temp
    {
        public string AdditionalName { get; set; }
        //Skipped AlternateVersion - need to define container.
        public string AudioFormat { get; set; }
        //Skipped Benefit - need to define container.
        //Skipped Benefits - need to define container.
        public string BenefitType { get; set; }
        public string BenefitDescription { get; set; }
        //Skipped Bin - need to define container.\
        public int BinItemCount { get; set; }
        public string BinName { get; set; }
        //Skipped BinParameter - need to define container.
        public int BrowseNodeId { get; set; }
        public int CartId { get; set; }
        public string CartItemId { get; set; }
        //Skipped CartItems - need to define container.
        public string Cateogry { get; set; }
        public string Code { get; set; }
        //Skipped Collection - need to define container.
        //Skipped CollectionItem - need to define container.
        //Skipped CollectionParent - need to define container.
        public string CorrectedQuery { get; set; }
    }

    public class ItemAttributes
    {
        public string Actor { get; set; }
        public string Artist { get; set; }
        public string AspectRatio { get; set; }
        public string AudienceRating { get; set; }
        public string Author { get; set; }
        public string Binding { get; set; }
        public string Brand { get; set; }
        public string CEROAgeRating { get; set; }
        public string ClothingSize { get; set; }
        public string Color { get; set; }
        public string Comment { get; set; }
        public string ComponentType { get; set; }
        public string Creator { get; set; }
        public string Department { get; set; }
        public string Director { get; set; }
        public string EAN { get; set; }
        //public EANList EANList
        public string EISBN { get; set; }
        public string ESRBAgeRating { get; set; }
        public string Feature { get; set; }
        public string Format { get; set; }
        public string Genre { get; set; }
        public string HazardousMaterialType { get; set; }
        public bool IsAdultProduct { get; set; }
        public bool IsAutographed { get; set; }
        public string ISBN { get; set; }
        public bool IsEligibleForTradeIn { get; set; }
        public bool IsMemorabilia { get; set; }
        //public Dimensions ItemDimensions { get; set; }
        public int IssuesPerYear { get; set; }
        public ItemPrice ListPrice { get; set; }
        public string OfferListingId { get; set; }
        public string Manufacturer { get; set; }
        public int ManufactuerMaxiumAge { get; set; }
        public int ManufactuerMinimumAge { get; set; }
        public string ManufacturerPartsWarrantyDescription { get; set; }
        public string MaterialType { get; set; }
        public string MediaType { get; set; }
        public string MetalType { get; set; }
        public string Model { get; set; }
        public string MPN { get; set; }
        public int NumberOfDiscs { get; set; }
        public int NumberOfIssues { get; set; }
        public int NumberOfItems { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfTracks { get; set; }
        public string OperatingSystem { get; set; }
        public string PartNumber { get; set; }
        public string Platform { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }
        public string RegionCode { get; set; }
        public string RelatedItem { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Role { get; set; }
        public string RunningTime { get; set; }
        public string SeikodoProductCode { get; set; }
        public string Size { get; set; }
        public string SKU { get; set; }
        public string Studio { get; set; }
        public string SubscriptionLength { get; set; }
        public string Title { get; set; }
        public object TradeInValue { get; set; }
        public string UPC { get; set; }
        //public UPCList UPCList 
        public string Warranty { get; set; }       
    }   

    public class About
    {
        [XmlAttribute("About")]
        public string Details { get; set; }
        public string AboutMe { get; set; }
    }
    
    public class Seller
    {
        public About About { get; set; }
    }

    public class ItemRequestEcho
    {

    }

    public class OperationRequestEcho
    {
        public string RequestId { get; set; }     
        public List<NameValueElement> HTTPHeaders { get; set; }
        public List<NameValueElement> Arguments { get; set; }
    }    

    public class ImageSet
    {
        public ItemImage SwatchImage { get; set; }
        public ItemImage SmallImage { get; set; }
        public ItemImage ThumbnailImage { get; set; }
        public ItemImage TinyImage { get; set; }
        public ItemImage MediumImage { get; set; }
        public ItemImage LargeImage { get; set; }
    }

    public class ItemImage
    {
        public string URL { get; set; }
        public Dimension Height { get; set; }
        public Dimension Width { get; set; }
    }

    public class Dimension
    {
        [XmlText]
        public string Value { get; set; }
        [XmlAttribute]
        public string Units { get; set; }
    }

    public class NameValueElement
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }       

    [AttributeUsage(AttributeTargets.Property)]
    public class UrlParameterAttribute : Attribute
    {
        public string ParameterName { get; set; }
        public string ValueListSeparator { get; set; }
        public UrlParameterAttribute(string parameterName, string valueSeparator)
        {
            ParameterName = parameterName;
            ValueListSeparator = valueSeparator;
        }
    }

    public class LocaleInformation
    {
        public LocaleInformation(string department, string searchIndex, string browseNodeId)
        {
            Department = department;
            SearchIndex = searchIndex;
            BrowseNodeId = browseNodeId;            
        }
        public string Department { get; set; }
        public string SearchIndex { get; set; }
        public string BrowseNodeId { get; set; }  
        public string HeirarchyStyle { get; set; }      
    }
    
    public static class LocaleInformationLookup
    {       
        public static LocaleInformation[] US = new []
        {
            //*************************************************************************************
            //NOTE: The ids' in this table are were retrieved from the amazon docs.
            //See here: http://docs.aws.amazon.com/AWSECommerceService/latest/DG/LocaleUS.html
            //It's important to note that the browse node id's specified in the docs are NOT
            //the id's of the browse node root. Instead, the appeart to be the ids' of the 
            //"Categories" node that exist directly under the root.
            //*************************************************************************************

            new LocaleInformation("All Departments", "All", null),
            new LocaleInformation("Amazon Instant Video", "UnboxVideo", "2858778011"),
            new LocaleInformation("Appliances", "Appliances", "2619526011"),
            new LocaleInformation("Apps & Games", "MobileApps", "2350150011"),
            new LocaleInformation("Arts, Crafts & Sewing", "ArtsAndCrafts", "2617942011" ),
            new LocaleInformation("Automotive", "Automotive", "15690151"),
            new LocaleInformation("Baby", "Baby", "165797011"),
            new LocaleInformation("Beauty", "Beauty", "11055981"),
            new LocaleInformation("Books", "Books", "1000"),
            new LocaleInformation("CDs & Vinyl", "Music", "301668"),
            new LocaleInformation("Cell Phones & Accessories", "Wireless", "2335753011"),
            new LocaleInformation("Clothing, Shoes & Jewelry", "Fashion", "7141124011"),
            new LocaleInformation("Clothing, Shoes & Jewelry - Baby", "FashionBaby", "7147444011"),
            new LocaleInformation("Clothing, Shoes & Jewelry - Boys", "FashionBoys", "7147443011"),
            new LocaleInformation("Clothing, Shoes & Jewelry - Girls", "FashionGirls", "7147442011"),
            new LocaleInformation("Clothing, Shoes & Jewelry - Men", "FashionMen", "7147441011"),
            new LocaleInformation("Clothing, Shoes & Jewelry - Women", "FashionWomen", "7147440011"),
            new LocaleInformation("Collectibles & Fine Arts", "Collectibles", "4991426011"),
            new LocaleInformation("Computers", "PCHardware", "541966"),
            new LocaleInformation("Digital Music", "MP3Downloads", "624868011"),
            new LocaleInformation("Electronics", "Electronics", "493964"),
            new LocaleInformation("Gift Cards", "GiftCards", "2864120011"),
            new LocaleInformation("Grocery & Gourmet Food", "Grocery", "16310211"),
            new LocaleInformation("Health & Personal Care", "HealthPersonalCare", "3760931"),
            new LocaleInformation("Home & Kitchen", "HomeGarden", "1063498"),
            new LocaleInformation("Industrial & Scientific", "Industrial", "16310161"),
            new LocaleInformation("Kindle Store", "KindleStore", "133141011"),
            new LocaleInformation("Luggage & Travel Gear", "Luggage", "9479199011"),
            new LocaleInformation("Magazine Subscriptions", "Magazines", "599872"),
            new LocaleInformation("Movies & TV", "Movies", "2625374011"),
            new LocaleInformation("Musical Instruments", "MusicalInstruments", "11965861"),
            new LocaleInformation("Office Products", "OfficeProducts", "1084128"),
            new LocaleInformation("Patio, Lawn & Garden", "LawnAndGarden", "3238155011"),
            new LocaleInformation("Pet Supplies", "PetSupplies", "2619534011"),
            new LocaleInformation("Prime Pantry", "Pantry", null),
            new LocaleInformation("Software", "Software", "409488"),
            new LocaleInformation("Sports & Outdoors", "SportingGoods", "3375301"),
            new LocaleInformation("Tools & Home Improvement", "Tools", "468240"),
            new LocaleInformation("Toys & Games", "Toys", "165795011"),
            new LocaleInformation("Video Games", "VideoGames", "11846801"),
            new LocaleInformation("Wine", "Wine", "2983386011"),
            new LocaleInformation(null, "Blended", null),            
        };           
        
        public static LocaleInformation GetById(this LocaleInformation[] table, string browseNodeId)
        {
            return table.First(l => l.BrowseNodeId == browseNodeId);
        }  

        public static LocaleInformation GetByIndex(this LocaleInformation[] table, string searchIndex)
        {
            return table.First(l => l.SearchIndex == searchIndex);
        }    
    }

    public static class BrowseNodeExtension
    {
        public static BrowseNode GetCategoriesNode(this BrowseNode browseNode)
        {
            if (browseNode.Name == "Categories")
                return browseNode;

            if (browseNode.Children != null && browseNode.Children.Count > 0 && browseNode.Children[0].Name == "Categories")
                return browseNode.Children[0];

            if (browseNode.Ancestors == null || browseNode.Ancestors.Count == 0)
                throw new InvalidOperationException("Unable to determine Categories ancestor browse node");

            return browseNode.Ancestors[0].GetCategoriesNode();
        }
    }
}

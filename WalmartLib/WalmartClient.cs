using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace CipherPark.Walmart.Api
{
    public class WalmartClient
    {
        public string ApiKey { get; set; }
        public string LastError { get; set; }

        public ItemsResponse PaginatedNext(ItemsResponse prevResponse)
        {
            if (!string.IsNullOrEmpty(prevResponse.NextPage))
            {
                string url = $"http://api.walmartlabs.com{prevResponse.NextPage}";
                string responseXml = SendRequest(url);
                return DeserializeFromXml<ItemsResponse>(responseXml);
            }
            else
                return null;
        }

        public ItemsResponse PaginatedFirst(string format = WalmartServiceDataFormat.XML, string category = null, string brand = null, SpecialOffer specialOffer = SpecialOffer.none )
        {
            if (format != WalmartServiceDataFormat.XML)
                throw new NotSupportedException("Specified format not supported.");

            string endPoint = $"http://api.walmartlabs.com/v1/paginated/items";
            string url = BuildPaginatedUrl(endPoint, format, category, brand, specialOffer, ApiKey);
            string responseXml = SendRequest(url);
            return DeserializeFromXml<ItemsResponse>(responseXml);
        }       

        public SearchResponse Search(string query, int? categoryId = null, string facet = null, string format = WalmartServiceDataFormat.XML)
        {
            if (format != WalmartServiceDataFormat.XML)
                throw new NotSupportedException("Specified format not supported.");

            string endPoint = $"http://api.walmartlabs.com/v1/search";
            string url = BuildSearchUrl(endPoint, query, categoryId, facet, format, ApiKey);
            string responseXml = SendRequest(url);           
            return DeserializeFromXml<SearchResponse>(responseXml);
        }

        public CategoriesResponse Categories(string format = WalmartServiceDataFormat.XML)
        {
            if (format != WalmartServiceDataFormat.XML)
                throw new NotSupportedException("Specified format not supported.");

            string endPoint = $"http://api.walmartlabs.com/v1/taxonomy";
            string url = $"{endPoint}?format={format}&apiKey={ApiKey}";
            string responseXml = SendRequest(url);
            return DeserializeFromXml<CategoriesResponse>(responseXml);
        }
        
        #region Support Methods    

        private string BuildSearchUrl(string endPoint, string query, int? categoryId, string facet, string format, string apiKey)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(endPoint);
            urlBuilder.Append("?");
            if(query != null)
                urlBuilder.Append($"&query={UrlEncode(query)}");
            if (categoryId != null)
                urlBuilder.Append($"&categoryId={categoryId.Value}");
            if (facet != null)
                urlBuilder.Append($"&facet=on&facet.filter={facet}");
            urlBuilder.Append($"&format={format}");
            urlBuilder.Append($"&apiKey={ApiKey}");
            return urlBuilder.Replace("?&", "?").ToString();
        }

        private string BuildPaginatedUrl(string endPoint, string format, string category, string brand, SpecialOffer specialOffer, string apiKey)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(endPoint);
            urlBuilder.Append("?");
            urlBuilder.Append($"format={format}");
            if (category != null)
                urlBuilder.Append($"&category={UrlEncode(category)}");
            if (brand != null)
                urlBuilder.Append($"&brand={brand}");
            if (specialOffer != SpecialOffer.none)
                urlBuilder.Append($"&specialOffer={specialOffer}");
            urlBuilder.Append($"&apiKey={ApiKey}");
            return urlBuilder.ToString();
        }        

        protected static T DeserializeFromXml<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(xml))
            {
                object result = serializer.Deserialize(textReader);
                return (T)result;
            }
        }

        protected string SendRequest(string url)
        {
            string responseData = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "text/xml";
                webRequest.Method = System.Net.Http.HttpMethod.Get.Method;
                webRequest.Accept = "text/xml";
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
                if (webEx.Response != null)
                {
                    Stream responseStream = webEx.Response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    LastError = reader.ReadToEnd();
                }
                throw new InvalidOperationException("Error occured sending request.", webEx);
            }

            return responseData;
        }

        private string UrlEncode(string s)
        {
            return System.Net.WebUtility.UrlEncode(s);
        }
        
        #endregion
    }

    [XmlRoot("itemsResponse")]
    public class ItemsResponse
    {
        [XmlElement("specialOffer")]
        public string SpecialOffer { get; set; }
        [XmlElement("format")]
        public string Format { get; set; }
        [XmlElement("nextPage")]
        public string NextPage { get; set; }
        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<Item> Items { get; set;  }
    }

    public class Item
    {
        [XmlElement("itemId")]
        public string ItemId { get; set; }
        [XmlElement("parentItemId")]
        public string ParentItemId { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("salePrice")]
        public double SalePrice { get; set; }
        [XmlElement("upc")]
        public string UPC { get; set; }
        [XmlElement("categoryPath")]
        public string CategoryPath { get; set; }
        [XmlElement("shortDescription")]
        public string ShortDescription { get; set; }
        [XmlElement("longDescription")]
        public string LongDescription { get; set; }
        [XmlElement("thumbnailImage")]
        public string ThumbnailImage { get; set; }
        [XmlElement("mediumImage")]
        public string MediumImage { get; set; }
        [XmlElement("largeImage")]
        public string LargeImage { get; set; }
        [XmlElement("productTrackingUrl")]
        public string ProductTrackingUrl { get; set; }
        [XmlElement("ninetySevenCentShipping")]
        public bool NinetySevenCentShipping { get; set; }
        [XmlElement("standardShipRate")]
        public double StandardShipRate { get; set; }
        [XmlElement("twoThreeDayShippingRate")]
        public double TwoThreeDayShippingRate { get; set; }
        [XmlElement("overnightShippingRate")]
        public double OvernightShippingRate { get; set; }
        [XmlElement("color")]
        public string Color { get; set; }
        [XmlElement("marketplace")]
        public bool MarketPlace { get; set; }
        [XmlElement("shipToStore")]
        public bool ShipToStore { get; set; }
        [XmlElement("freeShipToStore")]
        public bool FreeShipToStore { get; set; }
        [XmlElement("productUrl")]
        public string ProductUrl { get; set; }
        [XmlElement("customerRating")]
        public double CustomerRating { get; set; }
        [XmlElement("numReviews")]
        public int NumReviews { get; set; }
        [XmlElement("customerRatingImage")]
        public string CustomerRatingImage { get; set; }
        [XmlElement("bestMarketplacePrice")]
        public BestMarketplacePrice BestMarketplacePrice { get; set; }
        [XmlElement("categoryNode")]
        public string CategoryNode { get; set; }
        [XmlElement("bundle")]
        public bool Bundle { get; set; }
        [XmlElement("clearance")]
        public bool Clearance { get; set; }
        [XmlElement("preOrder")]
        public bool PreOrder { get; set; }
        [XmlElement("stock")]
        public string Stock { get; set; }
        [XmlElement("addToCartUrl")]
        public string AddToCartUrl { get; set; }
        [XmlElement("affiliateAddToCartUrl")]
        public string AffiliateAddToCartUrl { get; set; }
        [XmlElement("freeShippingOver50Dollars")]
        public bool FreeShippingOver50Dollars { get; set; }
        [XmlElement("maxItemsInOrder")]
        public int MaxItemsInOrder { get; set; }
        [XmlElement("availableOnline")]
        public bool AvailableOnline { get; set; }
        [XmlElement("specialOffer")]
        public string SpecialOffer { get; set; }
    }

    public class BestMarketplacePrice
    {
        [XmlElement("price")]
        public double Price { get; set; }
        [XmlElement("sellerInfo")]
        public string SellerInfo { get; set; }
        [XmlElement("standardShipRate")]
        public double StandardShipRate { get; set; }
        [XmlElement("availableOnline")]
        public bool AvailableOnline { get; set; }
        [XmlElement("clearance")]
        public bool Clearance { get; set; }
    } 

    [XmlRoot("searchresponse")]
    public class SearchResponse
    {
        [XmlElement("query")]
        public string Query { get; set; }
        [XmlElement("sort")]
        public string Sort { get; set; }
        [XmlElement("responseGroup")]
        public string ResponseGroup { get; set; }
        [XmlElement("totalResults")]
        public int TotalResults { get; set; }
        [XmlElement("start")]
        public int FirstItemIndex { get; set; }
        [XmlElement("numItems")]
        public int ItemCount { get; set; }
        [XmlElement("facets")]
        public List<string> Facets { get; set; }
        [XmlElement("items")]
        public SearchResult Result { get; set; }
    }

    public class SearchResult
    {
        [XmlElement("item")]
        public List<SearchItem> Items { get; set; }
    }

    public class SearchItem
    {
        [XmlElement("itemId")]
        public string ItemId { get; set; }
        [XmlElement("parentItemId")]
        public string ParentItemId { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("msrp")]
        public double Msrp { get; set; }
        [XmlElement("salePrice")]
        public double SalePrice { get; set; }
        [XmlElement("upc")]
        public string UPC { get; set; }
        [XmlElement("categoryPath")]
        public string CategoryPath { get; set; }
        [XmlElement("shortDescription")]
        public string ShortDescription { get; set; }
        [XmlElement("longDescription")]
        public string LongDescription { get; set; }
        [XmlElement("thumbnailImage")]
        public string ThumbnailImage { get; set; }
        [XmlElement("productTrackingUrl")]
        public string ProductTrackingUrl { get; set; }
        [XmlElement("standardShipRate")]
        public double StandarShipRate { get; set; }
        [XmlElement("marketPlace")]
        public bool MarketPlace { get; set; }
        [XmlElement("modelNumber")]
        public string ModelNumber { get; set; }
        [XmlElement("productUrl")]
        public string ProductUrl { get; set; }
        [XmlElement("customerRating")]
        public string Query { get; set; }
        [XmlElement("categoryNode")]
        public string CategoryNode { get; set; }
        [XmlElement("bundle")]
        public bool Bundle { get; set; }
        [XmlElement("addToCartUrl")]
        public string AddToCartUrl { get; set; }
        [XmlElement("affiliateAddToCartUrl")]
        public string AffiliateAddToCartUrl { get; set; }
        [XmlElement("availableOnline")]
        public bool AvailableOnline { get; set; }
    }   

    public static class WalmartServiceDataFormat
    {
        public const string XML = "xml";
        public const string JSON = "json";
    }

    [XmlRoot("categories")]
    public class CategoriesResponse
    {
        [XmlElement("category")]
        public Category[] Categories { get; set; }
    }
    
    [XmlRoot("category")]
    public class Category
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

        [XmlArray("children")]   
        [XmlArrayItem("category")]   
        public List<Category> Children { get; set; }
    }

    public enum SpecialOffer
    {
        none, 
        clearance,
        rollback,
        specialbuy
    }
}

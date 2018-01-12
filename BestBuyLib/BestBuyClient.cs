using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace CipherPark.BestBuy.Api
{
    public class BestBuyClient
    {
        private long? _lastCallTime = null;

        public string ApiKey { get; set; }       

        public ulong Throttle { get; set; }

        public CategoriesResult GetCategories(IDictionary<string, string> filter = null, int? page = null, int? pageSize = null, string format = BestBuyServiceDataFormat.XML, string[] show = null)
        {
            if (format != BestBuyServiceDataFormat.XML)
                throw new NotSupportedException("Specified format is not supported.");

            string endPoint = "https://api.bestbuy.com/v1/categories";
            string url = BuildUrl(endPoint, null, filter, page, pageSize, null, ApiKey, format, show);
            string responseData = SendRequest(url);
            var result = DeserializeFromXml<CategoriesResult>(responseData);
            return result;            
        }

        private static string BuildUrl(string endPoint, string[] keywords, IDictionary<string, string> filter, int? page, int? pageSize, string[] facet, string apiKey, string format, string[] show)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(endPoint);

            string searchExpression = keywords != null ? CreateSearchExpression(keywords) : null;
            string filterExpression = filter != null ? CreateFilterExpression(filter) : null;
            if (searchExpression != null || filterExpression != null)
            {
                urlBuilder.Append("(");

                if (searchExpression != null)
                    urlBuilder.Append($"({searchExpression})");

                if (searchExpression != null && filterExpression != null)
                    urlBuilder.Append("&&");

                if (filterExpression != null)
                    urlBuilder.Append($"({filterExpression})");

                urlBuilder.Append(")");
            }       

            urlBuilder.Append($"?apiKey={apiKey}");

            if (facet != null)
                urlBuilder.Append($"facet={string.Join(",", facet.Select(f => UrlEncode(f)).ToArray())}");

            if (page != null)
                urlBuilder.Append($"&page={page.Value}");

            if (pageSize != null)
                urlBuilder.Append($"&pageSize={pageSize.Value}");

            urlBuilder.Append($"&format={format}");

            if (show != null)
                urlBuilder.Append($"&show={string.Join(",", show.Select(s => UrlEncode(s)).ToArray())}");

            return urlBuilder.ToString();
        }

        public ProductsResult GetProducts(string[] keywords, IDictionary<string, string> filter = null, int? page = null, int? pageSize = null, string[] facet = null, string format = BestBuyServiceDataFormat.XML, string[] show = null)
        {
            if (format != BestBuyServiceDataFormat.XML)
                throw new NotSupportedException("Specified format is not supported.");
            string endPoint = "https://api.bestbuy.com/v1/products";
            string url = BuildUrl(endPoint, keywords, filter, page, pageSize, facet, ApiKey, format, show);
            string responseData = SendRequest(url);
          ProductsResult result = DeserializeFromXml<ProductsResult>(responseData);
            return result;
        }

        #region Support Methods

        private static string CreateSearchExpression(string[] keywords)
        {
            string[] searchTerms = keywords.Select(k => $"search={UrlEncode(k)}").ToArray();
            return string.Join("&", searchTerms);
        }

        private static string CreateFilterExpression(IDictionary<string, string> filter)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in filter.Keys)
            {
                if (builder.Length > 0)
                    builder.Append("&");               
                builder.Append($"{key}={UrlEncode(filter[key])}");
            }
            return builder.ToString();
        }

        private static T DeserializeFromXml<T>(string xml)
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
            ThrottleCall();

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
                responseStream.Flush();
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

        private static string UrlEncode(string expr)
        {
            return WebUtility.UrlEncode(expr).Replace("+", "%20");
        }

        private void ThrottleCall()
        {
            if (Throttle > 0)
            {
                long waitTime = _lastCallTime == null ? 0 : Math.Max(0, (long)Throttle - (Environment.TickCount - _lastCallTime.Value));
                if (waitTime > 0)
                    System.Threading.Thread.Sleep((int)waitTime);
            }
        }
        #endregion             
    }

    [XmlRoot("categories")]
    public class CategoriesResult
    {
        [XmlAttribute("currentPage")]
        public int CurrentPage { get; set; }

        [XmlAttribute("totalPages")]
        public int TotalPages { get; set; }

        [XmlAttribute("from")]
        public int From { get; set; }

        [XmlAttribute("to")]
        public int To { get; set; }

        [XmlAttribute("total")]
        public int Total { get; set; }

        [XmlAttribute("queryTime")]
        public double QueryTime { get; set; }

        [XmlAttribute("totalTime")]
        public double TotalTime { get; set; }

        [XmlAttribute("canonicalUrl")]
        public string Url { get; set; }

        [XmlElement("category")]
        public Category[] Categories { get; set; }
    }

    [XmlRoot("products")]
    public class ProductsResult
    {
        [XmlAttribute("currentPage")]
        public int CurrentPage { get; set; }

        [XmlAttribute("totalPages")]
        public int TotalPages { get; set; }

        [XmlAttribute("from")]
        public int FromItemIndex { get; set; }

        [XmlAttribute("to")]
        public int ToItemIndex { get; set; }

        [XmlAttribute("totalItems")]
        public int TotalItems { get; set; }

        [XmlAttribute("queryTime")]
        public double QueryTime { get; set; }

        [XmlAttribute("totalTime")]
        public double TotalTime { get; set; }

        [XmlAttribute("canonicalUrl")]
        public string CannonicalUrl { get; set; }

        [XmlAttribute("partial")]
        public bool Partial { get; set; }

        [XmlElement("product")]
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        [XmlElement("active")]
        public bool Active { get; set; }

        [XmlElement("accessories")]
        public AssociatedProducts Accessories { get; set; }

        [XmlElement("addToCartUrl")]
        public string AddToCartUrl { get; set; }

        [XmlElement("bestSellingRank")]
        public string BestSellingRank { get; set; }

        [XmlElement("categoryPath")]
        public CategoryList CategoryPath { get; set; }

        [XmlElement("color")]
        public string Color { get; set; }

        [XmlElement("condition")]
        public string Condition { get; set; }

        [XmlElement("customerReviewAverage")]
        public string CustomerReviewAverage { get; set; }

        [XmlElement("customerReviewCount")]
        public string CustomerReviewCount { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("details")]
        public DetailList DetailList { get; set; }

        [XmlElement("dollarSavings")]
        public double DollarSavings { get; set; }

        [XmlElement("features")]
        public FeatureList FeatureList { get; set; }

        [XmlElement("freeShipping")]
        public bool FreeShipping { get; set; }

        /*
        [XmlElement("frequentlyPurchasedWith")]
        public string Element { get; set; }
        */

        [XmlElement("image")]
        public string ImageUrl { get; set; }

        [XmlElement("includedItemList")]
        public IncludedItemList IncludedItemList { get; set; }

        [XmlElement("inStoreAvailability")]
        public bool InStoreAvailability { get; set; }

        [XmlElement("inStoreAvailabilityText")]
        public string InStoreAvailabilityText { get; set; }

        [XmlElement("longDescription")]
        public string LongDescription { get; set; }

        [XmlElement("manufacturer")]
        public string Manufacturer { get; set; }

        [XmlElement("mobileUrl")]
        public string MobileUrl { get; set; }

        [XmlElement("modelNumber")]
        public string ModelNumber { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("onlineAvailability")]
        public bool OnlineAvailability { get; set; }

        [XmlElement("onlineAvailabilityText")]
        public string OnlineAvailabilityText { get; set; }

        [XmlElement("onSale")]
        public bool OnSale { get; set; }

        [XmlElement("productId")]
        public string ProductId { get; set; }

        [XmlElement("percentSavings")]
        public double PercentSavings { get; set; }

        [XmlElement("preowned")]
        public bool Preowned { get; set; }

        [XmlElement("regularPrice")]
        public double RegularPrice { get; set; }

        [XmlElement("relatedProducts")]
        public AssociatedProducts RelatedProducts { get; set; }

        [XmlElement("salePrice")]
        public double SalePrice { get; set; }

        [XmlElement("shipping")]
        public ShippingPrices ShippingPrices { get; set; }

        [XmlElement("shippingCost")]
        public string ShippingCost { get; set; }

        [XmlElement("shortDescription")]
        public string ShortDescription { get; set; }

        [XmlElement("sku")]
        public string Skus { get; set; }

        [XmlElement("source")]
        public string Source { get; set; }

        [XmlElement("startDate")]
        public DateTime StartDate { get; set; }       

        [XmlElement("thumbnailImage")]
        public string ThumbNailImageUrl { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("upc")]
        public string Upc { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }

    public class Category        
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("active")]
        public bool Active { get; set; }

        [XmlElement("path")]
        public CategoryList Path { get; set; }

        [XmlElement("subcategories")]
        public CategoryList Subcategories { get; set; }
    }

    public class CategoryList
    {
        [XmlElement("category")]
        public Category[] Categories { get; set; }
    }
   
    public class Detail
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }
    }

    public class DetailList
    {
        [XmlElement("detail")]
        public List<Detail> Details { get; set; }
    }

    public class IncludedItemList
    {
        [XmlElement("includedItem")]
        public List<string> IncludedItems { get; set; }
    }

    public class AssociatedProducts
    {
        [XmlElement("sku")]
        public List<string> Sku { get; set; }
    }

    public class ShippingPrices
    {
        [XmlElement("ground")]
        public string Ground { get; set; }

        [XmlElement("secondDay")]
        public string SecondDay { get; set; }

        [XmlElement("nextDay")]
        public string NextDay { get; set; }

        /*
        [XmlElement("vendorDelivery")]
        public string Element { get; set; }
        */
    }

    public class FeatureList
    {
        [XmlElement("feature")]
        public List<string> Features { get; set; }
    }

    public static class BestBuyServiceDataFormat
    {
        public const string XML = "xml";
        public const string JSON = "json";
    }

    public static class BestBuyConstants
    {
        public const int MaxPageSize = 100;
    }
}

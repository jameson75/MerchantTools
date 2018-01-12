using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CipherPark.Alibaba.Api
{
    public class AlibabaClient
    {
        private string _session = null;

        public string AppKey { get; set; }

        public string Version { get; set; }

        public string AppSecretKey { get; set; }

        public object WholesaleGoodsSearch(SeachGoodOptions parameters, string fields = null)
        {
            StringBuilder requestBuilder = new StringBuilder();
            BuildBaseUrl(new AlibabaRequest()
            {             
                Method = "alibaba.wholesale.goods.search",
                Fields = fields,
                ResponseFormat = "xml"              
            }, requestBuilder);
            string url = requestBuilder.ToString();
            string requestJson = SerializeToJson(parameters);
            string responseXml = SendRequest(url, requestJson);
            return responseXml;
        }

        private void BuildBaseUrl(AlibabaRequest request, StringBuilder requestBuilder)
        {
            const string endPoint = "http://api.alibaba.com/router/rest";
            requestBuilder.Append($"{endPoint}?");

            if (AppKey == null)
                throw new InvalidOperationException("AlibabaClient.AppKey was not specified");

            if (Version == null)
                throw new InvalidOperationException("AlibabaClient.Version was not specified");

            if (_session != null)
                throw new IndexOutOfRangeException("Session was not created");

            if (request.Method == null)
                throw new InvalidOperationException("Method was not specified");

            if (request.Fields == null)
                throw new InvalidOperationException("Fields was not specified");

            //Append method parameter.
            requestBuilder.Append($"&method={request.Method}");

            //Append timestamp parameter.
            requestBuilder.Append($"&timestamp={UrlEncode(DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"))}");

            //Append format parameter.
            requestBuilder.Append($"&format={request.ResponseFormat}");

            //Append app_key parameter.
            requestBuilder.Append($"&app_key={AppKey}");

            //Append v parameter.
            requestBuilder.Append($"&v={Version}");

            //Append session parameter.
            requestBuilder.Append($"&session={_session}");

            //Append fields parameter.
            requestBuilder.Append($"&fields={UrlEncode(request.Fields)}");         

            //Append signature.
            AppendSignature(requestBuilder);        
        }

        private void AppendSignature(StringBuilder requestBuilder)
        {
            requestBuilder.Append($"&sign_method=md5");

            if (this.AppSecretKey == null)
                throw new InvalidOperationException("AlibabaClient.AppSecretKey not was not specified.");

            //********************************************************************************************************
            //We generate the signature parameter value using the technique describe by the alibaba guide in the docs.
            //See Guide: https://open.alibaba.com/us/portal/resourceDetail?articleId=102686&categoryId=101731#s3        
            //*******************************************************************************************************

            //Create input for MD5 hash.           
            //We sort the parameter list of the url by alphabetical order,
            //Join the parameter list into a single string,
            //Strip away the '=' sign from the string,
            //Append the app secret to the begining and end of this string.
            string stringToSign = AppSecretKey +
                                   string.Join("", requestBuilder.ToString()
                                  .Split(new[] { '?' })[1]
                                  .Split(new[] { '&' })
                                  .OrderBy(e => string.Join(".", Encoding.Default.GetBytes(e).Select(b => ((int)b).ToString("{000}")).ToArray()))
                                  .ToArray())
                                  .Replace("=", "") +
                                  AppSecretKey;

            //Generate MD5 hash.
            byte[] key = Encoding.Default.GetBytes(this.AppSecretKey);
            MD5 md5Algo = MD5.Create();
            byte[] hash = md5Algo.ComputeHash(Encoding.Default.GetBytes(stringToSign));

            //Convert has to hex string.
            string signature = ToHexString(hash);

            //Append signature parameter.
            requestBuilder.Append($"&sign={signature}");
        }

        private static string ToHexString(byte[] hash)
        {
            StringBuilder hex = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }

        protected string SendRequest(string url, string requestContent)
        {
            System.Console.WriteLine(url);
            System.Console.WriteLine();
            string responseData = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = System.Net.Http.HttpMethod.Get.Method;
                if (requestContent != null)
                {
                    Stream requestStream = webRequest.GetRequestStream();
                    requestStream.Write(System.Text.Encoding.ASCII.GetBytes(requestContent), 0, requestContent.Length);
                    requestStream.Close();
                }
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

        protected static string SerializeToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                serializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
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

        protected static string SerializeToJson<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            string json = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                serializer.WriteObject(memStream, obj);
                StreamReader reader = new StreamReader(memStream);
                json = reader.ReadToEnd();
                reader.Close();
            }
            return json;
        }

        protected static T DeserializeFromJson<T>(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream jsonStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(jsonStream);
            writer.Write(json);
            object result = serializer.ReadObject(jsonStream);
            writer.Close();
            jsonStream.Close();
            return (T)result;
        }

        private static string UrlEncode(string expr)
        {
            return WebUtility.UrlEncode(expr).Replace("+", "%20").Replace("!", "%21");
        }
    } 

    public class AlibabaRequest
    {
        public string Method { get; set; }

        public string ResponseFormat { get; set; }

        public string Fields { get; set; }       
    }

    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
    
    [DataContract]
    public class SeachGoodOptions
    {
        [XmlElement("price_to_cent")]
        [DataMember(Name ="price_to_cent")]
        public long PriceToCent { get; set; }

        [XmlElement("price_from_cent")]
        [DataMember(Name = "price_from_cent")]
        public long PriceFromCent { get; set; }

        [XmlElement("sort_by")]
        [DataMember(Name = "sort_by")]
        public string SortBy { get; set; }

        [XmlElement("page_no")]
        [DataMember(Name = "page_no")]
        public int PageNumber { get; set; }

        [XmlElement("keyword")]
        [DataMember(Name = "keyword")]
        public string Keyword { get; set; }

        [XmlElement("category_id")]
        [DataMember(Name = "category_id")]
        public string CategoryId { get; set; }

        [XmlElement("page_size")]
        [DataMember(Name = "page_size")]
        public int PageSize { get; set; }

        [XmlElement("min_order_from")]
        [DataMember(Name = "min_order_from")]
        public int MinimumOrderFrom { get; set; }

        [XmlElement("min_order_to")]
        [DataMember(Name = "min_order_to")]
        public int MinimumOrderTo { get; set; }

        [XmlElement("product_refine_tag")]
        [DataMember(Name = "product_refine_tags")]
        public string[] ProductRefineTags { get; set; }
    }    
}

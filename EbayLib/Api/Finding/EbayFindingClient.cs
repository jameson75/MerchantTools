using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CipherPark.Ebay.Api.Finding
{
    public sealed class EbayFindingClient : EbayClientBase
    {
        public string Version { get; set; }

        public FindItemsByKeywordsResponse FindItemsByKeywords(FindItemsByKeywordsRequest request, EbayServiceEnvironment env = EbayServiceEnvironment.Production)
        {
            return SendRequest<FindItemsByKeywordsResponse, FindItemsByKeywordsRequest>(request, env);
        }

        public FindItemsByCategoryResponse FindItemsByCategory(FindItemsByCategoryRequest request, EbayServiceEnvironment env = EbayServiceEnvironment.Production)
        {
            return SendRequest<FindItemsByCategoryResponse, FindItemsByCategoryRequest>(request, env);
        }

        private TResponse SendRequest<TResponse, TRequest>(TRequest request, EbayServiceEnvironment env) where TRequest : IEbayRequest
        {
            string requestXml = SerializeToXml(request);
            string responseXml = SendRequest(request.OperationName, requestXml, env);
            return DeserializeFromXml<TResponse>(responseXml);
        }

        private string SendRequest(string operationName, string requestXml, EbayServiceEnvironment env)
        {
            string responseData = null;
            string endPoint = null;
            string dataFormat = EbayServiceDataFormat.XML;

            if (env == EbayServiceEnvironment.Production)
                endPoint = "http://svcs.ebay.com/services/search/FindingService/v1";
            else if (env == EbayServiceEnvironment.Sandbox)
                endPoint = "http://svcs.sandbox.ebay.com/services/search/FindingService/v1";
            else
                throw new NotSupportedException($"Specified environment, \"{env}\", is not supported");
            
            try
            {
                if (AppId == null)
                    throw new InvalidOperationException("AppId property is not specified.");
                if (Version == null)
                    throw new InvalidOperationException("Version property not specified");
                string url = endPoint;
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "text/xml";
                webRequest.Method = System.Net.Http.HttpMethod.Post.Method;
                webRequest.Accept = "text/xml";
                webRequest.Headers.Add("X-EBAY-SOA-SERVICE-NAME", "FindingService");
                webRequest.Headers.Add("X-EBAY-SOA-OPERATION-NAME", operationName);
                webRequest.Headers.Add("X-EBAY-SOA-SERVICE-VERSION", Version);
                webRequest.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", AppId);
                webRequest.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                webRequest.Headers.Add("X-EBAY-SOA-REQUEST-DATA-FORMAT", dataFormat);
                if (requestXml != null)
                {
                    Stream requestStream = webRequest.GetRequestStream();
                    requestStream.Write(System.Text.Encoding.ASCII.GetBytes(requestXml), 0, requestXml.Length);
                    requestStream.Close();
                }
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.Default))
                    {
                        responseData = reader.ReadToEnd();
                    }
                }
            }

            catch (WebException webEx)
            {
                using (Stream responseStream = webEx.Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.Default))
                    {
                        responseData = reader.ReadToEnd();
                    }
                }
            }

            return responseData;
        }
    }
}

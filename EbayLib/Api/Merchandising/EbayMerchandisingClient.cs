using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CipherPark.Ebay.Api.Merchandising
{
    public class EbayMerchandisingClient : EbayClientBase
    {
        public string Version { get; set; }

        public GetMostWatchedItemsResponse GetMostWatchedItems(GetMostWatchedItemsRequest request)
        {
            return SendRequest<GetMostWatchedItemsResponse, GetMostWatchedItemsRequest>(request, EbayServiceEnvironment.Production);
        }

        public GetSimilarItemsResponse GetSimilarItems(GetSimilarItemsRequest request)
        {
            return SendRequest<GetSimilarItemsResponse, GetSimilarItemsRequest>(request, EbayServiceEnvironment.Production);
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
                endPoint = "http://svcs.ebay.com/MerchandisingService";
            else if (env == EbayServiceEnvironment.Sandbox)
                endPoint = "http://svcs.sandbox.ebay.com/MerchandisingService";
            else
                throw new NotSupportedException($"Specified environment, \"{env}\", is not supported");

            try
            {
                if (AppId == null)
                    throw new InvalidOperationException("AppId property is not specified.");

                string url = endPoint;
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "text/xml";
                webRequest.Method = System.Net.Http.HttpMethod.Post.Method;
                //webRequest.Accept = "text/xml"; //commented out due to apparent bug in ebay's Merchandising web service - returns 406 when specified.              
                webRequest.Headers.Add("X-EBAY-SOA-SERVICE-NAME", "MerchandisingService");
                webRequest.Headers.Add("X-EBAY-SOA-OPERATION-NAME", operationName);
                if(Version != null)
                    webRequest.Headers.Add("X-EBAY-SOA-SERVICE-VERSION", Version);
                webRequest.Headers.Add("EBAY-SOA-CONSUMER-ID", AppId);
                webRequest.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                webRequest.Headers.Add("X-EBAY-SOA-REQUEST-DATA-FORMAT", dataFormat);
                if (requestXml != null)
                {
                    Stream requestStream = webRequest.GetRequestStream();
                    requestStream.Write(System.Text.Encoding.ASCII.GetBytes(requestXml), 0, requestXml.Length);
                    requestStream.Close();
                }
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponseWithRetry();
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
    }
}

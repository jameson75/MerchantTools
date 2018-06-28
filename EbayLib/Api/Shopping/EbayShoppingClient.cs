using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CipherPark.Ebay.Api.Shopping;

namespace CipherPark.Ebay.Api.Shopping
{
    public class EbayShoppingClient : EbayClientBase
    {
        public string Version { get; set; }

        public GetMultipleItemsResponse GetMultipleItems(GetMultipleItemsRequest request, EbayServiceEnvironment env = EbayServiceEnvironment.Production)
        {
            return SendRequest<GetMultipleItemsResponse, GetMultipleItemsRequest>(request, env); 
        }

        private TResponse SendRequest<TResponse, TRequest>(TRequest request, EbayServiceEnvironment env) where TRequest : IEbayRequest
        {
            string requestXml = SerializeToXml(request);
            string responseXml = SendRequest(request.OperationName, requestXml, env, EbayServiceDataFormat.XML);
            return DeserializeFromXml<TResponse>(responseXml);
        }

        private string SendRequest(string operationName, string requestXml, EbayServiceEnvironment env, string dataFormat)
        {
            string responseData = null;
            string endPoint = null;
            if (env == EbayServiceEnvironment.Production)
                endPoint = "http://open.api.ebay.com/shopping";
            else if (env == EbayServiceEnvironment.Sandbox)
                endPoint = "http://open.api.sandbox.ebay.com/shopping";
            else
                throw new NotSupportedException($"Specified environment, \"{env}\", is not supported");

            try
            {
                if (AppId == null)
                    throw new InvalidOperationException("AppId property is not specified.");

                if (Version == null)
                    throw new InvalidOperationException("Version property is not specified.");

                string url = endPoint;
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.ContentType = "text/xml";
                webRequest.Method = System.Net.Http.HttpMethod.Post.Method;
                webRequest.Accept = "text/xml";
                webRequest.Headers.Add("X-EBAY-API-APP-ID", AppId);
                webRequest.Headers.Add("X-EBAY-API-CALL-NAME", operationName);
                webRequest.Headers.Add("X-EBAY-API-VERSION", Version);
                webRequest.Headers.Add("X-EBAY-API-SITE-ID", "0");
                webRequest.Headers.Add("X-EBAY-API-REQUEST-ENCODING", dataFormat);
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

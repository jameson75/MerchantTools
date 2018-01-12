using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CipherPark.Ebay.Api.Trading
{
    public class EbayTradingClient : EbayClientBase
    {
        public string DevId { get; set; }

        public string CertId { get; set; }

        public GetCategoriesResponse GetCategories(GetCategoriesRequest request, EbayServiceEnvironment env = EbayServiceEnvironment.Production)
        {
            return SendRequest<GetCategoriesResponse, GetCategoriesRequest>(request, env);
        }

        public GetItemTransactionsResponse GetItemTransactions(GetItemTransactionsRequest reqeust, EbayServiceEnvironment env = EbayServiceEnvironment.Production)
        {
            return SendRequest<GetItemTransactionsResponse, GetItemTransactionsRequest>(reqeust, env);
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
                endPoint = "https://api.ebay.com/ws/api.dll";
            else if (env == EbayServiceEnvironment.Sandbox)
                endPoint = "https://api.sandbox.ebay.com/ws/api.dll";
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
                webRequest.Accept = "text/xml";
                webRequest.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL", "391");
                webRequest.Headers.Add("X-EBAY-API-DEV-NAME", DevId);             
                webRequest.Headers.Add("X-EBAY-API-APP-NAME", AppId);
                webRequest.Headers.Add("X-EBAY-API-CERT-NAME", CertId);
                webRequest.Headers.Add("X-EBAY-API-CALL-NAME", operationName);
                webRequest.Headers.Add("X-EBAY-API-SITEID", "0");
                if (requestXml != null)
                {
                    Stream requestStream = webRequest.GetRequestStream();
                    requestStream.Write(System.Text.Encoding.ASCII.GetBytes(requestXml), 0, requestXml.Length);
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
    }
}

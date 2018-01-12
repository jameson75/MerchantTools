using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace CipherPark.Ebay.Util
{
    public static class EbayServiceEndpointExtesion 
    {
        public static void AddEbayHTTPHeaders(this ServiceEndpoint endpoint)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("X-EBAY-API-APP-ID", Properties.Settings.Default.EbayAPI_AppId);
            headers.Add("X-EBAY-API-VERSION", Properties.Settings.Default.EbayAPI_Version);
            endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(headers));
        }
    }

    internal class HttpHeaderMessageInspector : IClientMessageInspector
    {
        private readonly Dictionary<string, string> _httpHeaders;

        public HttpHeaderMessageInspector(Dictionary<string, string> httpHeaders)
        {
            this._httpHeaders = httpHeaders;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState) { }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;

            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;

                foreach (var httpHeader in _httpHeaders)
                {
                    httpRequestMessage.Headers[httpHeader.Key] = httpHeader.Value;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();

                foreach (var httpHeader in _httpHeaders)
                {
                    httpRequestMessage.Headers.Add(httpHeader.Key, httpHeader.Value);
                }
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }
            return null;
        }
    }

    internal class HttpHeadersEndpointBehavior : IEndpointBehavior
    {
        private readonly Dictionary<string, string> _httpHeaders;

        public HttpHeadersEndpointBehavior(Dictionary<string, string> httpHeaders)
        {
            this._httpHeaders = httpHeaders;
        }
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            var inspector = new HttpHeaderMessageInspector(this._httpHeaders);

            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }
        public void Validate(ServiceEndpoint endpoint) { }
    }

    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            List<T> nextbatch = new List<T>(batchSize);            
            foreach (T item in source)
            {
                nextbatch.Add(item);
                if (nextbatch.Count == batchSize)
                {
                    yield return nextbatch;
                    nextbatch = new List<T>(batchSize);
                }
            }
            if (nextbatch.Count > 0)
                yield return nextbatch;
        }
    }
}

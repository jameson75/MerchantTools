using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Web.Configuration;

namespace CipherPark.TriggerRed.Web.CoreServices
{
    [ServiceContract(Namespace = "CipherPark.TriggerOrange.WCF")]
    public interface IOrangeWCFService
    {
        [OperationContract]
        void RestartScheduler();        
        [OperationContract]
        void UpdateMarketPlaceCategories(string siteName);
        [OperationContract]
        void UpdateMarketPlaceHotLists(string siteName);
        [OperationContract]
        void UpdateReportAnalytics(long reportId);
    }

    public class OrangeWCFClient
    {
        private IOrangeWCFService _contract = null;     

        public void Open()
        {
            string address = WebConfigurationManager.AppSettings.Get("TriggerOrangeWCFEndpointAddress");
            var binding = new NetNamedPipeBinding();
            var endpoint = new EndpointAddress(address);
            var channelFactory = new ChannelFactory<IOrangeWCFService>(binding, endpoint);            
            try
            {
                _contract = channelFactory.CreateChannel();                
            }
            catch
            {
                if (_contract != null)                
                    ((ICommunicationObject)_contract).Abort();                
            }
        }

        public void Close()
        {
            if (_contract != null)
            {
                ((ICommunicationObject)_contract).Close();
                _contract = null;
            }
        }

        public IOrangeWCFService Contract {  get { return _contract; } }
    }
}
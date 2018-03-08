using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;
using CipherPark.TriggerOrange.Core;

namespace CipherPark.TriggerOrange.Service
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
        //[OperationContract]
        //string GetLogFilePath();
        [OperationContract]
        void UpdateReportAnalytics(long reportId);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class OrangeWCFService : IOrangeWCFService
    {
        private OrangeTaskManager taskManager = new OrangeTaskManager();

        #region WCF service implementation

        public void RestartScheduler()
        {
            taskManager.RestartScheduler();
        }              

        public void UpdateMarketPlaceCategories(string siteName)
        {
            taskManager.UpdateMarketPlaceCategories(siteName);
        }

        public void UpdateMarketPlaceHotLists(string siteName)
        {
            taskManager.UpdateMarketPlaceHotLists(siteName);            
        }      
        
        public void UpdateReportAnalytics(long reportId)
        {
            taskManager.UpdateReportAnalytics(reportId);
        }

        #endregion

        public void Start()
        {
            taskManager.Start();            
        }
                
        public void Stop()
        {            
            taskManager.Stop();                 
        }
    }
}

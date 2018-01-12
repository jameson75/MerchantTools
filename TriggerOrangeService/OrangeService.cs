using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CipherPark.TriggerOrange.Core.ApplicationServices;

namespace CipherPark.TriggerOrange.Service
{    
    public partial class OrangeService : ServiceBase
    {
        public const string _ServiceName = "TriggerRedService";
        public const string _ServiceDisplayName = "Trigger Red Service";

        private ServiceHost _serviceHost = null;
        private OrangeWCFService _orangeWCFService = null;

        public OrangeService()
        {
            InitializeComponent();
            InitializeLogging();
        }

        protected override void OnStart(string[] args)
        {
            if(_serviceHost != null)
            {
                _orangeWCFService.Stop();
                _serviceHost.Close();
                _orangeWCFService = null;
                _serviceHost = null;
            }

            _orangeWCFService = new OrangeWCFService();
            _orangeWCFService.Start();
            _serviceHost = new ServiceHost(_orangeWCFService);           
            _serviceHost.Open();     
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _orangeWCFService.Stop();
                _serviceHost.Close();
                _orangeWCFService = null;
                _serviceHost = null;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>        
        public static void Main()
        {

#if !DEBUG_WITH_CONSOLE             
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new OrangeService()
                };
                ServiceBase.Run(ServicesToRun);
#else
                var s = new OrangeService();
                Console.WriteLine("Starting service.");                
                s.OnStart(null);
                Console.WriteLine("Press any key to stop service.");
                Console.ReadLine();
                s.OnStop();            
#endif
        }

        private void InitializeLogging()
        {
            //Initialize Logging
            OrangeCoreDiagnostics.Logger = new DefaultLogger();
            ((DefaultLogger)OrangeCoreDiagnostics.Logger).EnableFileLogging(true, LogFileLocation.ExecutableDirectory);
            OrangeCoreDiagnostics.VerboseApiOperations = true;
            OrangeCoreDiagnostics.VerboseTaskManagement = true;
        }
    }
}

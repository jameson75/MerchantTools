using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherPark.TriggerOrange.Core.ApplicationServices;
using CipherPark.TriggerOrange.Core.Data;

namespace CipherPark.TriggerOrange.Core.ApplicationServices
{
    public static class OrangeCoreDiagnostics
    {
        public static ILogger Logger { get; set; }
        public static bool VerboseApiOperations { get; set; }   
        public static bool VerboseTaskManagement { get; set; }

        internal static void LogPullingCategories(string siteID)
        {
            if (Logger != null && VerboseApiOperations)
                Logger.LogInfo($"Downloading categories from {siteID}");
        }

        internal static void LogSavingCategories(string siteID)
        {
            if (Logger != null && VerboseApiOperations)
            {
                Logger.LogInfo($"Saving {siteID} categories to database...");
                using (OrangeEntities db = new OrangeEntities())
                {
                    if (db.Categories.Any(c => c.Site == siteID))
                        Logger.LogInfo($"(WARNING Existing categories for {siteID} will be overwritten)");
                }
            }           
        }

        internal static void LogOperationInfo(string operationName, string message)
        {
            if (Logger != null && VerboseApiOperations)
                Logger.LogInfo($"OPERATION: {operationName}. {message}");
        }      

        internal static void LogOperationWarning(string operationName, string message)
        {
            if (Logger != null && VerboseApiOperations)
                Logger.LogWarning($"OPERATION: {operationName}. {message}");
        }

        internal static void LogPullingProducts(string siteID)
        {
            if (Logger != null && VerboseApiOperations)
                Logger.LogInfo($"Pulling products from {siteID}...");
        }

        internal static void LogSavingProducts(string siteID)
        {
            if (Logger != null && VerboseApiOperations)
                Logger.LogInfo($"Saving {siteID} products to database...");
        }

        internal static void LogPullingListings(string siteID, Product product)
        {
            if (Logger != null && VerboseApiOperations)
                Logger.LogInfo($"Pulling listings from {siteID} for product {product.Name} (located at {product.Category.Site})...");
        }

        internal static void LogCreatingComparisonInfo(string siteID, Product product)
        {
            if (Logger != null && VerboseApiOperations)            
                Logger.LogInfo("Listing info found. Creating comparison info and storing it to database...");             
        }

        internal static void LogTaskException(QueuedTask qt, Exception ex)
        {
            if (Logger != null && VerboseTaskManagement)
            {
                Logger.LogError($"TASK: A fatal error occured during task {qt.Name} ({qt.TaskId})");
                Logger.LogException(ex);
            }
        }

        internal static void LogQueueingTask(QueuedTask qt)
        {
            if (Logger != null && VerboseTaskManagement)            
                Logger.LogInfo($"TASK: Queueing task {qt.Name} ({qt.TaskId})");            
        }

        internal static void LogTaskExecuting(QueuedTask qt)
        {
            if (Logger != null && VerboseTaskManagement)
                Logger.LogInfo($"TASK: Executing task {qt.Name} ({qt.TaskId})");
        }

        internal static void LogTaskComplete(QueuedTask qt)
        {
            if(Logger != null && VerboseTaskManagement)
                Logger.LogInfo($"TASK: Task {qt.Name} ({qt.TaskId}) Completed successfully.");
        }

        internal static void LogRequestPageComplete(string siteID, int? currentPage = null, int? totalPages = null)
        {
            if (Logger != null && VerboseApiOperations)
            {
                StringBuilder sb = new StringBuilder();
                if (currentPage != null)
                {
                    sb.Append($" {currentPage}");
                    if (totalPages != null)
                        sb.Append($" of {totalPages}");
                }

                Logger.LogInfo($"{siteID} request for page{sb} complete");
            }
        }

        internal static void LogOperationError(string operationName, Exception ex = null, string message = null)
        {
            if (Logger != null && VerboseApiOperations)
            {
                if (message != null)
                    Logger.LogError(message);

                if (ex != null)              
                    Logger.LogException(ex);
            }
        }
    }
}

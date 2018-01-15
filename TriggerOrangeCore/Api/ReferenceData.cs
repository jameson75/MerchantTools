using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.TriggerOrange.Core
{
    public static class MarketPlaceSiteNames
    {
        public const string eBayHotStarters = "eBay Hot Starters";
        public const string eBayMostWatched = "eBay Most Watched";
        public const string Amazon = "Amazon";
        public static string[] All = new string[] { eBayMostWatched, Amazon, eBayHotStarters };
        public static string[] AllSupported = new string[] { eBayMostWatched, eBayHotStarters };
    }

    public static class SourcingSiteNames
    {
        public const string eBay = "eBay";
        public const string DHGate = "DHGate";
        public const string Alibaba = "Alibaba";
        public static string[] All = new string[] { Alibaba, DHGate, eBay };
        public static string[] AllSupported = new string[] { Alibaba, DHGate };
    }

    public static class OperationNames
    {
        public const string UpdateEbayMostWatchedCategories = "Update Ebay Most Watched Categories";
        public const string UpdateEbayHotStartersCategories = "Update Ebay Hot Starters Categories";
        public const string UpdateAmazonCategories = "Update Amazon Categories";
        public const string UpdateEbayMostWatchedLists = "Update Ebay Most Watched Lists";
        public const string UpdateEbayHotStarters = "Update Ebay Hot And New";
        public const string UpdateAmazonHotLists = "Update Amazon Hot Lists";
        public const string UpdateAlibabaSourcingInfo = "Update Alibaba Sourcing Info";
        public const string UpdateWalmartCategories = "Update Walmart Categories";
        public const string UpdateBestBuyCategories = "Update Best Buy Categories";
        public const string UpdateAlibabaCategories = "Update Alibaba Categories";
    }

    public static class QueryResultType
    {
        public const string Category = "Category";
        public const string Product = "Product";
    }

    public static class LongRunningTaskNames
    {
        public const string UpdateMarketPlaceCategories = "Categories Update";
        public const string UpdateMarketPlaceHotLists = "Hot List Update";
        public static string[] All
        {
            get
            {
                return new[] {
                    UpdateMarketPlaceCategories,
                    UpdateMarketPlaceHotLists
                };
            }
        }

        internal static string Generate(string taskName, string siteName)
        {
            return $"{siteName} {taskName}";
        }
    }

    public static class LongRunningTaskStatus
    {
        public const string Pending = "Pending";
        public const string Processing = "Processing";
        public const string Complete = "Complete";
        public const string Error = "Error";
        public const string Canceled = "Canceled";
        public static bool IsFinished(string status)
        {
            return status == Complete || status == Error || status == Canceled;
        }
    }

    public static class LongRunningTaskMessageType
    {
        public const string Info = "Info";
        public const string Error = "Error";
        public const string Warning = "Warning";
    }

    public static class SearchSortKey
    {
        public const string WatchCount = "Watch Count";
        public const string UnitsSold = "Units Sold";
        public const string PriceHighToLow = "Price High->Low";
        public const string PriceLowToHigh = "Price Low->High";
        public const string SellerScore = "Seller Score";
        public static string[] All { get { return new[] { UnitsSold, SellerScore, PriceHighToLow, PriceLowToHigh }; } }
    }
}

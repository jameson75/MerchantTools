using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Core;
using EntityFramework.BulkInsert.Extensions;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerOrange.Core.ApplicationServices;
using CipherPark.Amazon.Api;
using CipherPark.Ebay.Api.Finding;
using CipherPark.Ebay.Api.Trading;
using CipherPark.Ebay.Api.Merchandising;
using CipherPark.Ebay.Api.Shopping;
using CipherPark.Ebay.Util;
using CipherPark.Walmart.Api;
using CipherPark.BestBuy.Api;
using CipherPark.Alibaba.Api;

namespace CipherPark.TriggerOrange.Core
{
    public class OrangeApi
    {
        #region Ebay Keys
        public const string EbayProductionDevId = "ecc2a8ce-2bad-41b5-995b-603ef425a06e";
        public const string EbayProductionAppId = "EugeneAd-aa5b-48be-a0c7-9cb1684a8072";
        public const string EbayProductionCertId = "5090c7e1-7001-4c22-bbdb-0cccef6ac65d";       
        public const string EbayUserTokenMeganova75 = "AgAAAA**AQAAAA**aAAAAA**LsmYVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFmIWlDJiGpAydj6x9nY+seQ**Pv4CAA**AAMAAA**4CqjXTl8CCk2p168EkoNpXWXIOEw8hn6z4aCEF1FFTuWge+MiFyZO5RI8QtzA0/tWvP3oWiOHvk3Lw/TtQw1+2btjJ9+3OKHwc0g6/0TQwkcv4dI6DBhwHOlhzZ7WtBuqLZjCtXS8UN3MIqQF+IePOjVg/dXqHIWzph2vbvAlotAYLlZPYFW4MUCWJDLcpHxXFsS8DPd7UxaKM0Ov86E30iYa1b8Fa4x3CyMgi2xCuMVYITxurv7+ykbBByrgynq3Wo6uQKLKB2j/RDv4f7SOj3CKDKd9CrbjdSskkvR+0CBCTXWR4Ml3nArGUI8H/5JyV582xTGOfXwm3Z5fI6+S90kVYVdstdr7fYrTO8Lw3YFZcgogYjEslIepjgc/p//DhQXCud4xaj0Nd03sS2iMU9xkFiIpQlx0QYztFsqTSARncobXsZRAypaOpRgF51jMcbXmRvdYPyLf35k5EFmiw+GjFgrdOVITNTFVpeiDyljTt7cUU2QJ044jICBV0AaVxqB+Zc6I9McmrVWKdTcE0ml8oIjzkOuy1q6dXqPLcHuepoPXQZndmUCCRfDmYVKzvWQoZO0dU/DrWAxLqDkl2xrT0mpXUDFOkxH5X6UZ3xMd1cDC8ddO4k8/MtRgQOAPzyvChnttwzBaV4yV0jeKbeUAJGXGlcIlT2RTGZ6JNgFm0cOOi15SmNiRjaIVDE9j1GKI+nLA0aStgweUtAKOkQAkgobLElBQWYu37EMh4cTdLRlsNzMK1n3KNIHTgra";      

        public static readonly DateTime EbayUserTokenExpiryMeganova75 = DateTime.Parse("2017-07-08 10:25:50");

        public const string EbayFindingClientVersion = "1.13.0";
        public const string EbayTradingClientVersion = "949";
        public const string EbayShoppingApiVersion = "949";
        public const string EbayMerchandisingVersion = "1.5.0";
        #endregion

        #region Walmart
        public const string WalmartApiKey = "aeaawfvfpveu89crf2akwx5a";
        #endregion

        #region BestBuy
        public const string BestBuyApiKey = "pnjfncrk9e32ade24strmdqa";
        #endregion     

        #region Amazon Keys
        public const string AWSAccessKeyID = "AKIAJWJA6KN2R3UVP7DQ";
        public const string AWSSecretAccessKey = "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";
        public const string AWSAccountID = "0192-2398-0786";
        public const string AmazonCanonicalUserID = "1b476234139c07390a2d79ff7055b37c37ed1ccc6b42f87716daecabfcca0b53";
        public const string AmazonAssociateTag = "trigoran-20";
        #endregion
 
        private const long BestBuyCallThrottle = 200;
        private const long AmazonCallThrottle = 3000;

        /// <summary>
        /// Pulls all the categories from the specified supplier's API and populates the database.
        /// </summary>
        /// <param name="siteName"></param>
        /// <remarks>This call deletes/removes all previous products and categories, for the given supplier, from the database.</remarks>
        public void UpateMarketPlaceCategories(string siteName)
        {
            switch (siteName)
            {
                case MarketPlaceSiteNames.eBayMostWatched:

                    LogOperationStart(OperationNames.UpdateEbayMostWatchedCategories);                

                    EbayTradingClient tradingClient = new EbayTradingClient()
                    {
                        AppId = EbayProductionAppId,
                        DevId = EbayProductionDevId,
                        CertId = EbayProductionCertId
                    };
                    
                    //Pull ALL categories from Ebay.
                    OrangeCoreDiagnostics.LogPullingCategories(siteName);
                    var categoriesResponse = tradingClient.GetCategories(new GetCategoriesRequest()
                    {                      
                        RequesterCredentials = new RequesterCredentials()
                        {
                            EBayAuthToken = EbayUserTokenMeganova75
                        },
                        LevelLimit = 2,
                        DetailLevel = new[] { DetailLevelCodeType.ReturnAll }
                    });
                    
                    using (OrangeEntities db = new OrangeEntities())
                    {
                        //Updating categories requires removal of all dependants entities.
                        //Overwrite any categories that already exist for the current supplier.
                        LogOperationInfo(OperationNames.UpdateEbayMostWatchedCategories, "Deleting saved categories and dependencies");

                        BulkDeleteCategoriesForSite(MarketPlaceSiteNames.eBayMostWatched);                        

                        OrangeCoreDiagnostics.LogSavingCategories(siteName);
                        List<Core.Data.Category> dbCategories = new List<Core.Data.Category>();
                        foreach (var category in categoriesResponse.CategoryArray)
                        {
                            dbCategories.Add(new Core.Data.Category()
                            {
                                Name = category.CategoryName,
                                ReferenceId = category.CategoryID,
                                Path = category.FindPath(categoriesResponse.CategoryArray),
                                PathLevel = Convert.ToByte(category.CategoryLevel - 1),
                                ParentReferenceId = category.CategoryLevel > 1 ? category.CategoryParentIDs.Last() : null,
                                Site = siteName
                            });
                        }
                        db.Categories.AddRange(dbCategories);
                        db.SaveChanges();
                    }

                    LogOperationComplete(OperationNames.UpdateEbayMostWatchedCategories);
                    break;

                case MarketPlaceSiteNames.eBayHotStarters:
                    LogOperationStart(OperationNames.UpdateEbayHotStartersCategories);

                    //Updating categories requires removal of all dependants entities.
                    //Overwrite any categories that already exist for the current supplier.
                    LogOperationInfo(OperationNames.UpdateEbayHotStartersCategories, "Deleting saved categories and dependencies");
                    BulkDeleteCategoriesForSite(MarketPlaceSiteNames.eBayHotStarters);

                    using (var db = new OrangeEntities())
                    {
                        string[] categoryReferenceIds = new string[]
                        {                            
                            "15032",    //Cell phone accessories.
                            "58058",    //Computers,
                            "293",      //Consumer Electronics
                            "888",      //Sporting Goods,
                            "220",      //Toys and Hobbies.
                            "11450",    //Clothing, Shoes & Accessories
                            "14339",    //Crafts
                            "26395",    //Health and beauty.
                        };
                        var sourceCategories = db.Categories.Where(x => categoryReferenceIds.Contains(x.ReferenceId)).ToList();
                        db.Categories.AddRange(sourceCategories.Select(x => new Data.Category()
                        {
                            Name = x.Name,
                            ParentReferenceId = x.ReferenceId, //For hot starter categories, we, purposely, make the parent equal self.
                            ReferenceId = x.ReferenceId,
                            Path = x.Path,
                            PathLevel = x.PathLevel,
                            Site = MarketPlaceSiteNames.eBayHotStarters
                        }));
                        db.SaveChanges();
                    }
                    LogOperationComplete(OperationNames.UpdateEbayHotStartersCategories);
                    break;

                case MarketPlaceSiteNames.Amazon:                   

                    LogOperationStart(OperationNames.UpdateAmazonCategories);

                    var rootBrowseNodes = LocaleInformationLookup.US.Where(l => l.BrowseNodeId != null);
                    using (OrangeEntities db = new OrangeEntities())
                    {
                        //Updating categories requires removal of all dependants entities.
                        //Overwrite any categories that already exist for the current supplier.
                        LogOperationInfo(OperationNames.UpdateAmazonCategories, "Deleting saved categories and dependencies");

                        BulkDeleteCategoriesForSite(MarketPlaceSiteNames.Amazon);

                        db.Categories.AddRange(rootBrowseNodes
                            .Select(l => new Core.Data.Category()
                            {
                                ReferenceId = l.BrowseNodeId,
                                Name = l.Department,
                                Path = l.Department,
                                PathLevel = 0,
                                Site = siteName
                            }));

                        //Pull ALL categories from amazon.
                        OrangeCoreDiagnostics.LogPullingCategories(siteName);
                        AmazonClient client = new AmazonClient() { AWSSecretAccessKey = AWSSecretAccessKey, Throttle = AmazonCallThrottle };
                        foreach (var rootBrowseNode in rootBrowseNodes)
                        {
                            var browseNodeResults = client.BrowseNodeLookup(new BrowseNodeLookupRequest()
                            {
                                AssociateTag = AmazonAssociateTag,
                                AWSAccessKeyId = AWSAccessKeyID,
                                ResponseGroups = new string[]
                                {
                                    "BrowseNodeInfo"
                                },
                                BrowseNodeId = rootBrowseNode.BrowseNodeId
                            });

                            if (browseNodeResults.BrowseNodes?.BrowseNodeCollection != null)
                            {
                                foreach (var browseNode in browseNodeResults.BrowseNodes.BrowseNodeCollection)
                                {
                                    db.Categories.AddRange(browseNode.Children.Select(n => new Core.Data.Category()
                                    {
                                        ReferenceId = n.BrowseNodeId,
                                        Name = n.Name,
                                        Path = $"{rootBrowseNode.Department}/{n.Name}",
                                        PathLevel = 1,
                                        Site = siteName,
                                        ParentReferenceId = rootBrowseNode.BrowseNodeId
                                    }));
                                }
                            }
                        }

                        OrangeCoreDiagnostics.LogSavingCategories(siteName);
                        db.SaveChanges();
                    }

                    LogOperationComplete(OperationNames.UpdateAmazonCategories);
                    break;

                default:
                    throw new InvalidOperationException("Unrecognized market place specified.");
            }
        }

        public void UpdateMarketPlaceHotLists(string siteName)
        {
            EbayFindingClient findingClient = new EbayFindingClient()
            {
                AppId = EbayProductionAppId,
                Version = EbayFindingClientVersion
            };

            EbayTradingClient tradingClient = new EbayTradingClient()
            {
                AppId = EbayProductionAppId,
                DevId = EbayProductionDevId,
                CertId = EbayProductionCertId
            };

            EbayMerchandisingClient merchandisingClient = new EbayMerchandisingClient()
            {
                AppId = EbayProductionAppId,
                Version = EbayMerchandisingVersion
            };

            EbayShoppingClient shoppingClient = new EbayShoppingClient()
            {
                AppId = EbayProductionAppId,
                Version = EbayShoppingApiVersion
            };

            switch (siteName)
            {
                case MarketPlaceSiteNames.eBayMostWatched:
                    #region Ebay

                    //Report operation start.
                    string operationName = OperationNames.UpdateEbayMostWatchedLists;
                    LogOperationStart(operationName);                   

                    using (OrangeEntities db = new OrangeEntities())
                    {                      
                        BulkDeleteProductsForSite(MarketPlaceSiteNames.eBayMostWatched);

                        var categories = db.Categories.Where(c => c.Site == MarketPlaceSiteNames.eBayMostWatched && c.PathLevel == 1).ToArray();
                       
                        var categoriesResponse = tradingClient.GetCategories(new GetCategoriesRequest()
                        {
                            CategoryParents = db.Categories.Where(c => c.Site == MarketPlaceSiteNames.eBayMostWatched && c.PathLevel == 0)
                                                           .Select( c => c.ReferenceId)
                                                           .ToArray(),
                            RequesterCredentials = new RequesterCredentials()
                            {
                                EBayAuthToken = EbayUserTokenMeganova75
                            },
                            LevelLimit = 3,
                            DetailLevel = new[] { DetailLevelCodeType.ReturnAll }
                        });

                        //NOTE: Our category level have 0-based indexes while eBay's have 1-based indexes.
                        var allLevel2Categories = categoriesResponse.CategoryArray.Where(cr => cr.CategoryLevel == 3).ToList();

                        foreach (var cat in categories)
                        {
                            LogOperationInfo(operationName, $"Processing category {cat.Name}({cat.ReferenceId})");
                            
                            //If the current level 1 category has level 2 children, we'll request the most watched items of the level 2
                            //children. Otherwise, we'll request the most watched items of the level 1 category.
                            dynamic catInfo = null;
                            var categoryArrayLevel2 = allLevel2Categories.Where(cr => cr.CategoryParentIDs.Last() == cat.ReferenceId).ToList();
                            if (categoryArrayLevel2.Count > 0)
                                catInfo = categoryArrayLevel2.Select(cr => new { CategoryID = cr.CategoryID, CategoryName = cr.CategoryName, PathLevel = 2 }).ToList();
                            else
                                catInfo = new[] { new { CategoryID = cat.ReferenceId, CategoryName = cat.Name, PathLevel = 1 } }.ToList();

                            foreach (var catDetail in catInfo)
                            {
                                LogOperationInfo(operationName, $"Processing category Level {catDetail.PathLevel} {catDetail.CategoryName}({catDetail.CategoryID})");

                                List<Data.Product> dbAllCategoryProducts = new List<Data.Product>();

                                LogOperationInfo(operationName, "Pulling most watched items.");
                                var mostWatchedItems = merchandisingClient.GetMostWatchedItems(new GetMostWatchedItemsRequest()
                                {
                                    MaxResults = GetMostWatchedItemsRequest.MaxResultsUpperLimit,
                                    CategoryId = catDetail.CategoryID
                                });

                                if (mostWatchedItems.ItemRecommendations.Items == null)
                                    continue;

                                LogOperationInfo(operationName, "Pulling details for each most watched item");
                                foreach (var itemsBatch in mostWatchedItems.ItemRecommendations.Items.Batch(GetMultipleItemsRequest.MaxItemsLength))
                                {
                                    var mostWatchedItemDetails = shoppingClient.GetMultipleItems(new GetMultipleItemsRequest()
                                    {
                                        ItemIDs = itemsBatch.Select(i => i.ItemId).ToArray(),
                                        IncludeSelector = "Details"
                                    });

                                    if (mostWatchedItemDetails.Items != null)
                                    {
                                        var products = mostWatchedItemDetails.Items.Where(item => item.PictureURL != null)
                                                                                 .Select(item => new Core.Data.Product()
                                                                                 {
                                                                                     CategoryId = cat.Id,
                                                                                     Price = Convert.ToDecimal(item.ConvertedCurrentPrice),
                                                                                     SmallImageUrl = item.PictureURL?[0],
                                                                                     LargeImageUrl = item.PictureURL?[0],
                                                                                     Name = item.Title,
                                                                                     OnlineAvailability = "Available Online",
                                                                                     ReferenceId = item.ItemID,
                                                                                     ProductUrl = item.ViewItemURLForNaturalSearch,
                                                                                     ShippingCost = mostWatchedItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).ShippingCost.ToString(),
                                                                                     WatchCount = mostWatchedItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).WatchCount,
                                                                                     UnitsSold = item.QuantitySold,
                                                                                     DateCreated = DateTime.Now,
                                                                                     DateModified = DateTime.Now,
                                                                                     Keywords = item.Title,                                                                                     
                                                                                     StartTime = item.StartTime,
                                                                                     SellerId = item.Seller?.UserID,
                                                                                     SellerScore = item.Seller?.FeedbackScore,
                                                                                     Location = item.Location,
                                                                                 }).ToList();
                                        dbAllCategoryProducts.AddRange(products);
                                    }
                                }

                                LogOperationInfo(operationName, "Storing most watched product details for current category to database");
                                db.Products.AddRange(dbAllCategoryProducts);
                                db.SaveChanges();
                            }

                            #region Obsolete
                            
                            #endregion
                        }                        
                    }
                    LogOperationComplete(operationName);
                    break;
                #endregion

                case MarketPlaceSiteNames.eBayHotStarters:
                    #region EbayHotAndNew
                    const int HoursInADay = 24;
                    const int LookBackDays = 7;
                    int MinSales = (int)Math.Ceiling(LookBackDays * 0.7);

                    //Report operation start.
                    operationName = OperationNames.UpdateEbayHotStarters;
                    LogOperationStart(operationName);

                    using (OrangeEntities db = new OrangeEntities())
                    {
                        LogOperationInfo(operationName, $"Deleting all \"hot starter\" products");                                          
                        BulkDeleteProductsForSite(MarketPlaceSiteNames.eBayHotStarters);                                                

                        var categories = db.Categories.Where(x => x.Site == siteName).ToList();

                        var now = DateTime.UtcNow;
                        var beginningOfDay = new DateTime(now.Year, now.Month, now.Day);

                        //foreach (var category in categories)
                        Parallel.ForEach(categories, (category) =>
                        {
                            List<Ebay.Api.Shopping.SimpleItem> allItems = new List<Ebay.Api.Shopping.SimpleItem>();
                            for (int i = 0; i < HoursInADay; i++)
                            {
                                int windowFirstHour = i;
                                DateTime startTimeFrom = beginningOfDay.AddDays(-LookBackDays).AddHours(windowFirstHour);
                                DateTime startTimeTo = startTimeFrom.AddHours(1).AddMilliseconds(-1);

                                var findItemsRequest = new FindItemsByCategoryRequest()
                                {
                                    SortOrder = SortOrderType.StartTimeNewest,
                                    ItemFilters = new ItemFilter[]
                                    {
                                new ItemFilter()
                                {
                                    Name = "ListingType",
                                    Values = new string[]
                                    {
                                        "FixedPrice",
                                    }.ToList(),
                                },
                                new ItemFilter()
                                {
                                    Name = "StartTimeFrom",
                                    Values = new string[]
                                    {
                                        startTimeFrom.ToEbayGMTString(),
                                    }.ToList(),
                                },
                                new ItemFilter()
                                {
                                    Name = "StartTimeTo",
                                    Values = new string[]
                                    {
                                        startTimeTo.ToEbayGMTString(),
                                    }.ToList(),
                                },
                                new ItemFilter()
                                {
                                    Name = "HideDuplicateItems",
                                    Values = new string[]
                                    {
                                        "true"
                                    }.ToList(),
                                },
                                new ItemFilter()
                                {
                                    Name = "MinQuantity",
                                    Values = new string[]
                                    {
                                        "5"
                                    }.ToList(),
                                },
                                    }.ToList(),
                                    CategoryId = category.ReferenceId,
                                    PaginationInput = new PaginationInput()
                                    {
                                        EntriesPerPage = 100
                                    }
                                };

                                LogOperationInfo(operationName, $"Finding Fixed price items starting from {startTimeFrom} to {startTimeTo}...");
                                List<Ebay.Api.Finding.SearchItem> allFoundItems = new List<Ebay.Api.Finding.SearchItem>();
                                FindItemsByCategoryResponse findItemsResponse = null;
                                do
                                {
                                    findItemsRequest.PaginationInput.PageNumber++;
                                    findItemsResponse = findingClient.FindItemsByCategory(findItemsRequest);
                                    if (findItemsResponse.SearchResult?.Items != null)
                                    {
                                        allFoundItems.AddRange(findItemsResponse.SearchResult.Items);
                                    }
                                } while (findItemsResponse.PaginationOutput.PageNumber < findItemsResponse.PaginationOutput.TotalPages);
                                LogOperationInfo(operationName, "Item search complete.");

                                var nPurged = allFoundItems.Count(x => x.ListingInfo.StartTime < startTimeFrom || x.ListingInfo.StartTime > startTimeTo);
                                allFoundItems.RemoveAll(x => x.ListingInfo.StartTime < startTimeFrom || x.ListingInfo.StartTime > startTimeTo);
                                LogOperationInfo(operationName, $"Purged {nPurged} items because they fell outside the queried date range");

                                LogOperationInfo(operationName, "Getting full details for each item...");
                                foreach (var batch in allFoundItems.Batch(GetMultipleItemsRequest.MaxItemsLength))
                                {
                                    var multipleItemsResponse = shoppingClient.GetMultipleItems(new GetMultipleItemsRequest()
                                    {
                                        ItemIDs = batch.Select(x => x.ItemId).ToArray(),
                                        IncludeSelector = "Details"
                                    });
                                    if (multipleItemsResponse.Items != null)
                                        allItems.AddRange(multipleItemsResponse.Items);
                                }
                                LogOperationInfo(operationName, $"Finished getting full details for for hour {i + 1} of {HoursInADay}.");
                            }

                            LogOperationInfo(operationName, $"Finished getting full details for all items. Populating database with {allItems.Count} items");

                            List<Data.Product> dbProducts = allItems.Where(x => x.QuantitySold >= MinSales)
                                                                    .Select(x => new Data.Product
                                                                    {
                                                                        CategoryId = category.Id,
                                                                        Price = Convert.ToDecimal(x.ConvertedCurrentPrice),
                                                                        SmallImageUrl = x.PictureURL?[0],
                                                                        LargeImageUrl = x.PictureURL?[0],
                                                                        Name = x.Title,
                                                                        OnlineAvailability = "Available Online",
                                                                        ReferenceId = x.ItemID,
                                                                        ProductUrl = x.ViewItemURLForNaturalSearch,
                                                                        ShippingCost = null, //mostWatchedItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).ShippingCost.ToString(),
                                                                        WatchCount = null, //mostWatchedItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).WatchCount,
                                                                        UnitsSold = x.QuantitySold,
                                                                        DateCreated = DateTime.Now,
                                                                        DateModified = DateTime.Now,
                                                                        Keywords = x.Title,
                                                                        StartTime = x.StartTime,
                                                                        SellerId = x.Seller?.UserID,
                                                                        SellerScore = x.Seller?.FeedbackScore,
                                                                        Location = x.Location,
                                                                    }).ToList();
                            //db.Products.AddRange(dbProducts);
                            //db.SaveChanges();   
                            db.BulkInsert(dbProducts);
                        });
                    }
                    break;
                #endregion

                case MarketPlaceSiteNames.Amazon:
                    #region Amazon   

                    operationName = OperationNames.UpdateAmazonHotLists;
                    LogOperationStart(OperationNames.UpdateAmazonHotLists);
                    AmazonClient client = new AmazonClient() { AWSSecretAccessKey = AWSSecretAccessKey, Throttle = AmazonCallThrottle };

                    using (OrangeEntities db = new OrangeEntities())
                    {                       
                        BulkDeleteProductsForSite(MarketPlaceSiteNames.Amazon);

                        var dbCategories = db.Categories.Where(c => c.Site == MarketPlaceSiteNames.Amazon && c.PathLevel == 1).ToList();
                        foreach (var cat in dbCategories)
                        {
                            LogOperationInfo(operationName, $"Processing category {cat.Name}({cat.ReferenceId})");

                            var browseNodesLevel1Result = client.BrowseNodeLookup(new BrowseNodeLookupRequest()
                            {
                                AssociateTag = AmazonAssociateTag,
                                AWSAccessKeyId = AWSAccessKeyID,
                                ResponseGroups = new string[]
                                {
                                    "BrowseNodeInfo"
                                },
                                BrowseNodeId = cat.ReferenceId
                            });

                            //If the current level 1 category has level 2 children, we'll request the most watched items of the level 2
                            //children. Otherwise, we'll request the most watched items of the level 1 category.
                            dynamic catInfo = null;
                            if (browseNodesLevel1Result.BrowseNodes?.BrowseNodeCollection != null && browseNodesLevel1Result.BrowseNodes.BrowseNodeCollection.SelectMany( bn => bn.Children).Count() > 0 )
                                catInfo = browseNodesLevel1Result.BrowseNodes?.BrowseNodeCollection.SelectMany(bn => bn.Children).Select(bn => new { BrowseNodeId = bn.BrowseNodeId, Name = bn.Name, PathLevel = 2 }).ToList();
                            else
                                catInfo = new[] { new { BrowseNodeId = cat.ReferenceId, Name = cat.Name, PathLevel = 1 } }.ToList();

                            foreach (var catDetail in catInfo)
                            {
                                LogOperationInfo(operationName, $"Processing category Level {catDetail.PathLevel} {catDetail.Name}({catDetail.BrowseNodeId})");

                                List<Data.Product> dbAllCategoryProducts = new List<Data.Product>();

                                var topSellerBrowseNodeResults = client.BrowseNodeLookup(new BrowseNodeLookupRequest()
                                {
                                    AssociateTag = AmazonAssociateTag,
                                    AWSAccessKeyId = AWSAccessKeyID,
                                    ResponseGroups = new string[]
                                    {
                                        "TopSellers"
                                    },
                                    BrowseNodeId = catDetail.BrowseNodeId
                                });

                                var topSellingItemsLookupResult = client.ItemLookup(new ItemLookupRequest()
                                {
                                    AssociateTag = AmazonAssociateTag,
                                    AWSAccessKeyId = AWSAccessKeyID,
                                    IdType = "ASIN",
                                    ItemId = string.Join(",", topSellerBrowseNodeResults.BrowseNodes.BrowseNodeCollection[0].TopSellers.Take(10).Select(ts => ts.ASIN)),
                                    ResponseGroups = new string[]
                                    {
                                        "Large",
                                        "Offers"
                                    }
                                });

                                if (topSellingItemsLookupResult.Items.ItemCollection != null)
                                {
                                    var products = topSellingItemsLookupResult.Items.ItemCollection
                                        .Where(item => item.OfferSummary?.LowestNewPrice != null &&
                                                       item.ItemAttributes?.Title != null &&
                                                       item.DetailPageURL != null)
                                        .Select(item => new Data.Product()
                                        {
                                            CategoryId = cat.Id,
                                            Price = Convert.ToDecimal(item.OfferSummary.LowestNewPrice.Amount / 100),
                                            SmallImageUrl = item.MediumImageUrl(),
                                            LargeImageUrl = item.LargeImageUrl(),
                                            Name = item.ItemAttributes.Title,
                                            OnlineAvailability = "Available Online",
                                            ReferenceId = item.ASIN,
                                            ProductUrl = item.DetailPageURL,
                                            ShippingCost = "",
                                            WatchCount = item.SalesRank,
                                            UnitsSold = 0,
                                            DateCreated = DateTime.Now,
                                            DateModified = DateTime.Now,
                                            Keywords = item.ItemAttributes.Title,                                            
                                            StartTime = null,
                                            SellerId = null,
                                            SellerScore = null,
                                            Location = null,
                                        });
                                    //db.BulkInsert(products);
                                    db.Products.AddRange(products);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    LogOperationComplete(OperationNames.UpdateAmazonHotLists);
                    break;
                #endregion

                default:
                    throw new InvalidOperationException("Unrecognized market place specified.");
            }
        }     
        
        /*
        public void UpdateEbayProductDetailedSalesHistory(long productId)
        {
            string operationName = "UpdateEbayProductSalesData";
            LogOperationInfo(operationName, "Pulling sales data from site");
            List<SalesData> dbSalesData = new List<SalesData>();
            using (var db = new OrangeEntities())
            {
                var product = db.Products.First(p => p.Id == productId);

                //Retreive sales data from ebay.
                LogOperationInfo(operationName, "Retreiving sales data from ebay");
                EbayTradingClient tradingClient = new EbayTradingClient()
                {
                    AppId = EbayProductionAppId,
                    DevId = EbayProductionDevId,
                    CertId = EbayProductionCertId
                };
                var transactions = tradingClient.GetItemTransactions(new GetItemTransactionsRequest()
                {
                    RequesterCredentials = new RequesterCredentials()
                    {
                        EBayAuthToken = EbayUserTokenMeganova75
                    },
                    ItemID = product.ProductId,
                    NumberOfDays = 30,
                    Platform = TransactionPlatformCodeType.eBay
                });

                //Remove old sales data from database.
                LogOperationInfo(operationName, "Removing old sales data from database");
                BulkDeleteSalesDataForProduct(productId);

                //Store retreived sales data to database.
                LogOperationInfo(operationName, "Saving new sales data to database");
                dbSalesData.AddRange(transactions.TransactionArray.Select(t => new SalesData()
                {
                    ProductId = product.Id,
                    SalePrice = Convert.ToDecimal(t.ConvertedTransactionPrice.Value),
                    SaleDate = t.CreatedDate
                }));                
                db.BulkInsert(dbSalesData);
                db.SaveChanges();
            }
        }
        */

        public SourcingResult GetSourcesByKeywords(string site, string keywords, int firstItemIndex)
        {
            int page = 0;
            switch (site)                
            {
                case SourcingSiteNames.Alibaba:
                    AlibabaScraper aliScraper = new AlibabaScraper();                    
                    page = (firstItemIndex / AlibabaScraper.DefaultPageSize) + 1;
                    var aliResult = aliScraper.FindItemsByKeywords(keywords, page);
                    return new SourcingResult()
                    {
                        IsSuccess = aliResult.IsSuccess,
                        TotalPages = aliResult.TotalPages,
                        RefinedKeywords = null,
                        Items = aliResult.Items.Select(i => new SourcingItem()
                        {
                            SourceProductId = i.ProductId,
                            DetailPageUrl = i.DetailPageUrl,
                            ImageUrl = i.ImageUrl,
                            PriceDescription = i.PriceDescription,
                            PriceRangeMin = i.PriceRangeMin,
                            PriceRangeMax = i.PriceRangeMax,
                            Title = i.Title,
                            WholesalerName = null,
                            SourceSite = SourcingSiteNames.Alibaba,
                        }).ToList()             
                    };                    

                case SourcingSiteNames.DHGate:
                    DHGateScraper dhScraper = new DHGateScraper();
                    page = (firstItemIndex / DHGateScraper.DefaultPageSize) + 1;
                    var dhResult = dhScraper.FindItemsByKeywords(keywords, page);
                    return new SourcingResult()
                    {
                        IsSuccess = dhResult.IsSuccess,
                        TotalPages = dhResult.TotalPages,
                        RefinedKeywords = null,
                        Items = dhResult.Items.Select(i => new SourcingItem()
                        {
                            SourceProductId = i.ProductId,
                            DetailPageUrl = i.DetailPageUrl,
                            ImageUrl = i.ImageUrl,
                            PriceDescription = i.PriceDescription,
                            PriceRangeMin = null, //TODO: We'll need to implement and populate this in the scraper.
                            PriceRangeMax = null, //TODO: We'll need to implement and populate this in the scraper.
                            Title = i.Title,
                            WholesalerName = null,
                            SourceSite = SourcingSiteNames.DHGate,
                        }).ToList()
                    };

                default:
                    throw new InvalidOperationException("Unrecognized site");                         
            }
        }

        public IEnumerable<Data.Product> GetEbaySimilarProducts(string ebayItemId)
        {
            string operationName = "FindSimilarItems";
            List<Data.Product> results = new List<Data.Product>();

            EbayMerchandisingClient merchandisingClient = new EbayMerchandisingClient()
            {
                AppId = EbayProductionAppId,
                Version = EbayMerchandisingVersion
            };

            EbayShoppingClient shoppingClient = new EbayShoppingClient()
            {
                AppId = EbayProductionAppId,
                Version = EbayShoppingApiVersion
            };

            LogOperationInfo(operationName, $"Pulling similar items to {ebayItemId}");
            var similarItems = merchandisingClient.GetSimilarItems(new GetSimilarItemsRequest()
            {
                MaxResults = GetSimilarItemsRequest.MaxResultsUpperLimit,
                ItemId = ebayItemId,
                ListingType = Ebay.Api.ListingCodeType.FixedPriceItem,               
            });
            
            if (similarItems.ItemRecommendations.Items != null)
            {
                foreach (var itemsBatch in similarItems.ItemRecommendations.Items.Batch(GetMultipleItemsRequest.MaxItemsLength))
                {
                    var similarItemDetails = shoppingClient.GetMultipleItems(new GetMultipleItemsRequest()
                    {
                        ItemIDs = itemsBatch.Select(i => i.ItemId).ToArray(),
                        IncludeSelector = "Details"                        
                    });

                    if (similarItemDetails.Items != null)
                    {
                        var products = similarItemDetails.Items.Where(item => item.PictureURL != null)
                                                                 .Select(item => new Core.Data.Product()
                                                                 {
                                                                     CategoryId = 0,
                                                                     Price = Convert.ToDecimal(item.ConvertedCurrentPrice),
                                                                     SmallImageUrl = item.PictureURL[0],
                                                                     LargeImageUrl = item.PictureURL[0],
                                                                     Name = item.Title,
                                                                     OnlineAvailability = "Available Online",
                                                                     ReferenceId = item.ItemID,
                                                                     ProductUrl = item.ViewItemURLForNaturalSearch,
                                                                     ShippingCost = similarItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).ShippingCost.ToString(),
                                                                     WatchCount = similarItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).WatchCount,
                                                                     UnitsSold = item.QuantitySold,
                                                                     DateCreated = DateTime.Now,
                                                                     DateModified = DateTime.Now,
                                                                     Keywords = item.Title,
                                                                     StartTime = item.StartTime,
                                                                     SellerScore = item.Seller?.FeedbackScore                                                                     
                                                                 }).ToList();
                        results.AddRange(products);
                    }
                }
            }
            return results;
        }

        public IEnumerable<Data.Product> FullTextProductSearch(int pageSize, int firstItemIndex, string site, string[] searchTerms, string sortKey, ProductSearchFilter filter, bool enableInflectionalSearch, out int totalMatches)
        {
            return _FullTextProductSearch(pageSize, firstItemIndex, false, site, searchTerms, sortKey, filter, enableInflectionalSearch, out totalMatches);
        }

        public IEnumerable<Data.Product> FullTextProductSearch(int pageSize, int firstItemIndex, long categoryId, string[] searchTerms, string sortKey, ProductSearchFilter filter, bool enableInflectionalSearch, out int totalMatches)
        {            
            return _FullTextProductSearch(pageSize, firstItemIndex, true, categoryId, searchTerms, sortKey, filter, enableInflectionalSearch, out totalMatches);
        }

        private IEnumerable<Data.Product> _FullTextProductSearch(int pageSize, int firstItemIndex, bool searchByCategoryId, object p0, string[] searchTerms, string sortKey, ProductSearchFilter filter, bool enableInflectionalSearch, out int totalMatches)
        {
            if (searchTerms != null)
            {
                using (OrangeEntities db = new OrangeEntities())
                {
                    string categoryFilter = (searchByCategoryId) ? "Category.Id" : "Category.Site";
                    string query = $"SELECT * FROM Product JOIN Category ON (Product.CategoryId = Category.Id) WHERE {categoryFilter} = @p0 AND CONTAINS (Keywords, @p1)";
            
                    StringBuilder containsParamBuilder = new StringBuilder();
                    for (int i = 0; i < searchTerms.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerms[i]) &&
                            !FullTextTSQLReservedWords.Contains(searchTerms[i].ToUpper()))
                        {
                            //Join each search term with AND - (Meaning any record returned must contain all expressions).
                            if (containsParamBuilder.Length > 0)
                                containsParamBuilder.Append(" AND");
                            //If the search term consists of more than one token or contains a keyword recognized by the Full Text "CONTAINS" function,
                            //We surround the term with double quotes.
                            if (searchTerms[i].Contains(" "))
                                containsParamBuilder.Append($" \"{searchTerms[i]}\"");
                            //Otherwise, if variant search is enabled we search also search for variants of the the search term.
                            else if (enableInflectionalSearch)
                                containsParamBuilder.Append($" FORMSOF(INFLECTIONAL, \"{searchTerms[i]}\")");
                            //Otherwise
                            else
                                containsParamBuilder.Append($" {searchTerms[i]}");
                        }
                    }
                    if (containsParamBuilder.Length > 0)
                    {
                        string p1 = containsParamBuilder.ToString();
                        //**************************************************************************************************
                        //NOTE: Even though LINQ is used below, there is NO deferred query. The SQL constructed above
                        //is the only part of the query sent to the database. The filter, order, and skip operations, 
                        //below, occur in resident memory.
                        //TODO: Optimize by constructing the filter, order, and skip operations into the query.
                        //**************************************************************************************************
                        var results = db.Database.SqlQuery<Data.Product>(query, p0, p1)
                                                 .FilterProductWhere(filter);
                        totalMatches = results.Count();
                        return results.OrderProductBy(sortKey)     
                                      .Skip(firstItemIndex)
                                      .Take(pageSize)
                                      .ToList();
                    }
                }
            }
            totalMatches = 0;
            return new List<Data.Product>();
        }           

        public SalesHistory GetEbayProductSummarizedSalesHistory(long productId, bool storeSalesData)
        {
            string productReferenceId = null;           

            using (var db = new OrangeEntities())
            {
                var product = db.Products.First(x => x.Id == productId);
                productReferenceId = product.ReferenceId;               
            }

            EbayTradingClient tradingClient = new EbayTradingClient()
            {
                AppId = EbayProductionAppId,
                DevId = EbayProductionDevId,
                CertId = EbayProductionCertId
            };            

            List<TransactionType> tList = new List<TransactionType>();
            bool queryMoreTransactions = false;
            int page = 0;
            do
            {
                page++;
                var transactions = tradingClient.GetItemTransactions(new GetItemTransactionsRequest()
                {
                    RequesterCredentials = new RequesterCredentials()
                    {
                        EBayAuthToken = EbayUserTokenMeganova75
                    },
                    ItemID = productReferenceId,
                    NumberOfDays = 30,
                    Platform = TransactionPlatformCodeType.eBay,
                    Pagination = new Pagination()
                    {
                        EntriesPerPage = 100,
                        PageNumber = page
                    }
                });
                tList.AddRange(transactions.TransactionArray);
                queryMoreTransactions = transactions.HasMoreTransactions;
            } while (queryMoreTransactions);           

            var _now = DateTime.Now.ToUniversalTime();              
            SalesHistory history = new SalesHistory
            {
                SoldInLastDay = tList.Count(t => (_now - t.CreatedDate).TotalHours <= 24),
                SoldInLastFiveDays = tList.Count(t => (_now - t.CreatedDate).TotalHours <= 24 * 5),
                SoldInLastFifteenDays = tList.Count(t => (_now - t.CreatedDate).TotalHours <= 24 * 15),
                SoldInLastThirtyDays = tList.Count(t => (_now - t.CreatedDate).TotalHours <= 24 * 30)
            };

            BulkDeleteSalesDataForProductMeta(productId);

            if (storeSalesData)
            {
                using (var db = new OrangeEntities())
                {
                    List<SalesData> dbSalesData = new List<SalesData>();
                    dbSalesData.AddRange(tList.Select(t => new SalesData()
                    {
                        ProductReferenceId = productReferenceId,
                        SalePrice = Convert.ToDecimal(t.ConvertedTransactionPrice.Value),
                        SaleDate = t.CreatedDate
                    }));
                    db.BulkInsert(dbSalesData);
                    db.SaveChanges();
                }
            }

            return history;            
        }

        public SourcingItem GetSourcingItemFromUrl(string sourceItemUrl)
        {
            AlibabaScraper scraper = new AlibabaScraper();
            AlibabaScrapedResult result = scraper.ParseSourceItemPageUrl(sourceItemUrl);
            if (result != null && result.IsSuccess)
            {
                return new SourcingItem()
                {
                    DetailPageUrl = result.Items[0].DetailPageUrl,
                    SourceSite = SourcingSiteNames.Alibaba,
                    PriceDescription = result.Items[0].PriceDescription,
                    PriceRangeMin = result.Items[0].PriceRangeMin,
                    PriceRangeMax = result.Items[1].PriceRangeMax,
                    ImageUrl = result.Items[0].ImageUrl,
                    SourceProductId = result.Items[0].ProductId,
                    Title = result.Items[0].Title,
                    WholesalerName = null,
                };
            }
            else
                return null;
        }       
       

        #region Operation Feedback

        private void LogOperationException(string operationName, Exception ex)
        {
            OrangeCoreDiagnostics.LogOperationError(operationName, ex);
        }

        private void LogOperationStart(string operationName)
        {
            string message = "Operation Starting";      
            OrangeCoreDiagnostics.LogOperationInfo(operationName, message);
        }

        private void LogOperationComplete(string operationName)
        {
            string message = "Operation Completed Successfully";            
            OrangeCoreDiagnostics.LogOperationInfo(operationName, message);           
        }
      
        private void LogOperationWarning(string operationName, string message)
        {            
            OrangeCoreDiagnostics.LogOperationWarning(operationName, message);
        }

        private void LogOperationInfo(string operationName, string message)
        {
            OrangeCoreDiagnostics.LogOperationInfo(operationName, message);
        }

        #endregion

        #region Helper/Support methods        

        private void BulkDeleteCategoriesForSite(string siteName)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                //NOTE: 
                db.Database.ExecuteSqlCommand($"DELETE Category WHERE Site = @p0", siteName);              
            }
        }

        private void BulkDeleteProductsForSite(string siteName)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                //**************************************************************
                //NOTE: It's expected that all tables related to product have a
                //Cascade on DELETE rule.
                //**************************************************************
                db.Database.ExecuteSqlCommand(@"DELETE Product WHERE CategoryId IN 
                                                    (SELECT Id FROM Category
                                                    WHERE Site = @p0)", siteName);
            }
        }
        
        private void BulkDeleteSalesDataForProductMeta(long productMetaId)
        {
            using (OrangeEntities db = new OrangeEntities())
            {               
                db.Database.ExecuteSqlCommand(@"DELETE SalesData WHERE ProductMetaId = @p0", productMetaId);               
            }
        }        
     
        #endregion

        private static readonly string[] FullTextTSQLReservedWords = new[]
        {
            "AND", "&", "AND NOT", "&!", "OR", "|"
        };
    }     
}

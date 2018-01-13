using System;
using System.Collections.Generic;
using System.Linq;
using CipherPark.TriggerOrange.Core;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerOrange.Core.ApplicationServices;
using CipherPark.Amazon.Api;
using CipherPark.Walmart.Api;
using CipherPark.BestBuy.Api;
using CipherPark.Ebay.Api.Trading;
using CipherPark.Ebay.Api.Finding;
using CipherPark.Ebay.Api.Merchandising;
using CipherPark.Ebay.Api.Shopping;
using CipherPark.Ebay.Util;
using CipherPark.Alibaba.Api;

namespace CipherPark.TriggerOrange.Console
{    
    public class Program
    {
        #region Amazon Keys
        public const string AWSAccessKeyID = "AKIAJWJA6KN2R3UVP7DQ";
        public const string AWSSecretAccessKey = "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";
        public const string AWSAccountID = "0192-2398-0786";
        public const string AmazonCanonicalUserID = "1b476234139c07390a2d79ff7055b37c37ed1ccc6b42f87716daecabfcca0b53";
        public const string AmazonAssociateTag = "trigoran-20";
        #endregion

        #region Ebay Keys
        public const string EbayProductionDevId = "ecc2a8ce-2bad-41b5-995b-603ef425a06e";
        public const string EbayProductionAppId = "EugeneAd-aa5b-48be-a0c7-9cb1684a8072";
        public const string EbayProductionCertId = "5090c7e1-7001-4c22-bbdb-0cccef6ac65d";

        public const string EbayFindingClientVersion = "1.13.0";
        public const string EbayTradingClientVersion = "949";
        public const string EbayShoppingApiVersion = "949";
        public const string EbayMerchandisingVersion = "1.5.0";

        public const string EbayUserTokenMeganova75 = "AgAAAA**AQAAAA**aAAAAA**LsmYVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFmIWlDJiGpAydj6x9nY+seQ**Pv4CAA**AAMAAA**4CqjXTl8CCk2p168EkoNpXWXIOEw8hn6z4aCEF1FFTuWge+MiFyZO5RI8QtzA0/tWvP3oWiOHvk3Lw/TtQw1+2btjJ9+3OKHwc0g6/0TQwkcv4dI6DBhwHOlhzZ7WtBuqLZjCtXS8UN3MIqQF+IePOjVg/dXqHIWzph2vbvAlotAYLlZPYFW4MUCWJDLcpHxXFsS8DPd7UxaKM0Ov86E30iYa1b8Fa4x3CyMgi2xCuMVYITxurv7+ykbBByrgynq3Wo6uQKLKB2j/RDv4f7SOj3CKDKd9CrbjdSskkvR+0CBCTXWR4Ml3nArGUI8H/5JyV582xTGOfXwm3Z5fI6+S90kVYVdstdr7fYrTO8Lw3YFZcgogYjEslIepjgc/p//DhQXCud4xaj0Nd03sS2iMU9xkFiIpQlx0QYztFsqTSARncobXsZRAypaOpRgF51jMcbXmRvdYPyLf35k5EFmiw+GjFgrdOVITNTFVpeiDyljTt7cUU2QJ044jICBV0AaVxqB+Zc6I9McmrVWKdTcE0ml8oIjzkOuy1q6dXqPLcHuepoPXQZndmUCCRfDmYVKzvWQoZO0dU/DrWAxLqDkl2xrT0mpXUDFOkxH5X6UZ3xMd1cDC8ddO4k8/MtRgQOAPzyvChnttwzBaV4yV0jeKbeUAJGXGlcIlT2RTGZ6JNgFm0cOOi15SmNiRjaIVDE9j1GKI+nLA0aStgweUtAKOkQAkgobLElBQWYu37EMh4cTdLRlsNzMK1n3KNIHTgra";
        public static readonly DateTime EbayUserTokenExpiryMeganova75 = DateTime.Parse("2017-07-08 10:25:50");
        #endregion
       
        #region Walmart Keys
        public const string WalmartApiKey = "aeaawfvfpveu89crf2akwx5a";
        #endregion

        #region BestBuy       
        public const string BestBuyApiKey = "pnjfncrk9e32ade24strmdqa";
        #endregion             

        private const long BestBuyCallThrottle = 200;
        private const long AmazonCallThrottle = 1000;

        static void Main(string[] args)
        {
            //ConnectivityTest();
            //BestSellingTest();
            //EbayHotStarters();
        }

        private void ConsoleBackOffice()
        {

        }        
       

        private static string QueryInput(string message)
        {
            ShowMessage(message);
            return System.Console.ReadLine();
        }

        private static void ShowMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        private static void EnsureDirectoryExists(string fileName)
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));
        }

        static void BestSellingTest()
        {   
            #region Amazon          

            //Amazon.
            //

            AmazonClient client = new AmazonClient() { AWSSecretAccessKey = AWSSecretAccessKey, Throttle = AmazonCallThrottle };

            //CATEGORY UPDATE
            //---------------
            /*
            var rootBrowseNodes = LocaleInformationLookup.US.Where(l => l.BrowseNodeId != null);
            using (OrangeEntities db = new OrangeEntities())
            {
                db.Categories.RemoveWhere(c => c.Site == MarketPlaceSiteNames.Amazon);

                db.Categories.AddRange(rootBrowseNodes
                    .Select(l => new Core.Data.Category()
                    {
                        CategoryId = l.BrowseNodeId,
                        Name = l.Department,
                        Path = l.Department,
                        PathLevel = 0,
                        Site = MarketPlaceSiteNames.Amazon
                    }));                

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
                                CategoryId = n.BrowseNodeId,
                                Name = n.Name,
                                Path = $"{rootBrowseNode.Department}/{n.Name}",
                                PathLevel = 1,
                                Site = MarketPlaceSiteNames.Amazon,
                                ParentCategoryId = rootBrowseNode.BrowseNodeId
                            }));
                        }
                    }
                }

                db.SaveChanges();

                foreach (var cat in db.Categories.Where(c => c.Site == MarketPlaceSiteNames.Amazon))
                    System.Diagnostics.Trace.WriteLine($"{cat.Path} {cat.Id} {cat.ParentCategoryId}");
            }
            */

            //HOT LIST UPDATE
            //---------------

            /*
            using (OrangeEntities db = new OrangeEntities())
            {
                foreach (var cat in db.Categories.Where(c => c.Site == MarketPlaceSiteNames.Amazon && c.PathLevel == 1))
                {
                    var browseNodeResults = client.BrowseNodeLookup(new BrowseNodeLookupRequest()
                    {
                        AssociateTag = AmazonAssociateTag,
                        AWSAccessKeyId = AWSAccessKeyID,
                        ResponseGroups = new string[]
                        {
                            "TopSellers"
                        },
                        BrowseNodeId = cat.CategoryId
                    });

                    var topSellingItemsLookupResult = client.ItemLookup(new ItemLookupRequest()
                    {
                        AssociateTag = AmazonAssociateTag,
                        AWSAccessKeyId = AWSAccessKeyID,
                        IdType = "ASIN",
                        ItemId = string.Join(",", browseNodeResults.BrowseNodes.BrowseNodeCollection[0].TopSellers.Take(10).Select(ts => ts.ASIN)),
                        ResponseGroups = new string[]
                        {
                            "ItemAttributes",
                            "Images",
                            "SalesRank",
                            "Variations"
                        }                       
                    });
                }
            }
            */

            /*
            List<Tuple<string, string, long>> r1 = new List<Tuple<string, string, long>>();
            List<Tuple<string, string, long>> r2 = new List<Tuple<string, string, long>>();

            foreach (var browseNode in browseNodeResults.BrowseNodes.BrowseNodeCollection)
            {
                foreach (var topSeller in browseNode.TopSellers)
                {
                    var itemLookup = client.ItemLookup(new ItemLookupRequest()
                    {
                        AssociateTag = AmazonAssociateTag,
                        AWSAccessKeyId = AWSAccessKeyID,
                        ResponseGroups = new string[]
                        {
                            "Offers",
                            "ItemAttributes",
                            "OfferSummary",
                            "Images",
                            "SalesRank"
                        },
                        Condition = "All",
                        ItemId = topSeller.ASIN
                    });
                    r1.Add(new Tuple<string, string, long>(itemLookup.Items.ItemCollection[0].ASIN,
                                                           itemLookup.Items.ItemCollection[0].ItemAttributes.Title,
                                                           itemLookup.Items.ItemCollection[0].SalesRank));
                }

                //Find matching items in the Amazon market place for the current product.
                var itemSearchResults = client.ItemSearch(new ItemSearchRequest()
                {
                    AssociateTag = AmazonAssociateTag,
                    AWSAccessKeyId = AWSAccessKeyID,           
                    Condition = "All",
                    ResponseGroups = new string[]
                    {
                        "Offers",
                        "ItemAttributes",
                        "OfferSummary",
                        "Images",                        
                        "SalesRank"
                    },
                    SearchIndex = LocaleInformationLookup.US.GetById(browseNode.GetCategoriesNode().BrowseNodeId).SearchIndex,
                    BrowseNode = browseNode.BrowseNodeId,
                    Sort = "salesrank"
                });
                r2.AddRange(itemSearchResults.Items.ItemCollection.Select(item => new Tuple<string, string, long>(item.ASIN,                                                                                                                   item.ItemAttributes.Title,                                                                                                                item.SalesRank)));
            }
            */
            #endregion

            #region Ebay


            EbayTradingClient tradingClient = new EbayTradingClient()
            {
                AppId = EbayProductionAppId,
                DevId = EbayProductionDevId,
                CertId = EbayProductionCertId
            };
            
            /*
        
            //CATEGORY UPDATE
            //---------------

            //Pull ALL categories from Ebay.            
            
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
                db.SalesDatas.Delete(p => p.Product.Category.Site == MarketPlaceSiteNames.eBay);
                db.Products.Delete(p => p.Category.Site == MarketPlaceSiteNames.eBay);
                db.Categories.Delete(c => c.Site == MarketPlaceSiteNames.eBay);

                List<Core.Data.Category> dbCategories = new List<Core.Data.Category>();
                foreach (var category in categoriesResponse.CategoryArray)
                {                    
                    dbCategories.Add(new Core.Data.Category()
                    {
                        Name = category.CategoryName,
                        CategoryId = category.CategoryID,
                        Path = category.FindPath(categoriesResponse.CategoryArray),
                        PathLevel = Convert.ToByte(category.CategoryLevel - 1),
                        ParentCategoryId = category.CategoryLevel > 1 ? category.CategoryParentIDs.Last() : null,
                        Site = MarketPlaceSiteNames.eBay
                    });
                }

                db.Categories.AddRange(dbCategories);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("failed to update", ex);
                }                  

                foreach (var cat in db.Categories.Where(c => c.Site == MarketPlaceSiteNames.eBay))
                    System.Diagnostics.Trace.WriteLine($"{cat.Path} {cat.Id} {cat.ParentCategoryId}");
            }
            */
            
            //HOT LIST UPDATE
            //---------------

            using (OrangeEntities db = new OrangeEntities())
            {
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

                var categories = db.Categories.Where(c => c.Site == MarketPlaceSiteNames.eBayMostWatched).ToArray();

                foreach (var cat in categories )
                {
                    var mostWatchedItems = merchandisingClient.GetMostWatchedItems(new GetMostWatchedItemsRequest()
                    {
                        MaxResults = GetMostWatchedItemsRequest.MaxResultsUpperLimit,
                        CategoryId = cat.ReferenceId
                    });

                    foreach (var itemsBatch in mostWatchedItems.ItemRecommendations.Items.Batch(GetMultipleItemsRequest.MaxItemsLength))
                    {
                        var mostWatchedItemDetails = shoppingClient.GetMultipleItems(new GetMultipleItemsRequest()
                        {
                            ItemIDs = itemsBatch.Select(i => i.ItemId).ToArray(),
                            IncludeSelector = "Details"
                        });

                        var dbProducts = mostWatchedItemDetails.Items.Select(item => new Core.Data.Product()
                        {
                            CategoryId = cat.Id,
                            Price = Convert.ToDecimal(item.ConvertedCurrentPrice),
                            SmallImageUrl = item.PictureURL[0],
                            LargeImageUrl = item.PictureURL[0],
                            Name = item.Title,                          
                            OnlineAvailability = "Available Online",
                            ReferenceId = item.ItemID,
                            ProductUrl = item.ViewItemURLForNaturalSearch,
                            ShippingCost = mostWatchedItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).ShippingCost.ToString(),
                            WatchCount = mostWatchedItems.ItemRecommendations.Items.First(i => i.ItemId == item.ItemID).WatchCount,
                            UnitsSold = item.QuantitySold,
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                        }).ToList();

                        /*
                        db.Products.AddRange(dbProducts);
                        db.SaveChanges(); //We need to save changes here because we need the product.Id generated.
                        */

                        /*
                        List<SalesData> dbSalesData = new List<SalesData>();
                        foreach (var product in dbProducts)
                        {
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
                            dbSalesData.AddRange(transactions.TransactionArray.Select(t => new SalesData()
                            {
                                ProductId = product.Id,
                                SalePrice = Convert.ToDecimal(t.ConvertedTransactionPrice.Value),
                                SaleDate = t.CreatedDate
                            }));
                        }
                        db.SalesDatas.AddRange(dbSalesData);
                        db.SaveChanges();
                        */
                    }                
                }
            }

            /*
            EbayFindingClient client = new EbayFindingClient()
            {
                AppId = EbayProductionAppId,
                Version = EbayFindingClientVersion
            };
            var results = client.FindItemsByKeywords(new FindItemsByKeywordsRequest()
            {
                Keywords = "iphone 6",
                PaginationInput = new PaginationInput()
                {
                    EntriesPerPage = 100,
                    PageNumber = 1
                }        
            });
            */
            #endregion

            #region Walmart
            /*
            var walmartClient = new WalmartClient() { ApiKey = WalmartApiKey };
            CategoriesResponse response = walmartClient.Categories();

            using (OrangeEntities db = new OrangeEntities())
            {
                //Updating categories requires removal of all dependants entities.
                //Overwrite any categories that already exist for the current supplier.
                db.Products.RemoveWhere(p => p.Category.Site == SourcingSiteNames.Walmart);
                db.Categories.RemoveWhere(c => c.Site == SourcingSiteNames.Walmart);

                //Transform and push all downloaded categories into the db.                        
                foreach (var category in response.Categories.Flatten(1))
                {
                    db.Categories.Add(new Core.Data.Category()
                    {
                        Name = category.Name,
                        CategoryId = category.Id,
                        Path = category.Path,
                        PathLevel = Convert.ToByte(category.Level()),
                        ParentCategoryId = category.ParentId(),
                        Site = SourcingSiteNames.Walmart
                    });
                    System.Console.WriteLine($"{category.Id} {category.Path} {category.Level()} {category.ParentId()}");
                }
                db.SaveChanges();
            }
            */
            #endregion

            #region BestBuy

            var bestBuyClient = new BestBuyClient() { ApiKey = BestBuyApiKey, Throttle = BestBuyCallThrottle };
            var allBestBuyCategories = new List<BestBuy.Api.Category>();
            CategoriesResult result = null;
            do
            {
                int currentPage = result != null ? result.CurrentPage : 0;
                result = bestBuyClient.GetCategories(null, currentPage + 1, BestBuyConstants.MaxPageSize, "xml");
                allBestBuyCategories.AddRange(result.Categories);
            } while (result.CurrentPage < result.TotalPages);

            using (OrangeEntities db = new OrangeEntities())
            {
                //Updating categories requires removal of all dependants entities.
                //Overwrite any categories that already exist for the current supplier.
                db.Products.Delete(p => p.Category.Site == SourcingSiteNames.DHGate);
                db.Categories.Delete(c => c.Site == SourcingSiteNames.DHGate);

                //Transform and push all categories into the db.                        
                foreach (var category in allBestBuyCategories.Where(c => c.Level() <= 1))
                {
                    //string path = string.Join("/", category.Path.Categories.Select(c => c.Name).ToArray());
                    //int pathLength = category.Path.Categories.Length;
                    //string parentId = pathLength > 1 ? category.Path.Categories[pathLength - 2].Id : null;
                    db.Categories.Add(new Core.Data.Category()
                    {
                        Name = category.Name,
                        ReferenceId = category.Id,
                        PathLevel = Convert.ToByte(category.Level()),
                        ParentReferenceId = category.ParentId(), //parentId,
                        Path = category.DisplayPath(), //path,
                        Site = SourcingSiteNames.DHGate
                    });
                    System.Console.WriteLine($"{category.DisplayPath()} {category.Level()} {category.ParentId()}");
                }
                db.SaveChanges();
            }

            #endregion
        }

        static void EbayHotStarters()
        {
            string exceptionDetails = null;

            try
            {
                ShowMessage("Staring Hot Staters file maker");

                //const string CATEGORY_COMPUTERS = "58058";
                //const string CATEGORY_CONSUMER_ELECTRONICS = "293";
                //const string CATEGORY_SPORTING_GOODS = "888";
                const string CATEGORY_TOYS_HOBBIES = "220";

                ShowMessage("Initializing clients");
                EbayFindingClient findingClient = new EbayFindingClient()
                {
                    AppId = EbayProductionAppId,
                    Version = EbayFindingClientVersion
                };

                EbayShoppingClient shoppingClient = new EbayShoppingClient()
                {
                    AppId = EbayProductionAppId,
                    Version = EbayShoppingApiVersion
                };

                List<Ebay.Api.Shopping.SimpleItem> allItems = new List<Ebay.Api.Shopping.SimpleItem>();
              
                for (int i = 0; i < 24; i++)
                {
                    var now = DateTime.UtcNow;
                    var beginningOfDay = new DateTime(now.Year, now.Month, now.Day);
                    int windowFirstHour = i;
                    DateTime startTimeFrom = beginningOfDay.AddDays(-7).AddHours(windowFirstHour);
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
                        CategoryId = CATEGORY_TOYS_HOBBIES,
                        PaginationInput = new PaginationInput()
                        {
                            EntriesPerPage = 100
                        }
                    };

                    ShowMessage($"Finding Fixed price items starting from {startTimeFrom} to {startTimeTo}...");
                    List<Ebay.Api.Finding.SearchItem> allFoundItems = new List<Ebay.Api.Finding.SearchItem>();
                    FindItemsByCategoryResponse findItemsResponse = null;

                    do
                    {
                        findItemsRequest.PaginationInput.PageNumber++;
                        ShowMessage($"Requesting current page, {findItemsRequest.PaginationInput.PageNumber}");
                        findItemsResponse = findingClient.FindItemsByCategory(findItemsRequest);
                        if (findItemsResponse.SearchResult?.Items != null)
                        {
                            ShowMessage($"{findItemsResponse.SearchResult.Items.Count} entries found for current page, {findItemsResponse.PaginationOutput.PageNumber} of {findItemsResponse.PaginationOutput.TotalPages}");
                            allFoundItems.AddRange(findItemsResponse.SearchResult.Items);
                        }
                        else if (findItemsResponse.ErrorMessage != null)
                            throw new InvalidOperationException($"The Ebay api returned the following error message(s) {string.Join(Environment.NewLine, findItemsResponse.ErrorMessage.Errors.Select(x => x.Message))}");
                        else
                            ShowMessage("No items found for current page");
                    } while (findItemsResponse.PaginationOutput.PageNumber < findItemsResponse.PaginationOutput.TotalPages);

                    ShowMessage("Item search complete.");
                    var nPurged = allFoundItems.Count(x => x.ListingInfo.StartTime < startTimeFrom || x.ListingInfo.StartTime > startTimeTo);
                    allFoundItems.RemoveAll(x => x.ListingInfo.StartTime < startTimeFrom || x.ListingInfo.StartTime > startTimeTo);
                    ShowMessage($"Purged {nPurged} items because they fell outside the queried date range");
                    ShowMessage($"Getting full details for each item...");

                    foreach (var batch in allFoundItems.Batch(GetMultipleItemsRequest.MaxItemsLength))
                    {
                        ShowMessage("Requesting details for item batch");
                        var multipleItemsResponse = shoppingClient.GetMultipleItems(new GetMultipleItemsRequest()
                        {
                            ItemIDs = batch.Select(x => x.ItemId).ToArray(),
                            IncludeSelector = "Details"
                        });
                        if (multipleItemsResponse.Items != null)
                            allItems.AddRange(multipleItemsResponse.Items);
                    }

                    ShowMessage($"Finished getting full details for for current window {i+1} of 24.");
                }

                ShowMessage("Finished getting full details for all items");

                ShowMessage("Generating .csv file");
                string filePath = "Logs/results.csv";
                EnsureDirectoryExists(filePath);
                CSVWriter writer = new CSVWriter(filePath);
                writer.DataSource = TableWriter.FlattenToDataTable(allItems.Where(x => x.QuantitySold > 0));
                writer.Write(true);
                ShowMessage("File finished generating.");
            }
            catch (Exception ex)
            {
                ShowMessage("An Exception occured!!");
                exceptionDetails = ex.GetCompleteDetails();
                ShowMessage(exceptionDetails);
            }

            ShowMessage("Press any key to exit.");
            System.Console.ReadLine();

            if (exceptionDetails != null)
            {
                ShowMessage("Dumping exception details");
                string errorFilePath = "Logs/error.txt";
                EnsureDirectoryExists(errorFilePath);
                System.IO.File.WriteAllText(errorFilePath, exceptionDetails + Environment.NewLine + Environment.NewLine);
            }
        }

        static void ConnectivityTest()
        {
            /*
            AlibabaScraper scraperClient = new AlibabaScraper();
            string keywordPhrase = "iphone 6 case";
            int currentPage = 1;            
            var result = scraperClient.FindItemsByKeywords(keywordPhrase, currentPage);
            int totalPages = result.TotalPages;
            do
            {                
                if( result.IsSuccess == false)
                {
                    System.Console.WriteLine($"No success getting page {currentPage} of {totalPages}");
                    break;
                }
                foreach (var item in result.Items)
                    System.Console.WriteLine($"{item.ProductId}-{item.Title}-{item.ImageUrl}");
                currentPage++;
                result = scraperClient.FindItemsByKeywords(keywordPhrase, currentPage);
            } while (currentPage <= totalPages);
            System.Console.WriteLine("Press ENTER to quit");
            System.Console.Read();
            */

            //CRAIGS LIST
            //-----------
            /*
            Scraper scraper = new Scraper();
            var results = scraper.GetItemsForSale("http://newyork.craigslist.org/search/sss", "iphone 6");
            for(int i = 0; i < results.Length; i++)
            {
                System.Console.WriteLine("Item {0}", results[i].MerchantItemId);
                System.Console.WriteLine("Description {0}", results[i].Description);
                System.Console.WriteLine("Price {0}", results[i].Price);
                if(results[i].ImageIds != null)
                {
                    for(int j = 0; j < results[i].ImageIds.Length; j++)
                    {
                        System.Console.WriteLine("Image Id{1} {0}", results[i].ImageIds[j], j);
                    }
                }
                System.Console.WriteLine("############");
            }
            System.Console.ReadLine();
            */
            //--------------------------------------------------------------------------------------                             

            //EBAY
            //----
            /*
            EbayFindingClient client = new EbayFindingClient();
            client.Version = "1.13.0";
            client.AppId = EbayProductionAppId;
            var response = client.FindItemsByKeywords(new FindItemsByKeywordsRequest()
            {
                Keywords = "iphone 6"
            });
            */

            //WALMART
            //-------
            /*
            WalmartClient client = new WalmartClient();
            client.ApiKey = WalmartApiKey;
            var response = client.Search("iphone 6");
            */

            //BEST BUY
            //--------

            /*
            BestBuyClient client = new BestBuyClient();
            client.ApiKey = BestBuyApiKey;
            System.Collections.Generic.Dictionary<string, string> filter = new System.Collections.Generic.Dictionary<string, string>();
            filter.Add("id", "abcat0010000");
            CategoriesResult result = client.GetCategories(filter);
            */

            //AMAZON
            //------

            /*
            AmazonClient client = new AmazonClient();
            var results = client.ItemSearch(new ItemSearchRequest()
            {
                Keywords = new[] { "iphone" },
                SearchIndex = "Blended"
            });            
            */
        }
    }                  
}

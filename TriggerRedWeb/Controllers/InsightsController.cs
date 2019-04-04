using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Core;
using CipherPark.TriggerRed.Web.CoreServices;
using CipherPark.TriggerRed.Web.Models;

namespace CipherPark.TriggerOrange.Web.Controllers
{    
    public class InsightsController : Controller
    {
        public const long CategoryId_All = -1;
        public const long DefaultSearchGroupId = 1;

        public ActionResult Explore(string category, string keywords)
        {
            string _selectedSite = TriggerOrange.Core.MarketPlaceSiteNames.eBayHotStarters;
            long _selectedRoot = LongOrDefault(category, CategoryId_All);
            ExploreProductsViewModel model = new ExploreProductsViewModel()
            {
                Sites = ReferenceDataServices.GetMarketPlaceSiteNames().Select(n => new SelectListItem() { Text = n, Value = n }).ToList(),
                SelectedSite = _selectedSite,
                SelectedRootCategory = _selectedRoot.ToString(),
                Keywords = SanitizeKeywords(keywords),
                SortKeys = SearchSortKey.All.Select(n => new SelectListItem() { Text = n, Value = n }).ToList(),
                SelectedSortKey = SearchSortKey.All.First(),
                UserActiveListId = 0, //CoreDataServices.GetUserActiveReportId(User.Identity.GetUserId()),
            };
            return View(model);
        }      

        public JsonResult GetHotStarterProducts(int pageSize, int currentPage, string site, string root, string keywords, string sortKey, long listId, ProductSearchFilter filter)
        {
            int nTotalItemCount = 0;
            List<ProductModel> products = null;
            long categoryId = LongOrDefault(root, CategoryId_All);
            pageSize = Math.Min(pageSize, ExploreProductsViewModel.MaxPageSize);
            int firstItemIndex = pageSize * (currentPage - 1);
            string[] searchTerms = !string.IsNullOrWhiteSpace(keywords) ? keywords.Split(' ')
                                                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                                                  .ToArray() : null;
            if (categoryId != CategoryId_All)
                products = CoreDataServices.GetProductsByKeywordsForPage(pageSize, firstItemIndex, categoryId, false, searchTerms, sortKey, filter, out nTotalItemCount);
            else
                products = CoreDataServices.GetProductsByKeywordsForPage(pageSize, firstItemIndex, site, searchTerms, sortKey, filter, out nTotalItemCount);
            var result = new
            {
                products = products,
                currentPage = currentPage,
                totalItemCount = nTotalItemCount,
                numberOfPages = CalcNumberPages(nTotalItemCount, pageSize),
                pageSize = pageSize,
                /*listedProductIds = products.Where( p => CoreDataServices.IsProductInReport(listId, p.Id))
                                                   .Select(x => x.Id)
                                                   .ToArray(),*/
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static long LongOrDefault(string s, long defaultValue = default(long))
        {
            long result = 0;
            if (!long.TryParse(s, out result))
                return defaultValue;
            else
                return result;
        }

        public static int CalcNumberPages(int totalItemCount, int itemsPerPage)
        {
            int nPages = totalItemCount / itemsPerPage;
            if (nPages % itemsPerPage != 0)
                nPages++;
            return nPages;
        }

        private static string SanitizeKeywords(string keywords)
        {
            return keywords != null ? HttpUtility.HtmlEncode(keywords).Replace("&amp;", "&")
                                                                      .Replace("&quot;", "") : null;
        }
    }
    
    public class SubscriberModel
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string[] Subscriptions { get; set; }
    }
}

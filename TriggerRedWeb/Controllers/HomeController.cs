using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Core;
using CipherPark.TriggerRed.Web.Models;
using CipherPark.TriggerRed.Web.CoreServices;

namespace CipherPark.TriggerOrange.Web.Controllers
{  
    public class HomeController : Controller
    {      
        public ActionResult Index()
        {      
            return RedirectToAction("Landing");
        }
        
        public ActionResult Landing()
        {
            int nTotalProducts = 0;
            int nTotalBlogPosts = 0;
            LandingViewModel model = new LandingViewModel()
            {
                Products = CoreDataServices.GetProductsBySiteForPage(12, 0, MarketPlaceSiteNames.eBayHotStarters, SearchSortKey.UnitsSold, null, out nTotalProducts),
                TotalProducts = nTotalProducts,
                BlogPosts = CoreDataServices.GetBlogPostsForPage(3, 0, null, null, null, out nTotalBlogPosts),
                TotalBlogPosts = nTotalBlogPosts,
            };
            return View(model);
        }
       
        public ActionResult About()
        {
            return View();
        }       

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Strategy()
        {
            return View();
        }

        public ActionResult Subscribe()
        {
            return View();
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerRed.Web.CoreServices;
using CipherPark.TriggerRed.Web.Models;
using Microsoft.AspNet.Identity;

namespace CipherPark.TriggerOrange.Web.Controllers
{
    [AllowAnonymous]
    public class SpotLightController : Controller
    {
        // GET: Insights
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPosts(int pageSize, int currentPage, string sortKey, string category, string keywords)
        {
            int nTotalItemCount = 0;
            int firstItemIndex = pageSize * (currentPage - 1);
            var posts = CoreDataServices.GetSpotlightPostsForPage(pageSize, firstItemIndex, sortKey, category, keywords, out nTotalItemCount);
            var result = new
            {
                posts = posts,
                totalItemCount = nTotalItemCount,
                numberOfPages = InsightsController.CalcNumberPages(nTotalItemCount, pageSize),
                pageSize = pageSize,
                currentPage = currentPage,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Web.Models;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerOrange.Web.CoreServices;

namespace CipherPark.TriggerOrange.Web.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        // GET: Insights
        public ActionResult Index()
        {
            return RedirectToAction("RecentPosts");
        }

        public ActionResult RecentPosts(Guid? id)
        {
            Blog blog = null;
            if (id != null)
            {
                using (var db = new OrangeEntities())
                {
                    blog = db.Blogs.FirstOrDefault(x => x.Id == id);
                }
            }
            return View(new RecentBlogPosts
            {
                BlogId = blog?.Id,
                BlogName = BlogPages.All.FirstOrDefault(x => x.Name == blog?.Name)?.Caption,
            });
        }

        /*
        public ActionResult Post(long id)
        {
            BlogPostViewModel model = new BlogPostViewModel();
            using (var db = new OrangeEntities())
            {
                var post = db.BlogPosts.First();
                model.Content = post.BlogContent;
                model.CoverImageId = post.CoverImageId;
                model.CoverImageUrl = CoreServices.ReferenceDataServices.HostedImageUrl(post.CoverImageId);
                model.DateCreated = post.DateCreated;
                model.DateModified = post.DateModified;
                model.ProductKeywords = post.ProductKeywords;
                model.ProductPrice = post.ProductPrice;
                model.ProductReferenceId = post.ProductReferenceId;
                model.ProductSellerScore = post.ProductSellerScore;
                model.ProductSellerLevel = Util.SellerLevels.ScoreToLevel(model.ProductSellerScore);
                model.ProductUnitsSold = post.ProductUnitsSold;
                model.ProductUrl = post.ProductUrl;
                model.Title = post.Title;
                return View(model);
            }
        }
        */

        public JsonResult GetPosts(int pageSize, int currentPage, string sortKey, Guid? blogId, string keywords)
        {
            int nTotalItemCount = 0;
            pageSize = System.Math.Min(pageSize, 10);
            int firstItemIndex = pageSize * (currentPage - 1);            
            return Json(new
            {
                posts = CoreDataServices.GetBlogPostsForPage(pageSize, firstItemIndex, sortKey, blogId,  keywords, out nTotalItemCount),
                nTotalItemCount,
                currentPage = currentPage,
            }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetCategories()
        {
            return Json(new
            {
                categories = BlogPages.All.ToArray(),                
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
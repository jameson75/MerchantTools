using System;
using System.Linq;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerRed.Web.CoreServices;
using CipherPark.TriggerRed.Web.Models;

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

       
        public ActionResult SinglePost(long id)
        {           
            using (var db = new OrangeEntities())
            {
                var p = db.BlogPosts.First(x => x.Id == id);
                var next = db.BlogPosts.OrderBy(x => x.Id)
                                       .FirstOrDefault(x => x.Id < id)
                                       ?.Id;
                var prev = db.BlogPosts.OrderBy(x => x.Id)
                                       .FirstOrDefault(x => x.Id > id)
                                       ?.Id;                                       
                var model = new BlogPostModel()
                {
                    Id = p.Id,
                    Content = p.BlogContent,
                    Summary = p.BlogSummary,
                    Title = p.Title,
                    DateCreated = p.DateCreated.ToUnixMilliseconds(),
                    DateModified = p.DateModified.ToUnixMilliseconds(),
                    ProductPrice = p.ProductPrice,
                    ProductSellerScore = p.ProductSellerScore,
                    ProductUnitsSold = p.ProductUnitsSold,
                    ProductUrl = p.ProductUrl,
                    CoverImageId = p.CoverImageId,
                    CoverImageUrl = ReferenceDataServices.HostedImageUrl(p.CoverImageId),
                    ProductSellerLevel = TriggerRed.Web.Util.SellerLevels.ScoreToLevel(p.ProductSellerScore),
                    ProductListingDate = p.ProductListingDate.GetValueOrDefault().ToUnixMilliseconds(),
                    ProductCategory = p.ProductCategory,
                    Category = p.Blog.Caption,
                    PrevId = prev,
                    NextId = next,
                };
                return View(model);
            }
        }        

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
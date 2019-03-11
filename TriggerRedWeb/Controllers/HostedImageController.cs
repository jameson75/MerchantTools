using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Core.Data;
namespace CipherPark.TriggerOrange.Web.Controllers
{
    public class HostedImageController : Controller
    {
        // GET: HostedImage
        [AllowAnonymous]
        public FileResult Index(long id)
        {
            using (var db = new OrangeEntities())
            {
                var path = db.HostedImages.First(x => x.Id == id).PhysicalPath;                
                return new FileStreamResult(new System.IO.FileStream(path, System.IO.FileMode.Open), "image/jpeg");                
            }
        }

        public ActionResult ResolveAll()
        {
            using (var db = new OrangeEntities())
            {
                db.ReportItems.Where(x => x.HostedImageId == null)
                              .ToList()
                              .ForEach(x => x.HostedImageId = CoreServices.CoreDataServices.DownloadImageToLocalHost(x.OriginalImageUrl));
                db.SaveChanges();
            }
            return Content("<html>Resolved Images</html>");
        }
    }
}
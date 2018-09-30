using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TriggerRedWeb.Controllers
{
    public class EbayLifeController : Controller
    {
        // GET: EbayLife
        public ActionResult Index()
        {
            return View("Latest");
        }

        public ActionResult Latest()
        {
            return View();
        }
    }
}
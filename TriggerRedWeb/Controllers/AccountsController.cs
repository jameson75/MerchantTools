using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CipherPark.TriggerRed.Web.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View("Subscribe");
        }

        public ActionResult Subscribe()
        {
            return View();
        }
    }
}
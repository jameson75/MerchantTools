using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Web.Models;
using CipherPark.TriggerOrange.Core.Data;

namespace CipherPark.TriggerOrange.Web.Controllers
{    
    public class InsightsController : Controller
    {
        

        public ActionResult Strategy()
        {
            return View();
        }

        public ActionResult Subscribe()
        {
            return View();
        }

        public JsonResult CreateSubscription(SubscriberModel subscriber)
        {
            return Json(null);
        }
    }
    
    public class SubscriberModel
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string[] Subscriptions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Web.Models;

namespace CipherPark.TriggerOrange.Web.Controllers
{  
    public class HomeController : Controller
    {      
        public ActionResult Index()
        {                    
            return View("Landing");
        }
        
        public ActionResult Landing()
        {
            return View();
        }
       
        public ActionResult About()
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
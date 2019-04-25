using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
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

        public ActionResult MessageSent(bool? captchaFailed)
        {
            ViewBag.CaptchaFailed = captchaFailed.GetValueOrDefault();
            return View();
        }

        public ActionResult ReleaseNotes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitEmail(FormCollection collection)
        {
            string fullName = collection["fullName"];
            string email = collection["email"];
            string message = collection["message"];           
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("system@triggerred.com", fullName);
            mail.To.Add(new MailAddress("info@triggerRed.com"));          
            mail.ReplyToList.Add(email);
            mail.Body = message;
            var captchaValid = IsReCaptchaResponseValid(collection["g-recaptcha-response"]);      
            if(captchaValid)
                smtpClient.Send(mail);
            return RedirectToAction("MessageSent", new { captchFailed = true});
        }

        public bool IsReCaptchaResponseValid(string captchaResponse)
        {
            var result = false;            
            var secretKey = System.Configuration.ConfigurationManager.AppSettings["RecaptchaSecretKey"];
            var requestUri = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}";           
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
    }
}
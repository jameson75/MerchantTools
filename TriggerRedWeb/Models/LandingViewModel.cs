using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CipherPark.TriggerRed.Web.Models;

namespace CipherPark.TriggerRed.Web.Models
{
    public class LandingViewModel
    {
        public List<ProductModel> Products { get; set; }
        public int TotalProducts { get; set; }
        public List<SpotlightPostJsonModel> SpotlightPosts { get; set; }
        public int TotalBlogPosts { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerOrange.Core;

namespace CipherPark.TriggerRed.Web.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        [SafeHtmlIgnore]
        public string Name { get; set; }

        public double Price { get; set; }
        
        public string Upc { get; set; }

        public string SmallImageUrl { get; set; }

        public string LargeImageUrl { get; set; }
        
        public string ProductUrl { get; set; }

        [EntityToModelMapping("AffiliateAddToCartUrl")]
        public string CartUrl { get; set; }

        public string OnlineAvailability { get; set; }

        public string ShippingCost { get; set; }

        public bool FreeShipping { get; set; }
    
        public long CategoryId { get; set; }

        public long UnitsSold { get; set; }

        public long WatchCount { get; set; }

        [EntityToModelMapping("DateCreated", EntityToModelMappingOptions.JavascriptDate)]
        public string DateCreated { get; set; }

        [EntityToModelMapping("DateModified", EntityToModelMappingOptions.JavascriptDate)]
        public string DateModified { get; set; }

        public string ReferenceId { get; set; }

        public int SellerScore { get; set; }

        [EntityToModelMapping("DateListed", EntityToModelMappingOptions.JavascriptDate)]
        public string DateListed { get; set; }

        [EntityToModelMapping("Category.Name")]
        public string CategoryName { get; set; }

        [EntityToModelDataMappingIgnore]
        public string SellerLevel { get { return Util.SellerLevels.ScoreToLevel(this.SellerScore);  } }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CipherPark.TriggerRed.Web.Models
{
    public class SpotlightPostJsonModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public long CoverImageId { get; set; }
        public string CoverImageUrl { get; set; }
        public long ProductUnitsSold { get; set; }
        public decimal ProductPrice { get; set; }
        public long ProductSellerScore { get; set; }
        public string ProductReferenceId { get; set; }
        public string ProductUrl { get; set; }
        public long DateCreated { get; set; }
        public long DateModified { get; set; }
        public string Category { get; set; }
        public string ProductSellerLevel { get; set; }
        public long ProductListingDate { get; set; }
        public string ProductCategory { get; set; }
        public SpotlightSaleJsonModel[] Sales { get; set; }
    }

    public class SpotlightSaleJsonModel
    {
        public long Date { get; set; }
        public long UnitsSold { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CipherPark.TriggerOrange.Web.Util;

namespace CipherPark.TriggerOrange.Web.Models
{
    public class BlogPostViewModel
    {
        public long? Id { get; set; }
        public string EditorCaption { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CoverImageUrl { get; set; }
        public long CoverImageId { get; set; }
        public List<SelectListItem> BlogNames { get; set; }
        public long ProductUnitsSold { get; set; }
        public decimal ProductPrice { get; set; }
        public long ProductSellerScore { get; set; }
        public string ProductReferenceId { get; set; }
        public string ProductKeywords { get; set; }
        public string ProductUrl { get; set; }
        public bool IsNewPost { get { return Id == null; } }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public long ReportReference { get; set; }
        public Guid SelectedBlogId { get; set; }
        public string ProductSellerLevel { get; set; }       
    }     
    
    public class BlogPostJsonModel
    {
        public long Id { get; set; }       
        public string Title { get; set; }
        public string Content { get; set; }
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
    }  
}
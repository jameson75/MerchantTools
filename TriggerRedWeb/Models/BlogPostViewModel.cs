﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CipherPark.TriggerRed.Web.Models
{
    public class BlogPostModel
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
        public string Summary { get; set; }
        public long? PrevId { get; internal set; }
        public long? NextId { get; internal set; }
    }

    public static class DateTimeHelper
    {
        private static readonly DateTime _UnixEpoc = new DateTime(1970, 1, 1);
        public static DateTime FromUnixMilliseconds(long dateTime)
        {
            return DateTime.MinValue.AddMilliseconds(dateTime + (_UnixEpoc - DateTime.MinValue).TotalMilliseconds).ToLocalTime();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.TriggerOrange.Core
{
    public class SourcingResult
    {
        public bool IsSuccess { get; internal set; }
        public List<SourcingItem> Items { get; internal set; }
        public string RefinedKeywords { get; internal set; }
        public int TotalPages { get; internal set; }
    }

    public class SourcingItem
    {
        public string DetailPageUrl { get; internal set; }
        public decimal? PriceRangeMin { get; internal set; }
        public decimal? PriceRangeMax { get; internal set; }
        public string ImageUrl { get; internal set; }
        public string PriceDescription { get; internal set; }
        public string SourceProductId { get; internal set; }
        public string SourceSite { get; set; }
        public string Title { get; internal set; }
        public string WholesalerName { get; internal set; }
    }
}

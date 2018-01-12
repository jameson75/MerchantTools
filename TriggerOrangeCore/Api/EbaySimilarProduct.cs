using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.TriggerOrange.Core
{
    public class EbaySimilarProduct
    {
        public decimal Price { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }        
        public string Name { get; set; }
        public string OnlineAvailability { get; set; }
        public string ReferenceId { get; set; }
        public string ProductUrl { get; set; }
        public string ShippingCost { get; set; }
        public long WatchCount { get; set; }
        public int UnitsSold { get; set; }
    }
}

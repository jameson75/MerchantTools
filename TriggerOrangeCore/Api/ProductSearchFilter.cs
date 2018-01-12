using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.TriggerOrange.Core
{
    public class ProductSearchFilter
    {
        public decimal? PriceHigh { get; set; }
        public decimal? PriceLow { get; set; }
        public long? UnitsHigh { get; set; }
        public long? UnitsLow { get; set; }
        public long? SellerRankHigh { get; set; }
        public long? SellerRankLow { get; set; }
    }
}

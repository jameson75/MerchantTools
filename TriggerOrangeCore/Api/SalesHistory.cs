using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.TriggerOrange.Core
{
    public class SalesHistory
    {
        public int SoldInLastDay { get; set; }
        public int SoldInLastFiveDays { get; set; }
        public int SoldInLastFifteenDays { get; set; }
        public int SoldInLastThirtyDays { get; set; }
    }
}

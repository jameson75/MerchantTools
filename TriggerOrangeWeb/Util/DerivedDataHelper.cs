using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CipherPark.TriggerOrange.Web.Util
{
    public static class DerivedDataHelper
    {
        public static string ToStringOrNull<T>(this T? n) where T : struct
        {
            return n != null ? n.ToString() : null;
        }

        public static decimal? Subtract(decimal? d1, decimal? d2)
        {
            return (d1 == null || d2 == null) ?  null : d1 - d2;
        }

        public static decimal? ParseDecimalOrNull(string estimatedPrice)
        {
            decimal d = 0;
            return (decimal.TryParse(estimatedPrice, out d)) ? d : (decimal?)null;   
        }
    }
}
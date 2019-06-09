using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CipherPark.TriggerRed.Web.Util
{
    public class RotatingAd
    {
        public string Name { get; set; }
        public string HtmlString { get; set; }
        /// <summary>
        /// Relative weighting of the rotating Ad.
        /// </summary>
        public float Weight { get; set; }
        public bool Enabled { get; set; }

        public static RotatingAd[] BoxAds = new RotatingAd[]
        {
            new RotatingAd
            {
                Name = "SaleHoo1",
                Weight = 0.30f,
                HtmlString = @"<a href=""https://www.salehoo.com/learn-more?aff=cipherseller"" target=""_blank"">
                               <img width=""480"" height=""250"" border=""0"" style=""border:none"" src=""https://cdn.salehoo.com/files/image/fb-banners/fb-1200-x-628-wholesale-a.png"" />
                               </a>",
                Enabled = true,
            },

            new RotatingAd
            {
                Name = "SaleHoo2",
                Weight = 0.30f,
                HtmlString = @"<a href=""https://www.salehoo.com/learn-more?aff=cipherseller"" target=""_blank"">
                              <img width=""300"" height=""250"" border=""0"" style=""border:none"" src=""https://cdn.salehoo.com/img/banners/salehoo/300-x-250-C.jpg"" />
                              </a>",
                Enabled = true,
            },

            new RotatingAd
            {
                Name = "InventorySource",
                Weight = 0.10f,
                HtmlString = @"<a href=""http://www.kqzyfj.com/click-1-13618431"" target=""_blank"">                              
                              <img src=""https://www.triggerred.com/Content/inventory_source.jpg"" width=""450"" height=""250"" alt=""Inventory Source"" border=""0""/>
                              </a>",
                Enabled = true,
            },

            new RotatingAd
            {
                Name = "AliExpress",
                Weight = 0.10f,
                HtmlString = "",
                Enabled = false,
            },

            new RotatingAd
            {
                Name = "DHGate",
                Weight = 0.10f,
                HtmlString = "",
                Enabled = false,
            },

            new RotatingAd
            {
                Name = "FedEx",               
                Weight = 0.10f,
                HtmlString = "",
                Enabled = false,
            }
        };

        internal static RotatingAd SelectRandom(RotatingAd[] ads)
        {
            Random rand = new Random();
            var randonNumber = rand.Next(1, 100);
            var enabledAds = ads.Where(x => x.Enabled).ToList();
            var totalWeight = enabledAds.Sum(x => x.Weight);
            Range<int> probabilityWindow = new Range<int>();
            RotatingAd result = null;
            foreach(var ad in enabledAds)
            {
                var currentwindowSize = (int)(100 * ad.Weight / totalWeight);
                probabilityWindow.Maximum = probabilityWindow.Minimum + currentwindowSize;
                if (probabilityWindow.ContainsValue(randonNumber))
                {
                    result = ad;
                    break;
                }
                probabilityWindow.Minimum += currentwindowSize;
            }
            return result;
        }
    }

    /// <summary>The Range class.</summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>Minimum value of the range.</summary>
        public T Minimum { get; set; }

        /// <summary>Maximum value of the range.</summary>
        public T Maximum { get; set; }

        /// <summary>Presents the Range in readable format.</summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", this.Minimum, this.Maximum);
        }

        /// <summary>Determines if the range is valid.</summary>
        /// <returns>True if range is valid, else false</returns>
        public bool IsValid()
        {
            return this.Minimum.CompareTo(this.Maximum) <= 0;
        }

        /// <summary>Determines if the provided value is inside the range.</summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value is inside Range, else false</returns>
        public bool ContainsValue(T value)
        {
            return (this.Minimum.CompareTo(value) <= 0) && (value.CompareTo(this.Maximum) <= 0);
        }

        /// <summary>Determines if this Range is inside the bounds of another range.</summary>
        /// <param name="Range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsInsideRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && range.ContainsValue(this.Minimum) && range.ContainsValue(this.Maximum);
        }

        /// <summary>Determines if another range is inside the bounds of this range.</summary>
        /// <param name="Range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool ContainsRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && this.ContainsValue(range.Minimum) && this.ContainsValue(range.Maximum);
        }
    }
}
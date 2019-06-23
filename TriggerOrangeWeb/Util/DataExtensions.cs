using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CipherPark.TriggerOrange.Core;
using CipherPark.TriggerOrange.Core.Data;

namespace CipherPark.TriggerOrange.Web.Util
{
    public static class DataExtensions
    {
        public static bool IsOrphaned(this HostedImage image)            
        {
            return image.ReportItems.Any() == false &&
                   image.SpotlightPosts.Any() == false;
        }
    }
}
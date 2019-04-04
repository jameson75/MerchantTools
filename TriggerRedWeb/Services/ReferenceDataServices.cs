using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CipherPark.TriggerRed.Web.CoreServices
{
    public static class ReferenceDataServices
    {
        public static string[] GetMarketPlaceSiteNames()
        {
            return TriggerOrange.Core.MarketPlaceSiteNames.AllSupported.ToArray();
        }
        
        public static string[] GetSourcingSiteNames()
        {
            return TriggerOrange.Core.SourcingSiteNames.AllSupported.ToArray();
        }       

        public static string[] GetTaskNames()
        {
            return TriggerOrange.Core.LongRunningTaskNames.All.ToArray();
        }

        public static IEnumerable<string> GetSchedulerStartTimes()
        {
            DateTime d = DateTime.MinValue;
            int minutesPerDay = 24 * 60;
            const int intervalLength = 15;
            List<string> javascriptTimeIntervals = new List<string>();
            for (int i = 0; i < minutesPerDay; i += intervalLength)
                javascriptTimeIntervals.Add(d.AddMinutes(i).ToString(SchedulerStartTimeFormat));
            return javascriptTimeIntervals;
        } 

        public const string SchedulerStartTimeFormat = "hh:mm tt";     

        public static string ImageStorePhysicalPath
        {
            get
            {
                var imageStorePath = System.Configuration.ConfigurationManager.AppSettings["ImageStorePath"];

                //If the path is not an absolute path, map the path to a physical directory.            
                if (System.IO.Path.GetFullPath(imageStorePath) != imageStorePath)
                    imageStorePath = HttpContext.Current.Server.MapPath(imageStorePath);

                return imageStorePath;
            }
        }

        public static string HostedImageUrl(long hostImageId)
        {
            return $"/HostedImage?id={hostImageId}";
        }
    } 
}

public class BlogPages
{
    public class BlogPage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
    }

    public static BlogPage[] All
    {
        get
        {
            //TODO: Store this information in config file.
            return new BlogPage[]
            {
                new BlogPage { Id = new Guid("{C98361A5-1295-4D49-A861-0E979F1790BB}"), Name="EbayLifeBeginner", Caption="Beginner" },
                new BlogPage { Id = new Guid("{2D33B3F4-3A4E-4ED1-9042-38117E07CBB7}"), Name="EbayLifeIntermediate", Caption="Intermediate" },
                new BlogPage { Id = new Guid("{065EE618-957C-45C4-A573-B70201ED0B4E}"), Name="EbayLifeAdvanced", Caption="Advanced" },
                new BlogPage { Id = new Guid("{2B43F3B5-50F3-40A2-8392-30C5F17ED0C2}"), Name="EbayLifeExpert", Caption="Expert" },
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CipherPark.TriggerOrange.Web
{
    public class BundleConfig
    { 
        public static void RegisterBundles(BundleCollection bundles)
        {             
            //Angular Libraries
            //-----------------
            bundles.Add(new ScriptBundle("~/bundles/angular/js")                        
                        .Include("~/Scripts/angular.js")
                        .Include("~/Scripts/angular-sanitize.js")
                        .Include("~/Scripts/angular-animate.js")
                        .Include("~/Scripts/angular-route.js")
                        .Include("~/Scripts/angular-resource.js")
                        .Include("~/Scripts/angular-cookies.js"));

            //Angular UI
            //----------
            bundles.Add(new ScriptBundle("~/bundles/angularui/js")
                        .Include("~/Scripts/ui-bootstrap-tpls-0.14.3.js"));

            //Custom scripts.
            //---------------
            bundles.Add(new ScriptBundle("~/bundles/orange/js")
                        .Include("~/Scripts/orange/angular-orange.js")
                        .Include("~/Scripts/ajaxhistory.js"));                     

            //Ng-Table Libraries
            //------------------
            bundles.Add(new ScriptBundle("~/bundles/ngtable/js")
                    .Include("~/Scripts/ng-table-master/dist/ng-table.js"));

            bundles.Add(new StyleBundle("~/bundles/ngtable/css")
                    .Include("~/Scripts/ng-table-master/dist/ng-table.css"));

            //Smart-Table Libraries
            //---------------------
            bundles.Add(new ScriptBundle("~/bundles/smarttable/js")
                    .Include("~/Scripts/smart-table-master/dist/smart-table.js"));

            //Gifted Libraries
            //----------------
            bundles.Add(new StyleBundle("~/bundles/gifted/css")                    
                    .Include("~/Content/gifted/css/font-awesome.min.css")
                    .Include("~/Content/gifted/css/global.css")
                    .Include("~/Content/gifted/css/style.css")
                    .Include("~/Content/gifted/css/beauty.css")
                    .Include("~/Content/gifted/css/auto-dealer.css")
                    .Include("~/Content/gifted/css/responsive.css")
                    .Include("~/Content/gifted/css/skin.less")
                    .Include("~/Content/gifted/css/transition-effect.css"));


            bundles.Add(new ScriptBundle("~/bundles/gifted/js")
                    .Include("~/Scripts/gifted/jquery-1.11.3.min.js")
                    .Include("~/Scripts/gifted/modernizr.js")
                    .Include("~/Scripts/gifted/bootstrap.min.js")
                    .Include("~/Scripts/gifted/jquery-ui.js")
                    .Include("~/Scripts/gifted/less.js")

                    .Include("~/Scripts/gifted/jquery.plugin.js")
                    .Include("~/Scripts/gifted/jquery.countdown.min.js")
                    .Include("~/Scripts/gifted/jquery.masonry.pkgd.min.js")                    

                    .Include("~/Scripts/gifted/owl.carousel.min.js")
                    .Include("~/Scripts/gifted/jquery.flexslider.js")
                    .Include("~/Scripts/gifted/jquery.appear.js")
                    .Include("~/Scripts/gifted/jquery.shuffle.js")

                    .Include("~/Scripts/gifted/site.js"));
        }                                           
    }
}
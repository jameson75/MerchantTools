using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CipherPark.TriggerOrange.Web.MvcHelpers
{
    public static class HtmlHelperExtension
    {
        public const string DefaultLoginCatpion = "Sign In";
        private const string DefaultLogoutCaption = "Sign Out";

        public static MvcHtmlString LoginLink(this HtmlHelper html, string loginAction, string logoutAction, string controllerName, string loginCaption = DefaultLoginCatpion, string logoutCaption = DefaultLogoutCaption )
        {
            string caption = html.ViewContext.HttpContext.Request.IsAuthenticated ? logoutCaption : loginCaption;
            string action = html.ViewContext.HttpContext.Request.IsAuthenticated ? logoutAction : loginAction;
            var url = new UrlHelper(html.ViewContext.RequestContext);
            string href = url.Action(action, controllerName);
            return new MvcHtmlString($"<a href=\"{href}\" class=\"btn btn-default\">{caption}</a>");
        }

        public static MvcHtmlString SignInButton(this HtmlHelper html, string modalFormId, string logoutAction, string logOutControllerName, string loginCaption = DefaultLoginCatpion, string logoutCaption = DefaultLogoutCaption)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var isUserAuthenticated = html.ViewContext.HttpContext.Request.IsAuthenticated;
            string caption = isUserAuthenticated ? logoutCaption : loginCaption;          
            string href = isUserAuthenticated ? urlHelper.Action(logoutAction, logOutControllerName) : "javascript:;";
            string data_toggle = isUserAuthenticated ? null : "modal";
            string data_target = isUserAuthenticated ? null : $"#{modalFormId}";
            //return new MvcHtmlString($"<a href=\"{href}\" class=\"btn btn-default\">{caption}</a>");
            string toolTip = isUserAuthenticated ? $"Sign Out {html.ViewContext.HttpContext.User.Identity.Name}" : null;
            return new MvcHtmlString($"<a href=\"{href}\" data-toggle=\"{data_toggle}\" data-target=\"{data_target}\" class=\"c-btn-border-opacity-04 c-btn btn-no-focus c-btn-header btn btn-sm c-btn-border-1x c-btn-white c-btn-circle c-btn-uppercase c-btn-sbold\" uib-tooltip=\"{toolTip}\"><i class=\"icon-user\"></i> {caption}</a>");
        }

        public static MvcHtmlString UnorderedList(this HtmlHelper html, IEnumerable<string> list, object liHtmlElements = null, object ulHtmlElements = null)
        {
            TagBuilder ul = new TagBuilder("ul");
            StringBuilder ulHtml = new StringBuilder();
            foreach(string item in list)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(item);
                ulHtml.Append(li.ToString());
            }
            ul.InnerHtml = ulHtml.ToString();
            return new MvcHtmlString(ul.ToString());
        }
    }
}

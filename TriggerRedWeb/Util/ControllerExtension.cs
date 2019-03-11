using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

namespace CipherPark.TriggerOrange.Web.Util
{
    public static class ControllerExtension
    {
        #region PartialViewToString

        public static string PartialViewToString(this Controller controller, string partialViewName, object model = null)
        {
            ControllerContext controllerContext = new ControllerContext(controller.Request.RequestContext, controller);
            var partialView = ViewEngines.Engines.FindPartialView(controllerContext, partialViewName);
            if (partialView == null)
                throw new FileNotFoundException("Partial view cannot be found.");

            return ViewToString(
                controller,
                controllerContext,
                partialView,
                model
            );
        }

        #endregion

        #region ViewToString

        public static string ViewToString(this Controller controller, string viewName, object model = null)
        {
            ControllerContext controllerContext = new ControllerContext(controller.Request.RequestContext, controller);
            var view = ViewEngines.Engines.FindView(controllerContext, viewName, null);
            if (view != null)
                throw new FileNotFoundException("View cannot be found.");

            return ViewToString(
                controller,
                controllerContext,
                view,
                model
            );
        }

        public static string ViewToString(this Controller controller, string viewName, string controllerName, string areaName, object model = null)
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", controllerName);

            if (areaName != null)
            {
                routeData.Values.Add("Area", areaName);
                routeData.DataTokens["area"] = areaName;
            }

            ControllerContext controllerContext = new ControllerContext(controller.HttpContext, routeData, controller);
            var view = ViewEngines.Engines.FindView(controllerContext, viewName, null);
            if (view == null)
                throw new FileNotFoundException("View cannot be found.");

            return ViewToString(
                controller,
                controllerContext,
                view,
                model
            );
        }

        #endregion

        #region Private Methods

        public static string ViewToString(this Controller controller, ControllerContext controllerContext, ViewEngineResult viewEngineResult, object model)
        {
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(
                    controllerContext,
                    viewEngineResult.View,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    writer
                );

                viewEngineResult.View.Render(viewContext, writer);

                return writer.ToString();
            }
        }

        #endregion
    }
}
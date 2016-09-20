using System.Web.Mvc;
using System.Web.Routing;

namespace ServeMeHR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
    namespaces: new string[] { "ServeMeHR.Controllers" }
            );
            //    routes.MapRoute(
            //"ErrorHandler",
            //"Error/{action}/{errMsg}",
            //new { controller = "Error", action = "Unauthorized", errMsg = UrlParameter.Optional }
            //);

            routes.MapRoute(
                "ServiceRequestNotes",
    "ServiceRequestNotes/{action}/{ServiceRequest}/{id}",
    new { controller = "ServiceRequestNotes" },
    new { ServiceRequest = @"d+" }
    );
        }
    }
}
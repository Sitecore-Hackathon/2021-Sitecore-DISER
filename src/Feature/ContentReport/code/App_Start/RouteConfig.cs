using System.Web.Mvc;
using System.Web.Routing;

namespace SitecoreDiser.Feature.ContentReport.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(name: "ContentReport_default", url: "{controller}/{action}/{id}", defaults: new { controller = "ContentReport", action = "ContentReport", id = UrlParameter.Optional });
        }
    }
}
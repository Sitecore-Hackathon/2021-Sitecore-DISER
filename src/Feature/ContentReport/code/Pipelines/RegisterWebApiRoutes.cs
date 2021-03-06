using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SitecoreDiser.Feature.ContentReport.Pipelines
{
    public class RegisterWebApiRoutes
    {
        /// <summary>
        /// Adding web api routes for custom api's
        /// </summary>
        /// <param name="args"></param>
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapHttpRoute("SitecoreDiser.DownloadApi", "downloadapi/{action}/{id}", defaults: new { controller = "DownloadApi", id = RouteParameter.Optional });
            RouteTable.Routes.MapHttpRoute("SitecoreDiser.ReportApi", "reportapi/{action}/{id}", defaults: new { controller = "ReportApi", id = RouteParameter.Optional });
        }
    }
}
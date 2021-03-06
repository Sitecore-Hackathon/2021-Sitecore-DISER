using System.Web.Mvc;

namespace SitecoreDiser.Feature.ContentReport.Areas
{
    public class ContentReportAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "ContentReport"; } }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("ContentReport_default", "ContentReport/{controller}/{action}/{id}", new { action = "ContentReport", id = UrlParameter.Optional });
        }
    }
}
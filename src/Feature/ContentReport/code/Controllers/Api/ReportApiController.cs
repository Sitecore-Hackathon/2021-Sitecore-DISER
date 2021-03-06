using Newtonsoft.Json;
using SitecoreDiser.Extensions.Attributes;
using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Feature.ContentReport.Repositories;
using System.Web.Http;

namespace SitecoreDiser.Feature.ContentReport.Controllers.Api
{
    public class ReportApiController : ApiController
    {
        public ReportApiController()
        {

        }

        /// <summary>
        /// Api method to get Report based on request
        /// </summary>
        /// <param name="request">request object to use for filtering</param>
        /// <returns>result object</returns>
        [HttpPost]
        [AuthorizeApi(AllowAnonymous = false, AllowDatabase = AuthorizeApiAttribute.SitecoreDatabase.Master)]
        public IHttpActionResult GetReport(ReportModel request)
        {
            if (!request.IsValid())
            {
                string error = JsonConvert.SerializeObject(request.ErrorMessage, Formatting.Indented);
                return Json(error);
            }

            var result = new ContentReportRepository().GetResults(request);
            return Json(result);
        }
    }
}
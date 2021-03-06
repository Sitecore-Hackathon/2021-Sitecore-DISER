using Newtonsoft.Json;
using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Feature.ContentReport.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

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
        [System.Web.Http.HttpPost]
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
using SitecoreDiser.Feature.ContentReport.Models;
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

        [System.Web.Http.HttpPost]
        public IHttpActionResult GetReport(ReportModel request)
        {
            return null;
        }
    }
}
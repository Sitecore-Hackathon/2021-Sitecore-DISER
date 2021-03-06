﻿using SitecoreDiser.Feature.ContentReport.Repositories;
using System.Web.Mvc;

namespace SitecoreDiser.Feature.ContentReport.Controllers
{
    public class ContentReportController : Controller
    {
        private readonly IContentReportRepository _contentReportRepository;

        public ContentReportController(IContentReportRepository contentReportRepository)
        {
            _contentReportRepository = contentReportRepository;
        }

        public ActionResult ContentReport()
        {
            return View();
        }
    }
}
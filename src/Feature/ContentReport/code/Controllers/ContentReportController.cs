﻿using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Feature.ContentReport.Repositories;
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

        /// <summary>
        /// The Controller Rendering action
        /// </summary>
        /// <returns>view with model</returns>
        public ActionResult ContentReport()
        {
            return View(_contentReportRepository.GetContentReport());
        }
       
    }
}
﻿using SitecoreDiser.Feature.ContentReport.Helper;
using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Feature.ContentReport.Service;
using SitecoreDiser.Foundation.DependencyInjection;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SitecoreDiser.Feature.ContentReport.Repositories
{
    [Service(typeof(IContentReportRepository))]
    public class ContentReportRepository : IContentReportRepository
    {
        private readonly SearchService<ReportSearchResultItemModel> _searchService;
        public ContentReportRepository()
        {
            _searchService = DependencyResolver.Current.GetService<SearchService<ReportSearchResultItemModel>>();
        }

        /// <summary>
        /// Get Report Content model for View
        /// </summary>
        /// <returns>Content report model</returns>
        public ReportContentModel GetContentReport()
        {
           var model = new ReportContentModel
            {
                SearchText = "Generate Report",
                SearchLink = "", // Url of API    
                Tabs = GetTabs()
            };
            return model;
        }

        /// <summary>
        /// Method to get report results
        /// </summary>
        /// <param name="request">request object</param>
        /// <returns>Result object</returns>
        public ReportDataModel GetResults(ReportModel request)
        {
            List<ReportSearchResultItemModel> updatedItems = null;
            if (request != null && request.Type != "ArchivedItems")
                    updatedItems = _searchService.GetResults(IndexHelper.UpdatedReportPredicates(request.StartDateTime.Value, request.EndDateTime.Value), request.Page);
            return ReportHelper.GetReport(updatedItems, request);
        }

        /// <summary>
        /// Get Tabs data for Model
        /// </summary>
        /// <returns>List of Tabs</returns>
        private List<ReportTabItemModel> GetTabs()
        {
            var tabs = new List<ReportTabItemModel>
            {
                new ReportTabItemModel() { DownloadText = "", DownloadLink = "", Name = "Summary", Type = "Summary" },
                new ReportTabItemModel() { DownloadText = "Download Create Items Report", DownloadLink = "", Name = "Created Items", Type = "CreatedItems" },
                new ReportTabItemModel() { DownloadText = "Download Updated Items Report", DownloadLink = "", Name = "Updated Items", Type = "UpdatedItems" },
                new ReportTabItemModel() { DownloadText = "Download Archive Items Report", DownloadLink = "", Name = "Archived Items", Type = "ArchivedItems" }
            };
            return tabs;
        }
    }
}
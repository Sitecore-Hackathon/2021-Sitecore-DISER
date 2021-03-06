using SitecoreDiser.Feature.ContentReport.Helper;
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
        /// Get Content Report model for View
        /// </summary>
        /// <returns>Content report model</returns>
        public ContentReportModel GetContentReport()
        {
            var model = new ContentReportModel
            {
                SearchText = "Generate Report",
                SearchLink = "", // Url of API
                Tabs = GetTabs(),
            };
            return model;
        }

        public ReportTabItemModel GetResults(RequestModel request)
        {
            var reportSearchResultModel = new ReportSearchResultItemModel();
            var updatedItems = _searchService.GetResults(IndexHelper.UpdatedReportPredicates(request.StartDateTime.Value, request.EndDateTime.Value));
            return ReportHelper.GetReport(updatedItems, request);
            //return resultData;
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
                new ReportTabItemModel() { DownloadText = "Generate Create Items Report", DownloadLink = "", Name = "Created Items", Type = "Created Items" },
                new ReportTabItemModel() { DownloadText = "Generate Updated Items Report", DownloadLink = "", Name = "Updated Items", Type = "Updated Items" },
                new ReportTabItemModel() { DownloadText = "Generate Archive Items Report", DownloadLink = "", Name = "Archived Items", Type = "Archived Items" }
            };
            return tabs;
        }
    }
}
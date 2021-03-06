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
        /// Get Report Content model for View
        /// </summary>
        /// <returns>Content report model</returns>

        public ReportContentModel GetContentReport()
        {
            var model = new ReportContentModel
            {
                SearchText = "Generate Report",
                SearchLink = "", // Url of API               
            };
            return model;
        }

        /// <summary>
        /// Get Report data Post Request
        /// </summary>
        /// <returns>Content report model</returns>
        public ReportModel GetContentReport(ReportModel reportModel)
        {
            var model = new ReportModel
            {
                ReportContent = reportModel.ReportContent,
                ReportData = GetResults(reportModel)
            };
            return model;
        }

        public ReportDataModel GetResults(ReportModel request)
        {
            var reportSearchResultModel = new ReportSearchResultItemModel();
            var updatedItems = _searchService.GetResults(IndexHelper.UpdatedReportPredicates(request.StartDateTime.Value, request.EndDateTime.Value), request.Page);
            return ReportHelper.GetReport(updatedItems, request);
        }

        /// <summary>
        /// Get Tabs data for Model
        /// </summary>
        /// <returns>List of Tabs</returns>
        private List<ReportDataModel> GetTabs()
        {
            var tabs = new List<ReportDataModel>
            {
                new ReportDataModel() { DownloadText = "", DownloadLink = "", Name = "Summary", Type = "Summary" },
                new ReportDataModel() { DownloadText = "Download Create Items Report", DownloadLink = "", Name = "Created Items", Type = "Created Items" },
                new ReportDataModel() { DownloadText = "Download Updated Items Report", DownloadLink = "", Name = "Updated Items", Type = "Updated Items" },
                new ReportDataModel() { DownloadText = "Download Archive Items Report", DownloadLink = "", Name = "Archived Items", Type = "Archived Items" }
            };
            return tabs;
        }
    }
}
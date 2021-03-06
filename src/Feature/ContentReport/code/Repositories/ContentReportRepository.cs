using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Foundation.DependencyInjection;
using System.Collections.Generic;

namespace SitecoreDiser.Feature.ContentReport.Repositories
{
    [Service(typeof(IContentReportRepository))]
    public class ContentReportRepository : IContentReportRepository
    {
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

        public List<ResultModel> GetResults(RequestModel request)
        {
            return null;
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
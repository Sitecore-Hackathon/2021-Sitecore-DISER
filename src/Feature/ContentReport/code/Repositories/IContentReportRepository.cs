using SitecoreDiser.Feature.ContentReport.Models;
using System.Collections.Generic;

namespace SitecoreDiser.Feature.ContentReport.Repositories
{
    public interface IContentReportRepository
    {
        ContentReportModel GetContentReport();

        ReportTabItemModel GetResults(RequestModel request);
    }
}

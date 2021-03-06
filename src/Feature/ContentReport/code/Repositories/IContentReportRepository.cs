using SitecoreDiser.Feature.ContentReport.Models;
using System.Collections.Generic;

namespace SitecoreDiser.Feature.ContentReport.Repositories
{
    public interface IContentReportRepository
    {
        ReportContentModel GetContentReport();
        ReportDataModel GetResults(ReportModel request);

    }
}

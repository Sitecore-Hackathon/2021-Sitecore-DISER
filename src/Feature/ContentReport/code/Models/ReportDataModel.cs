using Sitecore.Data.Archiving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ReportDataModel
    {
        public string Name { get; set; }

        public string DownloadLink { get; set; }

        public string DownloadText { get; set; }

        public string Type { get; set; }

        public List<ReportSearchResultItemModel> CreatedResults { get; set; }
        public List<ReportSearchResultItemModel> UpdatedResults { get; set; }

        public List<ReportSearchResultItemModel> ArchivedItems { get; set; }

        public int NoOfResults { get; set; }

        public int CreatedPages { get; set; }
        public int UpdatedPages { get; set; }
        public int ArchivedPages { get; set; }
        public string SearchText { get; set; }
        public string SearchLink { get; set; }
    }
}
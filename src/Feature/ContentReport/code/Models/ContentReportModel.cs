using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ContentReportModel
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public List<ReportTabItemModel> Tabs { get; set; }

        public string SearchText { get; set; }

        public string SearchLink { get; set; }
    }
}
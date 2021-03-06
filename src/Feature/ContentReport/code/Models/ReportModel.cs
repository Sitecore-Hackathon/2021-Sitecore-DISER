using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ReportModel
    {
        public string StartDate { get; set; }
        public DateTime? StartDateTime => string.IsNullOrWhiteSpace(StartDate) ? (DateTime?)null : Convert.ToDateTime(StartDate).ToUniversalTime();
        public string EndDate { get; set; }
        public DateTime? EndDateTime => string.IsNullOrWhiteSpace(EndDate) ? (DateTime?)null : Convert.ToDateTime(EndDate).AddDays(1).ToUniversalTime();
        public string Type { get; set; }
        public int Page { get; set; }
        public int NoOfItems { get; set; }
        public ReportDataModel ReportData { get; set; }
        public ReportContentModel ReportContent { get; set; }
    }
}
using System;
using System.Collections.Generic;

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
        public List<string> ErrorMessage { get; set; }

        public bool IsValid()
        {
            var status = true;
            ErrorMessage = new List<string>();

            if (StartDateTime == null)
            {
                ErrorMessage.Add("From date cannot be empty");
                status = false;
            }

            if (EndDateTime == null)
            {
                ErrorMessage.Add("To date cannot be empty");
                status = false;
            }
            if (StartDateTime > EndDateTime)
            {
                ErrorMessage.Add("From date cannot be greater than To date");
                status = false;
            }

            return status;

        }
    }
}
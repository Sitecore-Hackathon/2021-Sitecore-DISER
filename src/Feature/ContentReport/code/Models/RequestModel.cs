using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class RequestModel
    {
        public string StartDate { get; set; }
        public DateTime? StartDateTime => string.IsNullOrWhiteSpace(StartDate) ? (DateTime?)null : DateTime.ParseExact(StartDate, "d/M/yyyy", CultureInfo.InvariantCulture).ToUniversalTime();
        public string EndDate { get; set; }
        public DateTime? EndDateTime => string.IsNullOrWhiteSpace(EndDate) ? (DateTime?)null : DateTime.ParseExact(EndDate, "d/M/yyyy", CultureInfo.InvariantCulture).AddDays(1).ToUniversalTime();
        public string Type { get; set; }
        public int Page { get; set; }
        public int NoOfItems { get; set; }
    }
}
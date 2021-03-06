using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class RequestModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Type { get; set; }
        public int Page { get; set; }
        public int NoOfItems { get; set; }
    }
}
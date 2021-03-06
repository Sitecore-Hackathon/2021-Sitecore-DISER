using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ArchiveResultItem
    {
        public string FullPath { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public int ItemVersion { get; set; }

        public string ItemName { get; set; }
        public ID ItemId { get; set; }

    }
}
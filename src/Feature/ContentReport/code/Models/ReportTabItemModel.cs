﻿using Sitecore.Data.Archiving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ReportTabItemModel
    {
        public string Name { get; set; }

        public string DownloadLink { get; set; }

        public string DownloadText { get; set; }

        public string Type { get; set; }

        public List<ReportSearchResultItemModel> Results { get; set; }

        public List<ArchiveEntry> ArchivedItems { get; set; }

        public int NoOfResults { get; set; }

        public int CreatedPages { get; set; }
        public int UpdatedPages { get; set; }
        public int ArchivedPages { get; set; }
    }
}
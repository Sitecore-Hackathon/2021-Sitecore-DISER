﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ReportContentModel
    {
        public string SearchText { get; set; }

        public string SearchLink { get; set; }
        
        public List<ReportTabItemModel> Tabs { get; set; }

    }
}
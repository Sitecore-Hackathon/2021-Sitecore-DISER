using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Runtime.Serialization;

namespace SitecoreDiser.Feature.ContentReport.Models
{
    public class ReportSearchResultItemModel : SearchResultItem
    {
        [IndexField(Constants.Indexes.ContentIndex.Fields.FullPath)]
        [DataMember]
        public string FullPath { get; set; }

        [IndexField(Constants.Indexes.ContentIndex.Fields.UpdatedDate)]
        [DataMember]

        public DateTime UpdatedDate { get; set; }

        [IndexField(Constants.Indexes.ContentIndex.Fields.WorkflowVersion)]
        [DataMember]
        public int ItemVersion { get; set; }

        [IndexField(Constants.Indexes.ContentIndex.Fields.WorkflowState)]
        [DataMember]
        public string WorkflowState { get; set; }
    }
}
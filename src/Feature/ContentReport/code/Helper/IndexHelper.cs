using System;
using System.Linq.Expressions;
using Sitecore.ContentSearch.Linq.Utilities;
using SitecoreDiser.Feature.ContentReport.Models;

namespace SitecoreDiser.Feature.ContentReport.Helper
{
    public static class IndexHelper
    {

        #region "Report Predicates"

        /// <summary>
        /// Generates predicates for search
        /// </summary>
        /// <param name="fromdate">start date</param>
        /// <param name="todate">end date</param>
        /// <returns>Predicates</returns>
        public static Expression<Func<ReportSearchResultItemModel, bool>> UpdatedReportPredicates(DateTime fromdate, DateTime todate)
        {
            var predicates = PredicateBuilder.True<ReportSearchResultItemModel>();

            predicates = predicates.And(x => x.UpdatedDate >= fromdate);
            predicates = predicates.And(x => x.UpdatedDate <= todate);

            if (predicates.CanReduce)
                predicates.ReduceAndCheck();

            return predicates;
        }

        #endregion

    }
}
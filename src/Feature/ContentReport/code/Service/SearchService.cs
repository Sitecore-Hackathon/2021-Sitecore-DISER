using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sitecore.ContentSearch.Linq.Utilities;
using SitecoreDiser.Foundation.DependencyInjection;
using Sitecore.ContentSearch.SearchTypes;


namespace SitecoreDiser.Feature.ContentReport.Service
{
    [Service]
    public class SearchService<T> where T : SearchResultItem
    {
        /// <summary>
        /// Method to process search 
        /// </summary>
        /// <param name="predicates">search predicates</param>
        /// <param name="page"></param>
        /// <param name="index">index to use, by default it reads for setting file</param>
        /// <returns>Search results</returns>
        public List<T> GetResults(Expression<Func<T, bool>> predicates = null, int page = -1, string index = null)
        {
            List<T> results = new List<T>();
            try
            {
                //Initialize the predicate, ignoring __Standard Values
                var basePredicate = PredicateBuilder.True<T>()
                    .And(x => x.Language == Sitecore.Context.Language.Name)
                    .And(x => !x.Name.EndsWith("__Standard Values"));

                var builtAndPredicates = PredicateBuilder.True<T>();

                if (predicates != null)
                    builtAndPredicates = builtAndPredicates.And(predicates);

                if (builtAndPredicates.CanReduce)
                    builtAndPredicates.ReduceAndCheck();

                if (index == null)
                    index = Sitecore.Configuration.Settings.GetSetting("SitecoreContentIndex");

                //Perform the search on the index
                using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
                {
                    var baseQuery = context.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)) as IOrderedQueryable<T>;
                    var fullTextPredicate = PredicateBuilder.True<T>();

                    //Applying other filters
                    baseQuery = baseQuery?.Where(basePredicate.And(builtAndPredicates)) as IOrderedQueryable<T>;

                    //Getting results from the index
                    SearchResults<T> querySearchHits;

                    querySearchHits = page <= 0 ? baseQuery.GetResults() : baseQuery.Page(page - 1, (int)10).GetResults();

                    results = querySearchHits.Select(e => e.Document).ToList();
                }
            }

            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Failed to Perform Search " + ex.Message, ex, this);
            }

            return results;
        }
    }
}
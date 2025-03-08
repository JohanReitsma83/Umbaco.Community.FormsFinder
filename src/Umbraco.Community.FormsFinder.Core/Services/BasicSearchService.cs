using Examine;
using Examine.Lucene.Search;
using Lucene.Net.Search;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Extensions;

namespace Umbraco.Community.FormsFinder.Core.Services
{
    internal interface IBasicSearchService
    {
        ISearchResults Search(IEnumerable<string> searchKeywords);
    }

    public class BasicSearchService(IExamineManager examineManager) : IBasicSearchService
    {
        
        public ISearchResults Search(IEnumerable<string> searchKeywords)
        {
            if (!examineManager.TryGetIndex(Cms.Core.Constants.UmbracoIndexes.InternalIndexName,
                    out var index))
            {
                // Throw an exception if no index is found
                Exception exception = new Exception("No index found by name " + Cms.Core.Constants.UmbracoIndexes.InternalIndexName)
                {
                    HelpLink = "null",
                    HResult = 0,
                    Source = ""
                };
                throw exception;
            }

            // Get the searcher from the index
            var searcher = index.Searcher;

            // Cast the index to IUmbracoIndex
            var umbracoIndex = index as IUmbracoIndex;
            if (umbracoIndex == null)
            {
                // Throw an exception if the cast fails
                throw new InvalidOperationException("Could not cast index to IUmbracoIndex");
            }

            // Create a query for the content index
            var criteria = searcher.CreateQuery(IndexTypes.Content);
            ((LuceneSearchQueryBase)criteria).QueryParser.AllowLeadingWildcard = true;
            ((LuceneSearchQueryBase)criteria).SearchOptions.AllowLeadingWildcard = true;
            BooleanQuery.MaxClauseCount = int.MaxValue;
            // Create a query to search within the specified root path

            var fields = GetFields(umbracoIndex);
            var examineQuery = criteria.GroupedOr(fields, searchKeywords.ToArray());

            var results = examineQuery.Execute();
            return results;
        }

        private static string[] GetFields(IUmbracoIndex index)
        {
            return index.GetCultureAndInvariantFields("*").ToArray();
        }
    }
}

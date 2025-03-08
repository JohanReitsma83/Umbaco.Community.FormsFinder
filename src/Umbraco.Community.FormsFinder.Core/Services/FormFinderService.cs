using Examine;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Security;
using Umbraco.Community.FormsFinder.Core.Extensions;
using Umbraco.Community.FormsFinder.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.FormsFinder.Core.Services;

public interface IFormFinderService
{
    FormsUsage GetFormsUsageInformation(int currentPage, string? searchPhrase  = null);
}

internal class FormFinderService(
    AppCaches appCaches,
    IBasicSearchService basicSearchService,
    IContentUserService contentUserService,
    IFormAccessService formAccessService,
    IBackOfficeSecurity backOfficeSecurity) : IFormFinderService
{
    private const int ItemsPerPage = 25;

    public FormsUsage GetFormsUsageInformation(int currentPage, string? searchPhrase = null)
    {
        var user = CurrentUser();
        var results = appCaches.RuntimeCache.GetCacheItem($"{Constants.FormsFinderCachePrefix}-{user.Id}", () =>
        {
            var formsToSearch = formAccessService
                .GetUserForms(user)
                .ToFormUsageDictionary(form => formAccessService.GetFolderHierarchy(form.FolderId).ToArray());

            var searchResults = basicSearchService.Search(formsToSearch.Keys.Select(guid => $"*{guid}*"));

            var contentList = contentUserService.GetPageInformationFromSearch(searchResults, CurrentUser());

            return CreateFormUsageInformationList(formsToSearch, searchResults, contentList);
        });
        if (!string.IsNullOrEmpty(searchPhrase))
        {
            results = results?
                .Where(r => r.Form.Name.Contains(searchPhrase, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        return new FormsUsage
        {
            Paging = GetPaging(results ?? [], currentPage),
            Forms = results != null ? results.Skip((currentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray() : []
        };
    }

    private static Paging GetPaging(List<FormUsageInformation> results, int currentPage)
    {
        var totalItems = results.Count;
        if (totalItems == 0)
        {
            return new Paging() { CurrentPage = 1, TotalPages = 1 };
        }
        var numberOfPages = (int)Math.Ceiling((double)totalItems / ItemsPerPage);
        if (currentPage > numberOfPages)
        {
            currentPage = 1;
        }

        return new Paging() { CurrentPage = currentPage, TotalPages = numberOfPages };
    }

    private IUser CurrentUser()
    {
        var user = backOfficeSecurity.CurrentUser;
        if (user == null)
        {
            throw new ArgumentException("User not found")
            {
                HelpLink = string.Empty, HResult = 0, Source = string.Empty
            };
        }

        return user;

    }

    private static List<FormUsageInformation> CreateFormUsageInformationList(
        Dictionary<Guid, FormInformation> formsToSearch,
        ISearchResults searchResults,
        Dictionary<string, PageInformation> contentList)
    {
        var results = new List<FormUsageInformation>();

        foreach (var form in formsToSearch)
        {
            var foundResults = searchResults
                .Where(r => r.Values.Values.Any(value => value.Contains(form.Key.ToString())))
                .ToArray();

            var formUsageInformation = new FormUsageInformation
            {
                Form = form.Value,
                Pages = foundResults.Any() ? MapContentInformation(contentList, foundResults) : new List<PageInformation>()
            };

            results.Add(formUsageInformation);
        }

        return results
            .OrderBy(r => r.Form.TotalPath)
            .ThenBy(r => r.Form.Name)
            .ToList();
    }

    private static List<PageInformation> MapContentInformation(
        IDictionary<string, PageInformation> contentList,
        ISearchResult[] searchResults)
    {
        var result = new List<PageInformation>();

        foreach (var searchResult in searchResults)
        {
            if (contentList.TryGetValue(searchResult.Id, out var content))
            {
                result.Add(content);
            }
        }

        return result.OrderBy(r => r.PageTitle).ToList();
    }
}

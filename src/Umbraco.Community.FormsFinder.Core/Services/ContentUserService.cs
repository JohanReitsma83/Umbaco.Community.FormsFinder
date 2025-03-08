using Examine;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.FormsFinder.Core.Models;

namespace Umbraco.Community.FormsFinder.Core.Services
{
    internal interface IContentUserService
    {
        Dictionary<string, PageInformation> GetPageInformationFromSearch(ISearchResults searchResults, IUser user);
    }

    internal class ContentUserService(IContentService contentService, ContentPermissions contentPermissions) : IContentUserService
    {
        public Dictionary<string, PageInformation> GetPageInformationFromSearch(ISearchResults searchResults,
            IUser user)
        {

            return contentService.GetByIds(searchResults.Select(s => int.Parse(s.Id)))
                .ToDictionary(c => c.Id.ToString(), content =>
                {
                    var access = contentPermissions.CheckPermissions(content, user, 'F');

                    return new PageInformation()
                    {
                        PageTitle = content.Name,
                        ContentId = content.Id,
                        Published = content.Published,
                        HasAccess = access == ContentPermissions.ContentAccess.Granted
                    };
                });
        }
    }
}

using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Extensions;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Core.Security;
using Umbraco.Forms.Core.Services;

namespace Umbraco.Community.FormsFinder.Core.Services
{
    internal interface IFormAccessService
    {
        IEnumerable<FormSlim> GetUserForms(IUser user);
        List<string> GetFolderHierarchy(Guid? folderId);
    }

    internal class FormAccessService(IFormService formService, IFormsSecurity formsSecurity, IFolderService folderService) : IFormAccessService
    {
        public IEnumerable<FormSlim> GetUserForms(IUser user)
        {
            return formService
                .GetSlim()
                .Where(form =>
                    formsSecurity.HasAccessToForm(form.Id, user));
        }

        public List<string> GetFolderHierarchy(Guid? folderId)
        {
            return GetFolderHierarchy([], folderId);
        }

        private List<string> GetFolderHierarchy(List<string> paths, Guid? folderId)
        {
            if (!folderId.HasValue)
            {
                return paths;
            }

            var folder = folderService.Get(folderId.Value);
            if (folder == null)
            {
                return paths;
            }

            paths.Add(folder.Name);

            if (folder.ParentId.HasValue)
            {
                GetFolderHierarchy(paths, folder.ParentId);
            }
            return paths;
        }
    }
}

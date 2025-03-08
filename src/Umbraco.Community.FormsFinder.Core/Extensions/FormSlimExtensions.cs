using Umbraco.Community.FormsFinder.Core.Models;
using Umbraco.Forms.Core.Models;

namespace Umbraco.Community.FormsFinder.Core.Extensions;

public static class FormSlimExtensions
{
    public static Dictionary<Guid, FormInformation> ToFormUsageDictionary(this IEnumerable<FormSlim> forms, Func<FormSlim, string[]> getFormPath)
    {
        return forms.ToDictionary(
            form => form.Id,
            form => new FormInformation()
            {
                Name = form.Name,
                Guid = form.Id.ToString(),
                Path = getFormPath(form)
            });
    }
}

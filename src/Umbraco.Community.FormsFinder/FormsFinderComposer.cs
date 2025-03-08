using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.FormsFinder.Core.Extensions;

namespace Umbraco.Community.FormsFinder;

internal class FormsFinderComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddFormFinderService().AddNotificationHandlers();
        builder.ManifestFilters().Append<FormsFinderManifestFilter>();
     
    }
}

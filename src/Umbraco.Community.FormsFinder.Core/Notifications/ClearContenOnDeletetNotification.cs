using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Community.FormsFinder.Core.Notifications;

public class ClearContenOnDeletetNotification(AppCaches appCaches) : INotificationHandler<ContentDeletedNotification>
{

    private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

    public void Handle(ContentDeletedNotification notification)
    {
        _runtimeCache.ClearByKey(Constants.FormsFinderCachePrefix);
    }
}

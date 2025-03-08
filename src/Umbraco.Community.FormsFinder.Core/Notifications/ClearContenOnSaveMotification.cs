using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Community.FormsFinder.Core.Notifications;

public class ClearContenOnSaveMotification(AppCaches appCaches) : INotificationHandler<ContentSavedNotification>
{

    private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

    public void Handle(ContentSavedNotification notification)
    {
        _runtimeCache.ClearByKey(Constants.FormsFinderCachePrefix);
    }
}

using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Forms.Core.Services.Notifications;

namespace Umbraco.Community.FormsFinder.Core.Notifications;

public class FormClearOnSavedNotification(AppCaches appCaches) : INotificationHandler<FormSavedNotification>
{
    private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

    public void Handle(FormSavedNotification notification)
    {
        _runtimeCache.ClearByKey(Constants.FormsFinderCachePrefix);
    }
}

using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Forms.Core.Services.Notifications;

namespace Umbraco.Community.FormsFinder.Core.Notifications
{
    public class FormClearOnDeleteNotification(AppCaches appCaches) : INotificationHandler<FormDeletedNotification>
    {
        private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

        public void Handle(FormDeletedNotification notification)
        {
            _runtimeCache.ClearByKey(Constants.FormsFinderCachePrefix);
        }
    }

}

using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.FormsFinder.Core.Notifications;
using Umbraco.Community.FormsFinder.Core.Services;
using Umbraco.Forms.Core.Services.Notifications;

namespace Umbraco.Community.FormsFinder.Core.Extensions;

public static class UmbracoBuilderExtensions
{
    public static IUmbracoBuilder AddFormFinderService(this IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<IFormAccessService, FormAccessService>();
        builder.Services.AddScoped<IContentUserService, ContentUserService>();
        builder.Services.AddScoped<IBasicSearchService, BasicSearchService>();
        builder.Services.AddScoped<IFormFinderService, FormFinderService>();
        return builder;
    }

    public static IUmbracoBuilder AddNotificationHandlers(this IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<ContentSavedNotification, ClearContenOnSaveMotification>();
        builder.AddNotificationHandler<ContentDeletedNotification, ClearContenOnDeletetNotification>();

        
        builder.AddNotificationHandler<FormSavedNotification, FormClearOnSavedNotification>();
        builder.AddNotificationHandler<FormDeletedNotification, FormClearOnDeleteNotification>();
        return builder;
    }

}

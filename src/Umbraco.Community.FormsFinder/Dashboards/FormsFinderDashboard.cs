using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Umbraco.Community.FormsFinder.Dashboards;

[Weight(-10)]
public class FormsFinderDashboard : IDashboard
{
    
    public string Alias => Constants.Alias;

    public string[] Sections =>
    [
        Cms.Core.Constants.Applications.Forms
    ];

    public string View => $"/App_Plugins/{Constants.PackageName}/Views/dashboard.html?{Constants.VersionNumber}";

    public IAccessRule[] AccessRules
    {
        get
        {
            var rules = new IAccessRule[]
            {
                new AccessRule{Type = AccessRuleType.GrantBySection, Value = Cms.Core.Constants.Applications.Forms}
			};
            return rules;
        }
    }
    
}

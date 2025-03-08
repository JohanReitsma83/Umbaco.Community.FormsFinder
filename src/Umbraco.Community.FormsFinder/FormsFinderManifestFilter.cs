using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.FormsFinder;

internal class FormsFinderManifestFilter : IManifestFilter
{
    public void Filter(List<PackageManifest> manifests)
    {
        var assembly = typeof(FormsFinderManifestFilter).Assembly;

        manifests.Add(new PackageManifest
        {
            PackageName = Constants.PackageName,
            Version = assembly.GetName().Version?.ToString(3) ?? "0.1.0",
            AllowPackageTelemetry = true,
            BundleOptions = BundleOptions.None,
            Scripts =
            [
                $"/App_Plugins/{Constants.PackageName}/Scripts/Controllers/Dashboards/default.js"
            ],
            Stylesheets =
            [

            ]
        });
    }
}

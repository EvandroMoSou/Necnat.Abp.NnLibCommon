using Volo.Abp.Bundling;

namespace Necnat.Abp.NnLibCommon.Blazor.Host;

public class NnLibCommonBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}

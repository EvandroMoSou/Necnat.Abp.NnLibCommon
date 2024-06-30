using Volo.Abp.Bundling;

namespace Necnat.Abp.NnLibCommon.Blazor.Host.Client;

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

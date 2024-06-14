using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Necnat.Abp.NnLibCommon.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class NnLibCommonBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "NnLibCommon";
}

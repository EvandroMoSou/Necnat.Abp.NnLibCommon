using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon;

[Dependency(ReplaceServices = true)]
public class NnLibCommonBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "NnLibCommon";
}

using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Necnat.Abp.NnLibCommon.Blazor.Server.Host;

public abstract class NnLibCommonComponentBase : AbpComponentBase
{
    protected NnLibCommonComponentBase()
    {
        LocalizationResource = typeof(NnLibCommonResource);
    }
}

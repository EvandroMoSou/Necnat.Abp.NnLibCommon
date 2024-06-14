using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Necnat.Abp.NnLibCommon;

public abstract class NnLibCommonController : AbpControllerBase
{
    protected NnLibCommonController()
    {
        LocalizationResource = typeof(NnLibCommonResource);
    }
}

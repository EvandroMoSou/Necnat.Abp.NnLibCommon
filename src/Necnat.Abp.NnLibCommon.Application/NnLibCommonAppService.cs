using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon;

public abstract class NnLibCommonAppService : ApplicationService
{
    protected NnLibCommonAppService()
    {
        LocalizationResource = typeof(NnLibCommonResource);
        ObjectMapperContext = typeof(NnLibCommonApplicationModule);
    }
}

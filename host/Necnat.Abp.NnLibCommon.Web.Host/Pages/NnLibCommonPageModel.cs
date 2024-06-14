using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Necnat.Abp.NnLibCommon.Pages;

public abstract class NnLibCommonPageModel : AbpPageModel
{
    protected NnLibCommonPageModel()
    {
        LocalizationResourceType = typeof(NnLibCommonResource);
    }
}

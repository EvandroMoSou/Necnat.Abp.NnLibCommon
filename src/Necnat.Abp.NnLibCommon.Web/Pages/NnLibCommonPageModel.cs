using Necnat.Abp.NnLibCommon.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Necnat.Abp.NnLibCommon.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class NnLibCommonPageModel : AbpPageModel
{
    protected NnLibCommonPageModel()
    {
        LocalizationResourceType = typeof(NnLibCommonResource);
        ObjectMapperContext = typeof(NnLibCommonWebModule);
    }
}

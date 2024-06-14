using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Necnat.Abp.NnLibCommon.Pages;

public class IndexModel : NnLibCommonPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}

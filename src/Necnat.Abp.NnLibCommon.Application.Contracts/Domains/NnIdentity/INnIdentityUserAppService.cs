using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public interface INnIdentityUserAppService :
        ICrudAppService<
            IdentityUserDto,
            Guid,
            NnIdentityUserResultRequestDto>
    {
        Task<IdentityUserDto> GetMyAsync(Guid id);
    }
}
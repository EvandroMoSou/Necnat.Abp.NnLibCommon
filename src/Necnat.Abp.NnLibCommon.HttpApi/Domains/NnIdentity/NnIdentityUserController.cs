using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Necnat.Abp.NnLibCommon.Controllers;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    [RemoteService(Name = NnLibCommonRemoteServiceConsts.RemoteServiceName)]
    [Area(NnLibCommonRemoteServiceConsts.ModuleName)]
    [ControllerName("NnIdentityUser")]
    [Route("api/nn-lib-common/nn-identity-user")]
    public class NnIdentityUserController : NecnatController<INnIdentityUserAppService, NnIdentityUserDto, Guid, NnIdentityUserResultRequestDto>, INnIdentityUserAppService
    {
        public NnIdentityUserController(INnIdentityUserAppService appService) : base(appService)
        {
        }

        [HttpGet]
        [Route("{id}/my")]
        public virtual Task<NnIdentityUserDto> GetMyAsync(Guid id)
        {
            return AppService.GetMyAsync(id);
        }
    }
}

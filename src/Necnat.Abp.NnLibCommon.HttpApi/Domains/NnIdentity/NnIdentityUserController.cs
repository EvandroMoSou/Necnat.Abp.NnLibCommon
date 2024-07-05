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
    [Route("api/NnLibCommon/NnIdentityUser")]
    public class NnIdentityUserController : NecnatController<INnIdentityUserAppService, IdentityUserDto, Guid, NnIdentityUserResultRequestDto>, INnIdentityUserAppService
    {
        public NnIdentityUserController(INnIdentityUserAppService appService) : base(appService)
        {
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<IdentityUserDto> GetMyAsync(Guid id)
        {
            return AppService.GetMyAsync(id);
        }
    }
}

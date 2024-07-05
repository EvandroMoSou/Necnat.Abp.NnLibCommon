using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Necnat.Abp.NnLibCommon.Controllers;
using System;
using Volo.Abp;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    [RemoteService(Name = NnLibCommonRemoteServiceConsts.RemoteServiceName)]
    [Area(NnLibCommonRemoteServiceConsts.ModuleName)]
    [ControllerName("NnIdentityRole")]
    [Route("api/NnLibCommon/NnIdentityRole")]
    public class NnIdentityRoleController : NecnatController<INnIdentityRoleAppService, IdentityRoleDto, Guid, NnIdentityRoleResultRequestDto>, INnIdentityRoleAppService
    {
        public NnIdentityRoleController(INnIdentityRoleAppService appService) : base(appService)
        {
        }
    }
}

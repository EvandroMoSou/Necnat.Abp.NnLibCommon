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
    [Route("api/nn-lib-common/nn-identity-role")]
    public class NnIdentityRoleController : NecnatController<INnIdentityRoleAppService, NnIdentityRoleDto, Guid, NnIdentityRoleResultRequestDto>, INnIdentityRoleAppService
    {
        public NnIdentityRoleController(INnIdentityRoleAppService appService) : base(appService)
        {
        }
    }
}

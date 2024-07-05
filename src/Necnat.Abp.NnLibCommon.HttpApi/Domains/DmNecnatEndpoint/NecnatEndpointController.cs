using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Necnat.Abp.NnLibCommon.Controllers;
using System;
using Volo.Abp;

namespace Necnat.Abp.NnLibCommon.Domains.DmNecnatEndpoint
{
    [RemoteService(Name = NnLibCommonRemoteServiceConsts.RemoteServiceName)]
    [Area(NnLibCommonRemoteServiceConsts.ModuleName)]
    [ControllerName("NecnatEndpoint")]
    [Route("api/NnLibCommon/NecnatEndpoint")]
    public class NecnatEndpointController : NecnatController<INecnatEndpointAppService, NecnatEndpointDto, Guid, NecnatEndpointResultRequestDto>, INecnatEndpointAppService
    {
        public NecnatEndpointController(INecnatEndpointAppService appService) : base(appService)
        {
        }
    }
}

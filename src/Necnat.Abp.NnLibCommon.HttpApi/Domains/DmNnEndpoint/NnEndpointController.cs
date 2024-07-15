﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Necnat.Abp.NnLibCommon.Controllers;
using System;
using Volo.Abp;

namespace Necnat.Abp.NnLibCommon.Domains.DmNecnatEndpoint
{
    [RemoteService(Name = NnLibCommonRemoteServiceConsts.RemoteServiceName)]
    [Area(NnLibCommonRemoteServiceConsts.ModuleName)]
    [ControllerName("NnEndpoint")]
    [Route("api/nn-lib-common/nn-endpoint")]
    public class NnEndpointController : NecnatController<INnEndpointAppService, NnEndpointDto, Guid, NnEndpointResultRequestDto>, INnEndpointAppService
    {
        public NnEndpointController(INnEndpointAppService appService) : base(appService)
        {
        }
    }
}

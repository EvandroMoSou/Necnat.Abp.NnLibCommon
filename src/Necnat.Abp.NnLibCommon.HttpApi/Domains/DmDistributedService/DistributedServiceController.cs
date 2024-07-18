using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Necnat.Abp.NnLibCommon.Controllers;
using System;
using Volo.Abp;

namespace Necnat.Abp.NnLibCommon.Domains.DmNecnatEndpoint
{
    [RemoteService(Name = NnLibCommonRemoteServiceConsts.RemoteServiceName)]
    [Area(NnLibCommonRemoteServiceConsts.ModuleName)]
    [ControllerName("NnEndpoint")]
    [Route("api/nn-lib-common/distributed-service")]
    public class DistributedServiceController : NecnatController<IDistributedServiceDtoAppService, DistributedServiceDto, Guid, DistributedServiceResultRequestDto>, IDistributedServiceDtoAppService
    {
        public DistributedServiceController(IDistributedServiceDtoAppService appService) : base(appService)
        {
        }
    }
}

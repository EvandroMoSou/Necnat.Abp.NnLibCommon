using System;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface IDistributedServiceDtoAppService :
        ICrudAppService<
            DistributedServiceDto,
            Guid,
            DistributedServiceResultRequestDto>
    {

    }
}
using System;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INecnatEndpointAppService :
        ICrudAppService<
            NecnatEndpointDto,
            Guid,
            NecnatEndpointResultRequestDto>
    {

    }
}
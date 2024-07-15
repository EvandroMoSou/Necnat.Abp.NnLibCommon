using System;
using Volo.Abp.Application.Services;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INnEndpointAppService :
        ICrudAppService<
            NnEndpointDto,
            Guid,
            NnEndpointResultRequestDto>
    {

    }
}
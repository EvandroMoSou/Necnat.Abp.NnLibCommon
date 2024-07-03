using Necnat.Abp.NnLibCommon.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Blazor.Services
{
    public interface INecnatEndpointClientService
    {
        Task<List<NecnatEndpointDto>> GetListAsync();
    }
}

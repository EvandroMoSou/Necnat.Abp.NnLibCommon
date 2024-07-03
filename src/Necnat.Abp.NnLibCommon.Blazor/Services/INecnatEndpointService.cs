using Necnat.Abp.NnLibCommon.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Blazor.Services
{
    public interface INecnatEndpointService
    {
        Task<IReadOnlyList<NecnatEndpointDto>> GetListAsync();
        Task<NecnatEndpointDto> GetByPermissionsGroupNameAsync(string permissionsGroupName);
    }
}

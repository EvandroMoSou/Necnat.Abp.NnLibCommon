using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INecnatEndpointRepository : IRepository<NecnatEndpoint, Guid>
    {
        Task<NecnatEndpoint> GetByPermissionsGroupNameAsync(string permissionsGroupName);
    }
}

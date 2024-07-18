using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface IDistributedServiceStore
    {
        Task<List<DistributedServiceModel>> GetListAsync(string? applicationName = null, string? tag = null, bool isActive = true);    }
}
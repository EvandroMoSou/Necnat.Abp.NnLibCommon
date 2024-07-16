using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INnEndpointStore
    {
        Task<List<NnEndpointModel>> GetListAsync(bool isActive = true);
        Task<List<NnEndpointModel>> GetListByTagAsync(string tag, bool isActive = true);
    }
}

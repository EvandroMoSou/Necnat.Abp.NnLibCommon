using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INecnatEndpointStore
    {
        Task<List<NecnatEndpoint>> GetListAsync();
    }
}

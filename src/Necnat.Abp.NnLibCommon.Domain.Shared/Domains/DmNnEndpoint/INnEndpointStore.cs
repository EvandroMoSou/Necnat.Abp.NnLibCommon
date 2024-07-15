using Necnat.Abp.NnLibCommon.Domains.DmNnEndpoint;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INnEndpointStore
    {
        Task<List<NnEndpointModel>> GetListByTag(string tag, bool isActive = true);
    }
}

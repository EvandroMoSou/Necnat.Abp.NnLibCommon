using System.Collections.Generic;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INecnatEndpointStore
    {
        Task<string?> FindAuthServerEndpointAsync(bool isActive = true);
        Task<List<KeyValuePair<string, string>>> GetListAuthorizationEndpointAsync(bool isActive = true);
        Task<List<string>> GetListBillingEndpointAsync(bool isActive = true);
        Task<List<KeyValuePair<short, string>>> GetListHierarchyComponentEndpointAsync(bool isActive = true);
    }
}

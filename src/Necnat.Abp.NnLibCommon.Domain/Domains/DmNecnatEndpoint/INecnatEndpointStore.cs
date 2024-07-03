using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public interface INecnatEndpointStore
    {
        Task<string> GetEndpointByPermissionsGroupNameAsync(string permissionsGroupName);
    }
}

using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Helpers.Callers
{
    public interface IRequestToken
    {
        Task<string> GetAccessTokenAsync();
    }
}

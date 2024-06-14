using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Helpers.AccessTokenManager
{
    public interface IAccessTokenManager
    {
        Task<TokenResponse> GetAuthenticationTokenAsync();
    }
}

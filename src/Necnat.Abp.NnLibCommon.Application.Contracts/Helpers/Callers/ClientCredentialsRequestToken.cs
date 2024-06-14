//using Microsoft.Extensions.Configuration;
//using Necnat.Abp.NnLibCommon.Helpers.AccessTokenManager;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Necnat.Abp.NnLibCommon.Helpers.Callers
//{
//    public class ClientCredentialsRequestToken : IRequestToken
//    {
//        protected readonly IConfiguration _configuration;

//        protected TokenResponse? TokenResponse { get; set; }
//        protected DateTime ExpiryTime { get; set; }

//        public ClientCredentialsRequestToken(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public virtual async Task<string> GetAccessTokenAsync()
//        {
//            return (await GetTokenResponseAsync()).AccessToken!;
//        }

//        protected virtual async Task<TokenResponse> GetTokenResponseAsync()
//        {
//            if (TokenResponse != null && ExpiryTime > DateTime.UtcNow)
//                return TokenResponse;

//            var client = new HttpClient();
//            var discoveryDoc = await client.GetDiscoveryDocumentAsync($"{_configuration["AuthServer:Authority"]}/.well-known/openid-configuration");
//            if (discoveryDoc.IsError)
//                throw new ApplicationException(discoveryDoc.Error);

//            var tokenEndpoint = discoveryDoc.TokenEndpoint;
//            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
//               new ClientCredentialsTokenRequest()
//               {
//                   Address = tokenEndpoint,
//                   ClientId = _configuration["AuthServer:ClientId"]!,
//                   ClientSecret = _configuration["AuthServer:ClientSecret"],
//                   Scope = _configuration["AuthServer:Scope"]
//               });

//            TokenResponse = tokenResponse;
//            ExpiryTime = DateTime.UtcNow.AddSeconds(TokenResponse.ExpiresIn);

//            return TokenResponse;
//        }
//    }
//}

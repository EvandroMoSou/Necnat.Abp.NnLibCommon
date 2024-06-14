//using IdentityModel.Client;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Necnat.Abp.NnLibCommon.Helpers
//{
//    public class HttpService
//    {
//        protected readonly IConfiguration _configuration;

//        public HttpService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public async Task<Lazy<HttpClient>> GetHttpClientAsync(string authApiEndpoint, bool setBearerToken = true)
//        {
//            var client = new Lazy<HttpClient>(() => new HttpClient());

//            if (setBearerToken) client.Value.SetBearerToken(await GetAccessToken(authApiEndpoint));

//            client.Value.BaseAddress = new Uri(authApiEndpoint);
//            return await Task.FromResult(client);
//        }

//        private async Task<TokenResponse> RequestPasswordTokenAsync(string apiEndpoint)
//        {
//            var discoveryCache = new DiscoveryCache(apiEndpoint);
//            var disco = await discoveryCache.GetAsync();
//            var httpClient = new Lazy<HttpClient>(() => new HttpClient());
//            var response = await httpClient.Value.RequestPasswordTokenAsync(new PasswordTokenRequest
//            {
//                Address = disco.TokenEndpoint,
//                ClientId = _configuration["HttpService:ClientId"]!,
//                ClientSecret = _configuration["HttpService:ClientSecret"]!,
//                UserName = _configuration["HttpService:UserName"]!,
//                Password = _configuration["HttpService:Password"]!,
//                Scope = _configuration["HttpService:Scope"],
//            });
//            return response.IsError ? new TokenResponse() : response;
//        }

//        private async Task<string> GetAccessToken(string apiEndpoint) => (await RequestPasswordTokenAsync(apiEndpoint)).AccessToken!;
//    }
//}

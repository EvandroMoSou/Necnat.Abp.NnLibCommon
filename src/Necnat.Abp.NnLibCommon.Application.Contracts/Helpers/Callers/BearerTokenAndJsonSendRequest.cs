using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Helpers.Callers
{
    public class BearerTokenAndJsonSendRequest : SendRequest, ISendRequest
    {
        protected readonly IConfiguration _configuration;
        protected readonly IRequestToken _requestToken;

        public BearerTokenAndJsonSendRequest(
            IConfiguration configuration,
            IRequestToken requestToken)
        {
            _configuration = configuration;
            _requestToken = requestToken;
        }

        public virtual async Task<HttpResponseMessage> DeleteAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await _requestToken.GetAccessTokenAsync()));
                return await httpClient.DeleteAsync(GenerateRequestUri(requestUri, routeParameters, queryParameters));
            }
        }

        public virtual async Task<HttpResponseMessage> GetAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await _requestToken.GetAccessTokenAsync()));
                return await httpClient.GetAsync(GenerateRequestUri(requestUri, routeParameters, queryParameters));
            }
        }

        public virtual async Task<HttpResponseMessage> PostAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await _requestToken.GetAccessTokenAsync()));
                return await httpClient.PostAsync(GenerateRequestUri(requestUri, routeParameters, queryParameters), null);
            }
        }

        public virtual async Task<HttpResponseMessage> PostAsync<TValue>(string requestUri, TValue value, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent? httpContent = null;
                if (value != null)
                {
                    string json = JsonSerializer.Serialize(value);
                    httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await _requestToken.GetAccessTokenAsync()));
                return await httpClient.PostAsync(GenerateRequestUri(requestUri, routeParameters, queryParameters), httpContent);
            }
        }

        public virtual async Task<HttpResponseMessage> PutAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await _requestToken.GetAccessTokenAsync()));
                return await httpClient.PutAsync(GenerateRequestUri(requestUri, routeParameters, queryParameters), null);
            }
        }

        public virtual async Task<HttpResponseMessage> PutAsync<TValue>(string requestUri, TValue value, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent? httpContent = null;
                if (value != null)
                {
                    string json = JsonSerializer.Serialize(value);
                    httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await _requestToken.GetAccessTokenAsync()));
                return await httpClient.PutAsync(GenerateRequestUri(requestUri, routeParameters, queryParameters), httpContent);
            }
        }
    }
}

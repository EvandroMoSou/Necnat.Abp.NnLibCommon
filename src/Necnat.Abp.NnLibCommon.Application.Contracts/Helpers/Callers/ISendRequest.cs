using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Helpers.Callers
{
    public interface ISendRequest
    {
        Task<HttpResponseMessage> DeleteAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null);
        Task<HttpResponseMessage> GetAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null);
        Task<HttpResponseMessage> PostAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null);
        Task<HttpResponseMessage> PostAsync<TValue>(string requestUri, TValue value, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null);
        Task<HttpResponseMessage> PutAsync(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null);
        Task<HttpResponseMessage> PutAsync<TValue>(string requestUri, TValue value, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null);
        Task ProcessHttpResponseMessageAsync(HttpResponseMessage httpResponseMessage);
        Task<T> ProcessHttpResponseMessageAsync<T>(HttpResponseMessage httpResponseMessage);
    }
}

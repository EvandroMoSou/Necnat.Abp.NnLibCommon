using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Helpers.Callers
{
    public abstract class SendRequest
    {
        protected virtual string GenerateRequestUri(string requestUri, string[]? routeParameters = null, KeyValuePair<string, string>[]? queryParameters = null)
        {
            if (routeParameters != null)
                foreach (var iRouteParameter in routeParameters)
                    requestUri = requestUri + "/" + iRouteParameter;

            if (queryParameters != null)
                for (int i = 0; i < queryParameters.Length; i++)
                    requestUri = requestUri + (i == 0 ? "?" : "&") + queryParameters[i].Key + "=" + queryParameters[i].Value;

            return requestUri;
        }

        public virtual async Task ProcessHttpResponseMessageAsync(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(await httpResponseMessage.Content.ReadAsStringAsync());
            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new ApplicationException(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public virtual async Task<T> ProcessHttpResponseMessageAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(await httpResponseMessage.Content.ReadAsStringAsync());
            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new ApplicationException(await httpResponseMessage.Content.ReadAsStringAsync());

            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync())!;
        }
    }
}

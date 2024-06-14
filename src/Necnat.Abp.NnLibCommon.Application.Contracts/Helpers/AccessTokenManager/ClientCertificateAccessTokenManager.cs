using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace Necnat.Abp.NnLibCommon.Helpers.AccessTokenManager
{
    public class ClientCertificateAccessTokenManager : IAccessTokenManager
    {
        protected readonly string _certificatePath;
        protected readonly string _certificatePassword;
        protected readonly string _tokenEndpoint;

        protected TokenResponse? _accessToken;

        public ClientCertificateAccessTokenManager(string tokenEndpoint, string certificatePath, string certificatePassword)
        {
            _tokenEndpoint = tokenEndpoint;
            _certificatePath = certificatePath;
            _certificatePassword = certificatePassword;
        }

        public async Task<TokenResponse> GetAuthenticationTokenAsync()
        {
            if (_accessToken == null || (_accessToken.CalculatedExpiresIn != null && DateTime.Now > ((DateTime)_accessToken.CalculatedExpiresIn).AddSeconds(-10)))
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.SslProtocols = SslProtocols.Tls12;
                handler.ClientCertificates.Add(new X509Certificate2(_certificatePath, _certificatePassword, X509KeyStorageFlags.MachineKeySet));

                var client = new HttpClient(handler);
                var result = await client.GetAsync(_tokenEndpoint);
                var resultString = await result.Content.ReadAsStringAsync();

                _accessToken = JsonSerializer.Deserialize<TokenResponse>(resultString)!;

                if (!string.IsNullOrWhiteSpace(_accessToken.Error))
                    throw new AccessTokenException(_accessToken.Error);

                if (string.IsNullOrWhiteSpace(_accessToken.AccessToken))
                    throw new AccessTokenException("access_token null.");

                if (_accessToken.ExpiresIn != null)
                    _accessToken.CalculatedExpiresIn = DateTime.Now.AddSeconds(_accessToken.ExpiresIn.Value);
            }

            return _accessToken;
        }
    }
}

using System;
using System.Text.Json.Serialization;

namespace Necnat.Abp.NnLibCommon.Helpers.AccessTokenManager
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("scope")]
        public string? Ccope { get; set; }
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        [JsonPropertyName("expires_in")]
        public int? ExpiresIn { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        public DateTime? CalculatedExpiresIn { get; set; }
    }
}

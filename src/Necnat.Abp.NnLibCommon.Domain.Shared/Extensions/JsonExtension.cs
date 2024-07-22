using System.Text.Json;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class JsonExtension
    {
        private static JsonSerializerOptions _caseInsensitiveSerializerSettings = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static T? Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public static T? DeserializeWithOptions<T>(this string json, JsonSerializerOptions settings)
        {
            return JsonSerializer.Deserialize<T>(json, settings);
        }

        public static T? DeserializeCaseInsensitive<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, _caseInsensitiveSerializerSettings);
        }
    }
}

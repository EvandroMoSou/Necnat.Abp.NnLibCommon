using System;
using System.Text.Json;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class ObjectExtension
    {
        public static DateTime? ToNullableDateTime(this object obj)
        {
            var serializeValue = JsonSerializer.Serialize(obj);
            serializeValue = serializeValue.Replace("[", string.Empty).Replace("]", string.Empty);
            return (DateTime?)(serializeValue == "null" ? null : JsonSerializer.Deserialize<DateTime>(serializeValue));
        }

        public static DateTimeOffset? ToNullableDateTimeOffset(this object obj)
        {
            var serializeValue = JsonSerializer.Serialize(obj);
            serializeValue = serializeValue.Replace("[", string.Empty).Replace("]", string.Empty);
            return (DateTimeOffset?)(serializeValue == "null" ? null : JsonSerializer.Deserialize<DateTimeOffset>(serializeValue));
        }
    }
}

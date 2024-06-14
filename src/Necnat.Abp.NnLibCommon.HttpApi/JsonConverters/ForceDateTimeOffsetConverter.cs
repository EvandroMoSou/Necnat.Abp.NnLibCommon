using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Necnat.Abp.NnLibCommon.JsonConverters
{
    public class ForceDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Debug.Assert(typeToConvert == typeof(DateTimeOffset));
            if (!DateTimeOffset.TryParseExact(reader.GetString()!, "yyyy-MM-ddTHH:mm:ss.ffffffzzz", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out DateTimeOffset result))
                throw new Exception("Dates should not be in the format: 'Date and Time with Offset' - Ex: '2000-12-31T23:59:59.999999-03:00' - C# format: yyyy-MM-ddTHH:mm:ss.ffffffzzz.");

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz"));
        }
    }
}

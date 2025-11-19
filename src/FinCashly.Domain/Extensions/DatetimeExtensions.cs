using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinCashly.Domain.Extensions;

public static class DateTimeExtensions
{
    public static TimeZoneInfo GetBrazilTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }
        catch
        {
            return TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
        }
    }

    public class DateTimeOffsetToBrazilConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToOffset(TimeSpan.FromHours(-3)));
        }
    }
}

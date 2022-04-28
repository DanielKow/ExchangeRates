using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExchangeRatesSource.DomainLayer;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private readonly string? _dateFormat;

    public DateOnlyJsonConverter(string? dateFormat = null)
    {
        _dateFormat = dateFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        
        return DateOnly.Parse(stringValue!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormat));
    }
}
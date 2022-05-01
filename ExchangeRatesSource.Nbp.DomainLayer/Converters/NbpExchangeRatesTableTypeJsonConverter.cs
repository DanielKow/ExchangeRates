using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExchangeRatesSource.Nbp.DomainLayer.Converters;

public class NbpExchangeRatesTableTypeJsonConverter : JsonConverter<NbpExchangeRatesTableType>
{
    public override NbpExchangeRatesTableType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();

        return stringValue switch
        {
            "A" => NbpExchangeRatesTableType.A,
            "B" => NbpExchangeRatesTableType.B,
            "C" => NbpExchangeRatesTableType.C,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override void Write(Utf8JsonWriter writer, NbpExchangeRatesTableType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
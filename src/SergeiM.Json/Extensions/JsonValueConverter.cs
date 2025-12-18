using System.Text.Json;
using System.Text.Json.Serialization;

namespace SergeiM.Json.Extensions;

/// <summary>
/// JSON converter for <see cref="JsonValue"/> that integrates with System.Text.Json serialization.
/// </summary>
public sealed class JsonValueConverter : JsonConverter<JsonValue>
{
    /// <summary>
    /// Reads and converts the JSON to a <see cref="JsonValue"/>.
    /// </summary>
    public override JsonValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.ToJsonValue();
    }

    /// <summary>
    /// Writes a <see cref="JsonValue"/> as JSON.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, JsonValue value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        WriteJsonValue(writer, value);
    }

    private static void WriteJsonValue(Utf8JsonWriter writer, JsonValue value)
    {
        switch (value.ValueType)
        {
            case JsonValueType.Object:
                WriteJsonObject(writer, (JsonObject)value);
                break;
            case JsonValueType.Array:
                WriteJsonArray(writer, (JsonArray)value);
                break;
            case JsonValueType.String:
                writer.WriteStringValue(((JsonString)value).Value);
                break;
            case JsonValueType.Number:
                WriteJsonNumber(writer, (JsonNumber)value);
                break;
            case JsonValueType.Boolean:
                writer.WriteBooleanValue(((JsonBoolean)value).Value);
                break;
            case JsonValueType.Null:
                writer.WriteNullValue();
                break;
        }
    }

    private static void WriteJsonObject(Utf8JsonWriter writer, JsonObject jsonObject)
    {
        writer.WriteStartObject();
        foreach (var property in jsonObject)
        {
            writer.WritePropertyName(property.Key);
            WriteJsonValue(writer, property.Value);
        }
        writer.WriteEndObject();
    }

    private static void WriteJsonArray(Utf8JsonWriter writer, JsonArray jsonArray)
    {
        writer.WriteStartArray();
        foreach (var item in jsonArray)
        {
            WriteJsonValue(writer, item);
        }
        writer.WriteEndArray();
    }

    private static void WriteJsonNumber(Utf8JsonWriter writer, JsonNumber jsonNumber)
    {
        if (jsonNumber.IsIntegral())
        {
            try
            {
                writer.WriteNumberValue(jsonNumber.LongValue());
                return;
            }
            catch (OverflowException)
            {
                // Fall through to decimal
            }
        }
        writer.WriteNumberValue(jsonNumber.DecimalValue());
    }
}

using System.Collections.Immutable;
using System.Text.Json;

namespace SergeiM.Json.Extensions;

/// <summary>
/// Extension methods for integrating with System.Text.Json types.
/// </summary>
public static class SystemTextJsonExtensions
{
    /// <summary>
    /// Converts a <see cref="JsonElement"/> to an immutable <see cref="JsonValue"/>.
    /// </summary>
    /// <param name="element">The JSON element to convert.</param>
    /// <returns>An immutable JSON value.</returns>
    public static JsonValue ToJsonValue(this JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Object => ToJsonObject(element),
            JsonValueKind.Array => ToJsonArray(element),
            JsonValueKind.String => new JsonString(element.GetString()!),
            JsonValueKind.Number => ToJsonNumber(element),
            JsonValueKind.True => JsonValue.True,
            JsonValueKind.False => JsonValue.False,
            JsonValueKind.Null => JsonValue.Null,
            _ => throw new JsonException($"Unsupported JSON value kind: {element.ValueKind}")
        };
    }

    /// <summary>
    /// Converts a <see cref="JsonDocument"/> to an immutable <see cref="JsonValue"/>.
    /// </summary>
    /// <param name="document">The JSON document to convert.</param>
    /// <returns>An immutable JSON value.</returns>
    public static JsonValue ToJsonValue(this JsonDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        return document.RootElement.ToJsonValue();
    }

    /// <summary>
    /// Converts an immutable <see cref="JsonValue"/> to a <see cref="JsonElement"/>.
    /// </summary>
    /// <param name="value">The JSON value to convert.</param>
    /// <returns>A JSON element.</returns>
    public static JsonElement ToJsonElement(this JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var json = value.ToString();
        using var document = JsonDocument.Parse(json);
        return document.RootElement.Clone();
    }

    /// <summary>
    /// Converts an immutable <see cref="JsonValue"/> to a <see cref="JsonDocument"/>.
    /// </summary>
    /// <param name="value">The JSON value to convert.</param>
    /// <returns>A JSON document. The caller is responsible for disposing it.</returns>
    public static JsonDocument ToJsonDocument(this JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var json = value.ToString();
        return JsonDocument.Parse(json);
    }

    /// <summary>
    /// Converts an immutable <see cref="JsonObject"/> to a <see cref="JsonElement"/>.
    /// </summary>
    /// <param name="jsonObject">The JSON object to convert.</param>
    /// <returns>A JSON element.</returns>
    public static JsonElement ToJsonElement(this JsonObject jsonObject)
    {
        return ((JsonValue)jsonObject).ToJsonElement();
    }

    /// <summary>
    /// Converts an immutable <see cref="JsonObject"/> to a <see cref="JsonDocument"/>.
    /// </summary>
    /// <param name="jsonObject">The JSON object to convert.</param>
    /// <returns>A JSON document. The caller is responsible for disposing it.</returns>
    public static JsonDocument ToJsonDocument(this JsonObject jsonObject)
    {
        return ((JsonValue)jsonObject).ToJsonDocument();
    }

    /// <summary>
    /// Converts an immutable <see cref="JsonArray"/> to a <see cref="JsonElement"/>.
    /// </summary>
    /// <param name="jsonArray">The JSON array to convert.</param>
    /// <returns>A JSON element.</returns>
    public static JsonElement ToJsonElement(this JsonArray jsonArray)
    {
        return ((JsonValue)jsonArray).ToJsonElement();
    }

    /// <summary>
    /// Converts an immutable <see cref="JsonArray"/> to a <see cref="JsonDocument"/>.
    /// </summary>
    /// <param name="jsonArray">The JSON array to convert.</param>
    /// <returns>A JSON document. The caller is responsible for disposing it.</returns>
    public static JsonDocument ToJsonDocument(this JsonArray jsonArray)
    {
        return ((JsonValue)jsonArray).ToJsonDocument();
    }

    private static JsonObject ToJsonObject(JsonElement element)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, JsonValue>();
        foreach (var property in element.EnumerateObject())
        {
            builder[property.Name] = property.Value.ToJsonValue();
        }
        return new JsonObject(builder.ToImmutable());
    }

    private static JsonArray ToJsonArray(JsonElement element)
    {
        var builder = ImmutableArray.CreateBuilder<JsonValue>();
        foreach (var item in element.EnumerateArray())
        {
            builder.Add(item.ToJsonValue());
        }
        return new JsonArray(builder.ToImmutable());
    }

    private static JsonNumber ToJsonNumber(JsonElement element)
    {
        if (element.TryGetInt32(out var intValue))
            return new JsonNumber(intValue);
        if (element.TryGetInt64(out var longValue))
            return new JsonNumber(longValue);
        if (element.TryGetDecimal(out var decimalValue))
            return new JsonNumber(decimalValue);
        return new JsonNumber(element.GetDouble());
    }
}

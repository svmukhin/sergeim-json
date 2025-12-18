using SergeiM.Json.Builders;
using SergeiM.Json.IO;

namespace SergeiM.Json;

/// <summary>
/// Factory class for creating JSON objects, arrays, and builders.
/// This class provides static factory methods similar to Jakarta JSON API.
/// </summary>
public static class Json
{
    /// <summary>
    /// Creates a new <see cref="JsonObjectBuilder"/> for building JSON objects.
    /// </summary>
    /// <returns>A new JSON object builder.</returns>
    public static JsonObjectBuilder CreateObjectBuilder()
    {
        return new JsonObjectBuilder();
    }

    /// <summary>
    /// Creates a new <see cref="JsonObjectBuilder"/> initialized with the values from the specified JSON object.
    /// </summary>
    /// <param name="jsonObject">The JSON object to copy values from.</param>
    /// <returns>A new JSON object builder.</returns>
    public static JsonObjectBuilder CreateObjectBuilder(JsonObject jsonObject)
    {
        return new JsonObjectBuilder(jsonObject);
    }

    /// <summary>
    /// Creates a new <see cref="JsonArrayBuilder"/> for building JSON arrays.
    /// </summary>
    /// <returns>A new JSON array builder.</returns>
    public static JsonArrayBuilder CreateArrayBuilder()
    {
        return new JsonArrayBuilder();
    }

    /// <summary>
    /// Creates a new <see cref="JsonArrayBuilder"/> initialized with the values from the specified JSON array.
    /// </summary>
    /// <param name="jsonArray">The JSON array to copy values from.</param>
    /// <returns>A new JSON array builder.</returns>
    public static JsonArrayBuilder CreateArrayBuilder(JsonArray jsonArray)
    {
        return new JsonArrayBuilder(jsonArray);
    }

    /// <summary>
    /// Creates a <see cref="JsonString"/> value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A JSON string value.</returns>
    public static JsonString CreateValue(string value)
    {
        return new JsonString(value);
    }

    /// <summary>
    /// Creates a <see cref="JsonNumber"/> value.
    /// </summary>
    /// <param name="value">The int value.</param>
    /// <returns>A JSON number value.</returns>
    public static JsonNumber CreateValue(int value)
    {
        return new JsonNumber(value);
    }

    /// <summary>
    /// Creates a <see cref="JsonNumber"/> value.
    /// </summary>
    /// <param name="value">The long value.</param>
    /// <returns>A JSON number value.</returns>
    public static JsonNumber CreateValue(long value)
    {
        return new JsonNumber(value);
    }

    /// <summary>
    /// Creates a <see cref="JsonNumber"/> value.
    /// </summary>
    /// <param name="value">The double value.</param>
    /// <returns>A JSON number value.</returns>
    public static JsonNumber CreateValue(double value)
    {
        return new JsonNumber(value);
    }

    /// <summary>
    /// Creates a <see cref="JsonNumber"/> value.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>A JSON number value.</returns>
    public static JsonNumber CreateValue(decimal value)
    {
        return new JsonNumber(value);
    }

    /// <summary>
    /// Creates a <see cref="JsonBoolean"/> value.
    /// </summary>
    /// <param name="value">The boolean value.</param>
    /// <returns>A JSON boolean value.</returns>
    public static JsonValue CreateValue(bool value)
    {
        return value ? JsonValue.True : JsonValue.False;
    }

    /// <summary>
    /// Creates a <see cref="JsonReader"/> from a stream.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>A new JSON reader.</returns>
    public static JsonReader CreateReader(Stream stream, JsonReaderOptions? options = null)
    {
        return JsonReader.Create(stream, options);
    }

    /// <summary>
    /// Creates a <see cref="JsonReader"/> from a TextReader.
    /// </summary>
    /// <param name="textReader">The text reader to read from.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>A new JSON reader.</returns>
    public static JsonReader CreateReader(TextReader textReader, JsonReaderOptions? options = null)
    {
        return JsonReader.Create(textReader, options);
    }

    /// <summary>
    /// Creates a <see cref="JsonReader"/> from a string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>A new JSON reader.</returns>
    public static JsonReader CreateReader(string json, JsonReaderOptions? options = null)
    {
        return JsonReader.Create(new StringReader(json), options);
    }

    /// <summary>
    /// Parses a JSON string and returns a <see cref="JsonValue"/>.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>The parsed JSON value.</returns>
    /// <exception cref="JsonException">If the JSON is invalid.</exception>
    public static JsonValue Parse(string json, JsonReaderOptions? options = null)
    {
        using var reader = CreateReader(json, options);
        return reader.Read();
    }

    /// <summary>
    /// Parses a JSON string and returns a <see cref="JsonObject"/>.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>The parsed JSON object.</returns>
    /// <exception cref="JsonException">If the JSON is invalid or not an object.</exception>
    public static JsonObject ParseObject(string json, JsonReaderOptions? options = null)
    {
        using var reader = CreateReader(json, options);
        return reader.ReadObject();
    }

    /// <summary>
    /// Parses a JSON string and returns a <see cref="JsonArray"/>.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>The parsed JSON array.</returns>
    /// <exception cref="JsonException">If the JSON is invalid or not an array.</exception>
    public static JsonArray ParseArray(string json, JsonReaderOptions? options = null)
    {
        using var reader = CreateReader(json, options);
        return reader.ReadArray();
    }

    /// <summary>
    /// Creates a <see cref="JsonWriter"/> that writes to a stream.
    /// </summary>
    /// <param name="stream">The stream to write to.</param>
    /// <param name="options">Writer options. If null, default options are used.</param>
    /// <returns>A new JSON writer.</returns>
    public static JsonWriter CreateWriter(Stream stream, JsonWriterOptions? options = null)
    {
        return JsonWriter.Create(stream, options);
    }

    /// <summary>
    /// Creates a <see cref="JsonWriter"/> that writes to a TextWriter.
    /// </summary>
    /// <param name="textWriter">The text writer to write to.</param>
    /// <param name="options">Writer options. If null, default options are used.</param>
    /// <returns>A new JSON writer.</returns>
    public static JsonWriter CreateWriter(TextWriter textWriter, JsonWriterOptions? options = null)
    {
        return JsonWriter.Create(textWriter, options);
    }

    /// <summary>
    /// Converts a JSON value to its string representation.
    /// </summary>
    /// <param name="value">The JSON value to convert.</param>
    /// <param name="indented">Whether to use indented formatting.</param>
    /// <returns>A JSON string representation.</returns>
    public static string Stringify(JsonValue value, bool indented = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        using var writer = new StringWriter();
        var options = indented ? JsonWriterOptions.PrettyPrint : JsonWriterOptions.Default;
        using (var jsonWriter = CreateWriter(writer, options))
        {
            jsonWriter.Write(value);
        }
        return writer.ToString();
    }
}

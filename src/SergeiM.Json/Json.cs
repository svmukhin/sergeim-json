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
}

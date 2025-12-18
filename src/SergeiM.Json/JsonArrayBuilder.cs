using System.Collections.Immutable;

namespace SergeiM.Json;

/// <summary>
/// A builder for creating <see cref="JsonArray"/> instances.
/// This class provides a fluent API for constructing immutable JSON arrays.
/// </summary>
public sealed class JsonArrayBuilder
{
    private ImmutableArray<JsonValue>.Builder _builder;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonArrayBuilder"/> class.
    /// </summary>
    public JsonArrayBuilder()
    {
        _builder = ImmutableArray.CreateBuilder<JsonValue>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonArrayBuilder"/> class with initial values from a JSON array.
    /// </summary>
    /// <param name="jsonArray">The JSON array to copy values from.</param>
    public JsonArrayBuilder(JsonArray jsonArray)
    {
        ArgumentNullException.ThrowIfNull(jsonArray);
        _builder = ImmutableArray.CreateBuilder<JsonValue>();
        foreach (var value in jsonArray)
        {
            _builder.Add(value);
        }
    }

    /// <summary>
    /// Adds a JSON value to the array being built.
    /// </summary>
    /// <param name="value">The JSON value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _builder.Add(value);
        return this;
    }

    /// <summary>
    /// Adds a string value to the array being built.
    /// </summary>
    /// <param name="value">The string value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _builder.Add(new JsonString(value));
        return this;
    }

    /// <summary>
    /// Adds an int value to the array being built.
    /// </summary>
    /// <param name="value">The int value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(int value)
    {
        _builder.Add(new JsonNumber(value));
        return this;
    }

    /// <summary>
    /// Adds a long value to the array being built.
    /// </summary>
    /// <param name="value">The long value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(long value)
    {
        _builder.Add(new JsonNumber(value));
        return this;
    }

    /// <summary>
    /// Adds a double value to the array being built.
    /// </summary>
    /// <param name="value">The double value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(double value)
    {
        _builder.Add(new JsonNumber(value));
        return this;
    }

    /// <summary>
    /// Adds a decimal value to the array being built.
    /// </summary>
    /// <param name="value">The decimal value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(decimal value)
    {
        _builder.Add(new JsonNumber(value));
        return this;
    }

    /// <summary>
    /// Adds a boolean value to the array being built.
    /// </summary>
    /// <param name="value">The boolean value to add.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(bool value)
    {
        _builder.Add(value ? JsonValue.True : JsonValue.False);
        return this;
    }

    /// <summary>
    /// Adds a JSON object from a <see cref="JsonObjectBuilder"/> to the array being built.
    /// The builder is built into a <see cref="JsonObject"/> before being added.
    /// </summary>
    /// <param name="builder">The JSON object builder.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(JsonObjectBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        _builder.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a JSON array from a <see cref="JsonArrayBuilder"/> to the array being built.
    /// The builder is built into a <see cref="JsonArray"/> before being added.
    /// </summary>
    /// <param name="builder">The JSON array builder.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder Add(JsonArrayBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        _builder.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a null value to the array being built.
    /// </summary>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder AddNull()
    {
        _builder.Add(JsonValue.Null);
        return this;
    }

    /// <summary>
    /// Removes the element at the specified index from the array being built.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <returns>This builder instance.</returns>
    public JsonArrayBuilder RemoveAt(int index)
    {
        _builder.RemoveAt(index);
        return this;
    }

    /// <summary>
    /// Builds and returns the immutable <see cref="JsonArray"/>.
    /// </summary>
    /// <returns>The created immutable JSON array.</returns>
    public JsonArray Build()
    {
        return new JsonArray(_builder.ToImmutable());
    }
}

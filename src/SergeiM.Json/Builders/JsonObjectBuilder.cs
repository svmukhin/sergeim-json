using System.Collections.Immutable;

namespace SergeiM.Json;

/// <summary>
/// A builder for creating <see cref="JsonObject"/> instances.
/// This class provides a fluent API for constructing immutable JSON objects.
/// </summary>
public sealed class JsonObjectBuilder
{
    private ImmutableDictionary<string, JsonValue>.Builder _builder;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonObjectBuilder"/> class.
    /// </summary>
    public JsonObjectBuilder()
    {
        _builder = ImmutableDictionary.CreateBuilder<string, JsonValue>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonObjectBuilder"/> class with initial values from a JSON object.
    /// </summary>
    /// <param name="jsonObject">The JSON object to copy values from.</param>
    public JsonObjectBuilder(JsonObject jsonObject)
    {
        ArgumentNullException.ThrowIfNull(jsonObject);
        _builder = jsonObject.ToImmutableDictionary().ToBuilder();
    }

    /// <summary>
    /// Adds a name/value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The JSON value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(value);
        _builder[name] = value;
        return this;
    }

    /// <summary>
    /// Adds a name/string value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The string value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, string value)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(value);
        _builder[name] = new JsonString(value);
        return this;
    }

    /// <summary>
    /// Adds a name/int value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The int value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, int value)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder[name] = new JsonNumber(value);
        return this;
    }

    /// <summary>
    /// Adds a name/long value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The long value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, long value)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder[name] = new JsonNumber(value);
        return this;
    }

    /// <summary>
    /// Adds a name/double value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The double value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, double value)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder[name] = new JsonNumber(value);
        return this;
    }

    /// <summary>
    /// Adds a name/decimal value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The decimal value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, decimal value)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder[name] = new JsonNumber(value);
        return this;
    }

    /// <summary>
    /// Adds a name/boolean value pair to the JSON object being built.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The boolean value.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, bool value)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder[name] = value ? JsonValue.True : JsonValue.False;
        return this;
    }

    /// <summary>
    /// Adds a name/object value pair from a <see cref="JsonObjectBuilder"/> to the JSON object being built.
    /// The builder is built into a <see cref="JsonObject"/> before being added.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="builder">The JSON object builder.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, JsonObjectBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(builder);
        _builder[name] = builder.Build();
        return this;
    }

    /// <summary>
    /// Adds a name/array value pair from a <see cref="JsonArrayBuilder"/> to the JSON object being built.
    /// The builder is built into a <see cref="JsonArray"/> before being added.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="builder">The JSON array builder.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Add(string name, JsonArrayBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(builder);
        _builder[name] = builder.Build();
        return this;
    }

    /// <summary>
    /// Adds a null value for the specified property name.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder AddNull(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder[name] = JsonValue.Null;
        return this;
    }

    /// <summary>
    /// Removes a property with the specified name from the JSON object being built.
    /// </summary>
    /// <param name="name">The property name to remove.</param>
    /// <returns>This builder instance.</returns>
    public JsonObjectBuilder Remove(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _builder.Remove(name);
        return this;
    }

    /// <summary>
    /// Builds and returns the immutable <see cref="JsonObject"/>.
    /// </summary>
    /// <returns>The created immutable JSON object.</returns>
    public JsonObject Build()
    {
        return new JsonObject(_builder.ToImmutable());
    }
}

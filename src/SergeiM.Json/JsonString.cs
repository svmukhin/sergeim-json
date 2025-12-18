using System.Text.Json;

namespace SergeiM.Json;

/// <summary>
/// Represents an immutable JSON string value.
/// </summary>
public sealed class JsonString : JsonValue
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonString"/> class.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <exception cref="ArgumentNullException">If value is null.</exception>
    public JsonString(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Gets the string value.
    /// </summary>
    public string Value => _value;

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    public override JsonValueType ValueType => JsonValueType.String;

    /// <summary>
    /// Returns a JSON representation of this value (quoted and escaped).
    /// </summary>
    /// <returns>A JSON string representation.</returns>
    public override string ToString() => JsonSerializer.Serialize(_value);

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is a JsonString with the same value; otherwise, false.</returns>
    public override bool Equals(object? obj) => 
        obj is JsonString other && _value == other._value;

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Implicitly converts a JsonString to a string.
    /// </summary>
    /// <param name="jsonString">The JSON string to convert.</param>
    public static implicit operator string(JsonString jsonString) => jsonString._value;
}

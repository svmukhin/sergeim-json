using System.Collections;
using System.Collections.Immutable;

namespace SergeiM.Json;

/// <summary>
/// Represents an immutable JSON object (an unordered collection of name/value pairs).
/// This class implements <see cref="IReadOnlyDictionary{TKey, TValue}"/> providing read-only access to the object's properties.
/// </summary>
public sealed class JsonObject : JsonValue, IReadOnlyDictionary<string, JsonValue>
{
    /// <summary>
    /// An empty JSON object.
    /// </summary>
    public static readonly JsonObject Empty = new(ImmutableDictionary<string, JsonValue>.Empty);

    private readonly ImmutableDictionary<string, JsonValue> _properties;

    internal JsonObject(ImmutableDictionary<string, JsonValue> properties)
    {
        _properties = properties;
    }

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    public override JsonValueType ValueType => JsonValueType.Object;

    /// <summary>
    /// Gets the number of properties in this JSON object.
    /// </summary>
    public int Count => _properties.Count;

    /// <summary>
    /// Gets the property names in this JSON object.
    /// </summary>
    public IEnumerable<string> Keys => _properties.Keys;

    /// <summary>
    /// Gets the property values in this JSON object.
    /// </summary>
    public IEnumerable<JsonValue> Values => _properties.Values;

    /// <summary>
    /// Gets the value associated with the specified property name.
    /// </summary>
    /// <param name="key">The property name.</param>
    /// <returns>The value associated with the specified property name.</returns>
    /// <exception cref="KeyNotFoundException">If the property name is not found.</exception>
    public JsonValue this[string key] => _properties[key];

    /// <summary>
    /// Determines whether this JSON object contains a property with the specified name.
    /// </summary>
    /// <param name="key">The property name to locate.</param>
    /// <returns>true if the JSON object contains a property with the name; otherwise, false.</returns>
    public bool ContainsKey(string key) => _properties.ContainsKey(key);

    /// <summary>
    /// Gets the value associated with the specified property name.
    /// </summary>
    /// <param name="key">The property name.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified name, if found; otherwise, null.</param>
    /// <returns>true if the JSON object contains a property with the specified name; otherwise, false.</returns>
    public bool TryGetValue(string key, out JsonValue value) => _properties.TryGetValue(key, out value!);

    /// <summary>
    /// Returns an enumerator that iterates through the property name/value pairs.
    /// </summary>
    /// <returns>An enumerator for the JSON object.</returns>
    public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator() => _properties.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Returns a JSON representation of this object.
    /// </summary>
    /// <returns>A JSON string representation.</returns>
    public override string ToString()
    {
        // Placeholder - will be implemented with proper JSON serialization
        return "{}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not JsonObject other || Count != other.Count)
            return false;

        foreach (var kvp in _properties)
        {
            if (!other._properties.TryGetValue(kvp.Key, out var otherValue) || !kvp.Value.Equals(otherValue))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var kvp in _properties.OrderBy(p => p.Key))
        {
            hash.Add(kvp.Key);
            hash.Add(kvp.Value);
        }
        return hash.ToHashCode();
    }
}

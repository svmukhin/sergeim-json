// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

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
    /// Converts this JSON object to an <see cref="ImmutableDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <returns>An immutable dictionary containing all properties.</returns>
    internal ImmutableDictionary<string, JsonValue> ToImmutableDictionary() => _properties;

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
    /// Returns the <see cref="JsonArray"/> value to which the specified name is mapped.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned.</param>
    /// <returns>The array value to which the specified name is mapped, or null if no mapping exists.</returns>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonArray type.</exception>
    public JsonArray? GetJsonArray(string name)
    {
        if (!_properties.TryGetValue(name, out var value))
            return null;
        if (value is JsonArray jsonArray)
            return jsonArray;
        throw new InvalidCastException($"Property '{name}' is of type {value.ValueType}, not Array");
    }

    /// <summary>
    /// Returns the <see cref="JsonObject"/> value to which the specified name is mapped.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned.</param>
    /// <returns>The object value to which the specified name is mapped, or null if no mapping exists.</returns>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonObject type.</exception>
    public JsonObject? GetJsonObject(string name)
    {
        if (!_properties.TryGetValue(name, out var value))
            return null;
        if (value is JsonObject jsonObject)
            return jsonObject;
        throw new InvalidCastException($"Property '{name}' is of type {value.ValueType}, not Object");
    }

    /// <summary>
    /// Returns the <see cref="JsonNumber"/> value to which the specified name is mapped.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned.</param>
    /// <returns>The number value to which the specified name is mapped, or null if no mapping exists.</returns>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonNumber type.</exception>
    public JsonNumber? GetJsonNumber(string name)
    {
        if (!_properties.TryGetValue(name, out var value))
            return null;
        if (value is JsonNumber jsonNumber)
            return jsonNumber;
        throw new InvalidCastException($"Property '{name}' is of type {value.ValueType}, not Number");
    }

    /// <summary>
    /// Returns the <see cref="JsonString"/> value to which the specified name is mapped.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned.</param>
    /// <returns>The string value to which the specified name is mapped, or null if no mapping exists.</returns>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonString type.</exception>
    public JsonString? GetJsonString(string name)
    {
        if (!_properties.TryGetValue(name, out var value))
            return null;
        if (value is JsonString jsonString)
            return jsonString;
        throw new InvalidCastException($"Property '{name}' is of type {value.ValueType}, not String");
    }

    /// <summary>
    /// Returns the string value for the specified name.
    /// This is a convenience method for <c>GetJsonString(name).Value</c>.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as string.</param>
    /// <returns>The string value to which the specified name is mapped.</returns>
    /// <exception cref="KeyNotFoundException">If the specified name doesn't have any mapping.</exception>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonString type.</exception>
    public string GetString(string name)
    {
        var jsonString = GetJsonString(name);
        if (jsonString == null)
            throw new KeyNotFoundException($"Property '{name}' not found");
        return jsonString.Value;
    }

    /// <summary>
    /// Returns the string value of the associated <see cref="JsonString"/> mapping for the specified name.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as string.</param>
    /// <param name="defaultValue">A default value to be returned if the property doesn't exist.</param>
    /// <returns>The string value of the associated mapping for the name, or the default value.</returns>
    public string GetString(string name, string defaultValue)
    {
        var jsonString = GetJsonString(name);
        return jsonString?.Value ?? defaultValue;
    }

    /// <summary>
    /// Returns the int value for the specified name.
    /// This is a convenience method for <c>GetJsonNumber(name).IntValue()</c>.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as int.</param>
    /// <returns>The int value to which the specified name is mapped.</returns>
    /// <exception cref="KeyNotFoundException">If the specified name doesn't have any mapping.</exception>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonNumber type.</exception>
    public int GetInt(string name)
    {
        var jsonNumber = GetJsonNumber(name);
        if (jsonNumber == null)
            throw new KeyNotFoundException($"Property '{name}' not found");
        return jsonNumber.IntValue();
    }

    /// <summary>
    /// Returns the int value of the associated <see cref="JsonNumber"/> mapping for the specified name.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as int.</param>
    /// <param name="defaultValue">A default value to be returned if the property doesn't exist.</param>
    /// <returns>The int value of the associated mapping for the name, or the default value.</returns>
    public int GetInt(string name, int defaultValue)
    {
        var jsonNumber = GetJsonNumber(name);
        return jsonNumber?.IntValue() ?? defaultValue;
    }

    /// <summary>
    /// Returns the long value for the specified name.
    /// This is a convenience method for <c>GetJsonNumber(name).LongValue()</c>.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as long.</param>
    /// <returns>The long value to which the specified name is mapped.</returns>
    /// <exception cref="KeyNotFoundException">If the specified name doesn't have any mapping.</exception>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonNumber type.</exception>
    public long GetLong(string name)
    {
        var jsonNumber = GetJsonNumber(name);
        if (jsonNumber == null)
            throw new KeyNotFoundException($"Property '{name}' not found");
        return jsonNumber.LongValue();
    }

    /// <summary>
    /// Returns the long value of the associated <see cref="JsonNumber"/> mapping for the specified name.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as long.</param>
    /// <param name="defaultValue">A default value to be returned if the property doesn't exist.</param>
    /// <returns>The long value of the associated mapping for the name, or the default value.</returns>
    public long GetLong(string name, long defaultValue)
    {
        var jsonNumber = GetJsonNumber(name);
        return jsonNumber?.LongValue() ?? defaultValue;
    }

    /// <summary>
    /// Returns the double value for the specified name.
    /// This is a convenience method for <c>GetJsonNumber(name).DoubleValue()</c>.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as double.</param>
    /// <returns>The double value to which the specified name is mapped.</returns>
    /// <exception cref="KeyNotFoundException">If the specified name doesn't have any mapping.</exception>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonNumber type.</exception>
    public double GetDouble(string name)
    {
        var jsonNumber = GetJsonNumber(name);
        if (jsonNumber == null)
            throw new KeyNotFoundException($"Property '{name}' not found");
        return jsonNumber.DoubleValue();
    }

    /// <summary>
    /// Returns the double value of the associated <see cref="JsonNumber"/> mapping for the specified name.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as double.</param>
    /// <param name="defaultValue">A default value to be returned if the property doesn't exist.</param>
    /// <returns>The double value of the associated mapping for the name, or the default value.</returns>
    public double GetDouble(string name, double defaultValue)
    {
        var jsonNumber = GetJsonNumber(name);
        return jsonNumber?.DoubleValue() ?? defaultValue;
    }

    /// <summary>
    /// Returns the boolean value of the associated mapping for the specified name.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as boolean.</param>
    /// <returns>The boolean value to which the specified name is mapped.</returns>
    /// <exception cref="KeyNotFoundException">If the specified name doesn't have any mapping.</exception>
    /// <exception cref="InvalidCastException">If the value is not assignable to JsonValue.True or JsonValue.False.</exception>
    public bool GetBoolean(string name)
    {
        if (!_properties.TryGetValue(name, out var value))
            throw new KeyNotFoundException($"Property '{name}' not found");
        if (value is JsonBoolean jsonBoolean)
            return jsonBoolean.Value;
        throw new InvalidCastException($"Property '{name}' is of type {value.ValueType}, not Boolean");
    }

    /// <summary>
    /// Returns the boolean value of the associated mapping for the specified name.
    /// </summary>
    /// <param name="name">The name whose associated value is to be returned as boolean.</param>
    /// <param name="defaultValue">A default value to be returned if the property doesn't exist.</param>
    /// <returns>The boolean value of the associated mapping for the name, or the default value.</returns>
    public bool GetBoolean(string name, bool defaultValue)
    {
        if (!_properties.TryGetValue(name, out var value))
            return defaultValue;
        if (value is JsonBoolean jsonBoolean)
            return jsonBoolean.Value;
        return defaultValue;
    }

    /// <summary>
    /// Returns true if the associated value for the specified name is <see cref="JsonValue.Null"/>.
    /// </summary>
    /// <param name="name">The name whose associated value is checked.</param>
    /// <returns>true if the associated value is JsonValue.Null; otherwise false.</returns>
    /// <exception cref="KeyNotFoundException">If the specified name doesn't have any mapping.</exception>
    public bool IsNull(string name)
    {
        if (!_properties.TryGetValue(name, out var value))
            throw new KeyNotFoundException($"Property '{name}' not found");
        return value is JsonNull;
    }

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
        if (Count == 0)
            return "{}";
        var items = _properties.Select(kvp =>
            $"{System.Text.Json.JsonSerializer.Serialize(kvp.Key)}:{kvp.Value.ToString()}");
        return "{" + string.Join(",", items) + "}";
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

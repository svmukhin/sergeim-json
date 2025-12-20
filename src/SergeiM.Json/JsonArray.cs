// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using System.Collections;
using System.Collections.Immutable;

namespace SergeiM.Json;

/// <summary>
/// Represents an immutable JSON array (an ordered sequence of JSON values).
/// This class implements <see cref="IReadOnlyList{T}"/> providing read-only access to the array's elements.
/// </summary>
public sealed class JsonArray : JsonValue, IReadOnlyList<JsonValue>
{
    /// <summary>
    /// An empty JSON array.
    /// </summary>
    public static readonly JsonArray Empty = new(ImmutableArray<JsonValue>.Empty);

    private readonly ImmutableArray<JsonValue> _values;

    internal JsonArray(ImmutableArray<JsonValue> values)
    {
        _values = values;
    }

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    public override JsonValueType ValueType => JsonValueType.Array;

    /// <summary>
    /// Gets the number of elements in this JSON array.
    /// </summary>
    public int Count => _values.Length;

    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get.</param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="IndexOutOfRangeException">If the index is out of range.</exception>
    public JsonValue this[int index] => _values[index];

    /// <summary>
    /// Returns the <see cref="JsonArray"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the array element.</param>
    /// <returns>The array at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonArray.</exception>
    public JsonArray GetJsonArray(int index)
    {
        var value = _values[index];
        if (value is JsonArray jsonArray)
            return jsonArray;
        throw new InvalidCastException($"Element at index {index} is of type {value.ValueType}, not Array");
    }

    /// <summary>
    /// Returns the <see cref="JsonObject"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the object element.</param>
    /// <returns>The object at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonObject.</exception>
    public JsonObject GetJsonObject(int index)
    {
        var value = _values[index];
        if (value is JsonObject jsonObject)
            return jsonObject;
        throw new InvalidCastException($"Element at index {index} is of type {value.ValueType}, not Object");
    }

    /// <summary>
    /// Returns the <see cref="JsonNumber"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the number element.</param>
    /// <returns>The number at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonNumber.</exception>
    public JsonNumber GetJsonNumber(int index)
    {
        var value = _values[index];
        if (value is JsonNumber jsonNumber)
            return jsonNumber;
        throw new InvalidCastException($"Element at index {index} is of type {value.ValueType}, not Number");
    }

    /// <summary>
    /// Returns the <see cref="JsonString"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the string element.</param>
    /// <returns>The string at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonString.</exception>
    public JsonString GetJsonString(int index)
    {
        var value = _values[index];
        if (value is JsonString jsonString)
            return jsonString;
        throw new InvalidCastException($"Element at index {index} is of type {value.ValueType}, not String");
    }

    /// <summary>
    /// Returns the string value at the specified index.
    /// This is a convenience method for <c>GetJsonString(index).Value</c>.
    /// </summary>
    /// <param name="index">The index of the string element.</param>
    /// <returns>The string value at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonString.</exception>
    public string GetString(int index) => GetJsonString(index).Value;

    /// <summary>
    /// Returns the int value at the specified index.
    /// This is a convenience method for <c>GetJsonNumber(index).IntValue()</c>.
    /// </summary>
    /// <param name="index">The index of the number element.</param>
    /// <returns>The int value at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonNumber.</exception>
    public int GetInt(int index) => GetJsonNumber(index).IntValue();

    /// <summary>
    /// Returns the long value at the specified index.
    /// This is a convenience method for <c>GetJsonNumber(index).LongValue()</c>.
    /// </summary>
    /// <param name="index">The index of the number element.</param>
    /// <returns>The long value at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonNumber.</exception>
    public long GetLong(int index) => GetJsonNumber(index).LongValue();

    /// <summary>
    /// Returns the double value at the specified index.
    /// This is a convenience method for <c>GetJsonNumber(index).DoubleValue()</c>.
    /// </summary>
    /// <param name="index">The index of the number element.</param>
    /// <returns>The double value at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonNumber.</exception>
    public double GetDouble(int index) => GetJsonNumber(index).DoubleValue();

    /// <summary>
    /// Returns the boolean value at the specified index.
    /// </summary>
    /// <param name="index">The index of the boolean element.</param>
    /// <returns>The boolean value at the specified index.</returns>
    /// <exception cref="InvalidCastException">If the element is not a JsonBoolean.</exception>
    public bool GetBoolean(int index)
    {
        var value = _values[index];
        if (value is JsonBoolean jsonBoolean)
            return jsonBoolean.Value;
        throw new InvalidCastException($"Element at index {index} is of type {value.ValueType}, not Boolean");
    }

    /// <summary>
    /// Returns true if the element at the specified index is <see cref="JsonValue.Null"/>.
    /// </summary>
    /// <param name="index">The index to check.</param>
    /// <returns>true if the element is JsonValue.Null; otherwise false.</returns>
    public bool IsNull(int index) => _values[index] is JsonNull;

    /// <summary>
    /// Returns an enumerator that iterates through the array elements.
    /// </summary>
    /// <returns>An enumerator for the JSON array.</returns>
    public IEnumerator<JsonValue> GetEnumerator() => ((IEnumerable<JsonValue>)_values).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Returns a JSON representation of this array.
    /// </summary>
    /// <returns>A JSON string representation.</returns>
    public override string ToString()
    {
        if (Count == 0)
            return "[]";
        var items = _values.Select(v => v.ToString());
        return "[" + string.Join(",", items) + "]";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not JsonArray other || Count != other.Count)
            return false;
        for (int i = 0; i < Count; i++)
        {
            if (!_values[i].Equals(other._values[i]))
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
        foreach (var value in _values)
        {
            hash.Add(value);
        }
        return hash.ToHashCode();
    }
}

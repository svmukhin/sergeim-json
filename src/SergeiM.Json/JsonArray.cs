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
        // Placeholder - will be implemented with proper JSON serialization
        return "[]";
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

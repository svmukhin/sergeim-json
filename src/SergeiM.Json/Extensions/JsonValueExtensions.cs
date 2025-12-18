using SergeiM.Json.Patch;

namespace SergeiM.Json.Extensions;

/// <summary>
/// Extension methods for JSON values providing additional formatting and utility methods.
/// </summary>
public static class JsonValueExtensions
{
    /// <summary>
    /// Converts a JSON value to a compact JSON string.
    /// </summary>
    /// <param name="value">The JSON value to convert.</param>
    /// <returns>A compact JSON string representation.</returns>
    public static string ToJsonString(this JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return Json.Stringify(value, indented: false);
    }

    /// <summary>
    /// Converts a JSON value to an indented (pretty-printed) JSON string.
    /// </summary>
    /// <param name="value">The JSON value to convert.</param>
    /// <returns>An indented JSON string representation.</returns>
    public static string ToIndentedJsonString(this JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return Json.Stringify(value, indented: true);
    }

    /// <summary>
    /// Gets a value at the specified JSON Pointer path.
    /// </summary>
    /// <param name="value">The root JSON value.</param>
    /// <param name="pointer">The JSON Pointer path.</param>
    /// <returns>The value at the pointer location.</returns>
    /// <exception cref="JsonException">If the pointer cannot be resolved.</exception>
    public static JsonValue GetValue(this JsonValue value, string pointer)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(pointer);
        return new JsonPointer(pointer).GetValue(value);
    }

    /// <summary>
    /// Gets a value at the specified JSON Pointer path.
    /// </summary>
    /// <param name="value">The root JSON value.</param>
    /// <param name="pointer">The JSON Pointer.</param>
    /// <returns>The value at the pointer location.</returns>
    /// <exception cref="JsonException">If the pointer cannot be resolved.</exception>
    public static JsonValue GetValue(this JsonValue value, JsonPointer pointer)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(pointer);
        return pointer.GetValue(value);
    }

    /// <summary>
    /// Tries to get a value at the specified JSON Pointer path.
    /// </summary>
    /// <param name="value">The root JSON value.</param>
    /// <param name="pointer">The JSON Pointer path.</param>
    /// <param name="result">When this method returns, contains the value if found; otherwise, null.</param>
    /// <returns>true if the value was found; otherwise, false.</returns>
    public static bool TryGetValue(this JsonValue value, string pointer, out JsonValue? result)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(pointer);
        return new JsonPointer(pointer).TryGetValue(value, out result);
    }

    /// <summary>
    /// Applies a JSON Patch to this value.
    /// </summary>
    /// <param name="value">The target JSON value.</param>
    /// <param name="patch">The patch to apply.</param>
    /// <returns>A new JSON value with the patch applied.</returns>
    public static JsonValue ApplyPatch(this JsonValue value, JsonPatch patch)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(patch);
        return patch.Apply(value);
    }

    /// <summary>
    /// Applies a JSON Merge Patch to this value.
    /// </summary>
    /// <param name="value">The target JSON value.</param>
    /// <param name="patch">The merge patch to apply.</param>
    /// <returns>A new JSON value with the patch applied.</returns>
    public static JsonValue ApplyMergePatch(this JsonValue value, JsonValue patch)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(patch);
        return JsonMergePatch.Apply(value, patch);
    }

    /// <summary>
    /// Determines whether this JSON value is equivalent to another value.
    /// This performs a deep equality check.
    /// </summary>
    /// <param name="value">The first JSON value.</param>
    /// <param name="other">The second JSON value.</param>
    /// <returns>true if the values are equivalent; otherwise, false.</returns>
    public static bool IsEquivalentTo(this JsonValue value, JsonValue other)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);
        return value.Equals(other);
    }

    /// <summary>
    /// Creates a deep clone of this JSON value.
    /// Since JSON values are immutable, this returns the same instance.
    /// </summary>
    /// <param name="value">The JSON value to clone.</param>
    /// <returns>The same JSON value (values are immutable).</returns>
    public static JsonValue Clone(this JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        // Since JSON values are immutable, we can return the same instance
        return value;
    }

    /// <summary>
    /// Gets the size (number of properties or elements) of this JSON value.
    /// Returns 0 for primitive values, the number of properties for objects,
    /// and the number of elements for arrays.
    /// </summary>
    /// <param name="value">The JSON value.</param>
    /// <returns>The size of the value.</returns>
    public static int GetSize(this JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value switch
        {
            JsonObject obj => obj.Count,
            JsonArray arr => arr.Count,
            _ => 0
        };
    }

    /// <summary>
    /// Determines whether this JSON value contains the specified pointer path.
    /// </summary>
    /// <param name="value">The root JSON value.</param>
    /// <param name="pointer">The JSON Pointer path.</param>
    /// <returns>true if the path exists; otherwise, false.</returns>
    public static bool ContainsPath(this JsonValue value, string pointer)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(pointer);
        return new JsonPointer(pointer).Contains(value);
    }

    /// <summary>
    /// Determines whether this JSON value contains the specified pointer path.
    /// </summary>
    /// <param name="value">The root JSON value.</param>
    /// <param name="pointer">The JSON Pointer.</param>
    /// <returns>true if the path exists; otherwise, false.</returns>
    public static bool ContainsPath(this JsonValue value, JsonPointer pointer)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(pointer);
        return pointer.Contains(value);
    }
}

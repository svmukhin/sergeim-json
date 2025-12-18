namespace SergeiM.Json;

/// <summary>
/// Implements JSON Pointer (RFC 6901) for navigating JSON structures.
/// </summary>
public sealed class JsonPointer
{
    private readonly string _pointer;
    private readonly string[] _tokens;

    /// <summary>
    /// An empty JSON Pointer (refers to the whole document).
    /// </summary>
    public static readonly JsonPointer Empty = new JsonPointer("");

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPointer"/> class.
    /// </summary>
    /// <param name="pointer">The JSON Pointer string (e.g., "/path/to/value").</param>
    /// <exception cref="ArgumentException">If the pointer is invalid.</exception>
    public JsonPointer(string pointer)
    {
        ArgumentNullException.ThrowIfNull(pointer);
        if (pointer.Length > 0 && !pointer.StartsWith('/'))
            throw new ArgumentException("JSON Pointer must start with '/' or be empty", nameof(pointer));
        _pointer = pointer;
        _tokens = ParsePointer(pointer);
    }

    /// <summary>
    /// Gets the JSON Pointer string.
    /// </summary>
    public string Pointer => _pointer;

    /// <summary>
    /// Gets the reference tokens that make up this pointer.
    /// </summary>
    public IReadOnlyList<string> Tokens => _tokens;

    /// <summary>
    /// Gets a value from a JSON structure using this pointer.
    /// </summary>
    /// <param name="root">The root JSON value to navigate.</param>
    /// <returns>The value at the pointer location.</returns>
    /// <exception cref="JsonException">If the pointer cannot be resolved.</exception>
    public JsonValue GetValue(JsonValue root)
    {
        ArgumentNullException.ThrowIfNull(root);
        if (_tokens.Length == 0)
            return root;
        var current = root;
        for (int i = 0; i < _tokens.Length; i++)
        {
            var token = _tokens[i];
            
            if (current is JsonObject obj)
            {
                if (!obj.ContainsKey(token))
                    throw new JsonException($"Property '{token}' not found at pointer '{GetPointerUpTo(i)}'");
                current = obj[token];
            }
            else if (current is JsonArray arr)
            {
                if (!TryParseArrayIndex(token, arr.Count, out var index))
                    throw new JsonException($"Invalid array index '{token}' at pointer '{GetPointerUpTo(i)}'");
                current = arr[index];
            }
            else
            {
                throw new JsonException($"Cannot navigate into {current.ValueType} at pointer '{GetPointerUpTo(i)}'");
            }
        }
        return current;
    }

    /// <summary>
    /// Tries to get a value from a JSON structure using this pointer.
    /// </summary>
    /// <param name="root">The root JSON value to navigate.</param>
    /// <param name="value">When this method returns, contains the value if found; otherwise, null.</param>
    /// <returns>true if the value was found; otherwise, false.</returns>
    public bool TryGetValue(JsonValue root, out JsonValue? value)
    {
        try
        {
            value = GetValue(root);
            return true;
        }
        catch (JsonException)
        {
            value = null;
            return false;
        }
    }

    /// <summary>
    /// Determines whether a value exists at this pointer location in the JSON structure.
    /// </summary>
    /// <param name="root">The root JSON value to check.</param>
    /// <returns>true if a value exists at the pointer location; otherwise, false.</returns>
    public bool Contains(JsonValue root)
    {
        return TryGetValue(root, out _);
    }

    /// <summary>
    /// Creates a new JSON Pointer by appending a token.
    /// </summary>
    /// <param name="token">The token to append.</param>
    /// <returns>A new JSON Pointer.</returns>
    public JsonPointer Append(string token)
    {
        ArgumentNullException.ThrowIfNull(token);
        var escaped = EscapeToken(token);
        return new JsonPointer(_pointer + "/" + escaped);
    }

    /// <summary>
    /// Creates a new JSON Pointer by appending an array index.
    /// </summary>
    /// <param name="index">The array index to append.</param>
    /// <returns>A new JSON Pointer.</returns>
    public JsonPointer Append(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative");
        return new JsonPointer(_pointer + "/" + index);
    }

    /// <summary>
    /// Parses a JSON Pointer string into tokens.
    /// </summary>
    /// <param name="pointer">The JSON Pointer string.</param>
    /// <returns>An array of unescaped tokens.</returns>
    public static string[] ParsePointer(string pointer)
    {
        if (string.IsNullOrEmpty(pointer))
            return Array.Empty<string>();
        var parts = pointer.Substring(1).Split('/'); // Remove leading '/'
        var tokens = new string[parts.Length];        
        for (int i = 0; i < parts.Length; i++)
        {
            tokens[i] = UnescapeToken(parts[i]);
        }
        return tokens;
    }

    /// <summary>
    /// Unescapes a JSON Pointer token according to RFC 6901.
    /// </summary>
    /// <param name="token">The escaped token.</param>
    /// <returns>The unescaped token.</returns>
    public static string UnescapeToken(string token)
    {
        // RFC 6901: ~1 becomes /, ~0 becomes ~
        return token.Replace("~1", "/").Replace("~0", "~");
    }

    /// <summary>
    /// Escapes a string for use as a JSON Pointer token according to RFC 6901.
    /// </summary>
    /// <param name="token">The token to escape.</param>
    /// <returns>The escaped token.</returns>
    public static string EscapeToken(string token)
    {
        // RFC 6901: ~ becomes ~0, / becomes ~1 (order matters!)
        return token.Replace("~", "~0").Replace("/", "~1");
    }

    private string GetPointerUpTo(int tokenIndex)
    {
        if (tokenIndex == 0)
            return "";        
        return "/" + string.Join("/", _tokens.Take(tokenIndex).Select(EscapeToken));
    }

    private static bool TryParseArrayIndex(string token, int arrayLength, out int index)
    {
        // RFC 6901: array index must be a non-negative integer without leading zeros
        // Exception: "0" is valid, but "00" is not        
        if (token == "-")
        {
            // "-" is a special token for appending (used in JSON Patch)
            index = arrayLength;
            return true;
        }
        if (!int.TryParse(token, out index))
            return false;
        if (index < 0)
            return false;
        // Check for leading zeros (except for "0" itself)
        if (token.Length > 1 && token[0] == '0')
            return false;
        // Index must be within bounds for access operations
        if (index >= arrayLength)
            return false;
        return true;
    }

    /// <summary>
    /// Returns the JSON Pointer string representation.
    /// </summary>
    /// <returns>The JSON Pointer string.</returns>
    public override string ToString() => _pointer;

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is JsonPointer other && _pointer == other._pointer;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    public override int GetHashCode() => _pointer.GetHashCode();
}

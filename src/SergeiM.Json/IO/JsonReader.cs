using System.Collections.Immutable;
using System.Text.Json;

namespace SergeiM.Json.IO;

/// <summary>
/// Reads JSON data and converts it to immutable <see cref="JsonValue"/> instances.
/// This class implements <see cref="IDisposable"/> and should be disposed after use.
/// </summary>
public sealed class JsonReader : IDisposable
{
    private readonly Stream? _stream;
    private readonly TextReader? _textReader;
    private readonly JsonReaderOptions _options;
    private bool _disposed;

    private JsonReader(Stream stream, JsonReaderOptions options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? JsonReaderOptions.Default;
    }

    private JsonReader(TextReader textReader, JsonReaderOptions options)
    {
        _textReader = textReader ?? throw new ArgumentNullException(nameof(textReader));
        _options = options ?? JsonReaderOptions.Default;
    }

    /// <summary>
    /// Creates a JSON reader from a stream.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>A new JSON reader.</returns>
    public static JsonReader Create(Stream stream, JsonReaderOptions? options = null)
    {
        return new JsonReader(stream, options ?? JsonReaderOptions.Default);
    }

    /// <summary>
    /// Creates a JSON reader from a TextReader.
    /// </summary>
    /// <param name="textReader">The text reader to read from.</param>
    /// <param name="options">Reader options. If null, default options are used.</param>
    /// <returns>A new JSON reader.</returns>
    public static JsonReader Create(TextReader textReader, JsonReaderOptions? options = null)
    {
        return new JsonReader(textReader, options ?? JsonReaderOptions.Default);
    }

    /// <summary>
    /// Reads and returns a JSON object from the input.
    /// </summary>
    /// <returns>A JSON object read from the input.</returns>
    /// <exception cref="JsonException">If the JSON is invalid or not an object.</exception>
    public JsonObject ReadObject()
    {
        var value = Read();
        if (value is JsonObject jsonObject)
            return jsonObject;        
        throw new JsonException($"Expected JSON object but found {value.ValueType}");
    }

    /// <summary>
    /// Reads and returns a JSON array from the input.
    /// </summary>
    /// <returns>A JSON array read from the input.</returns>
    /// <exception cref="JsonException">If the JSON is invalid or not an array.</exception>
    public JsonArray ReadArray()
    {
        var value = Read();
        if (value is JsonArray jsonArray)
            return jsonArray;        
        throw new JsonException($"Expected JSON array but found {value.ValueType}");
    }

    /// <summary>
    /// Reads and returns a JSON value from the input. The type is auto-detected.
    /// </summary>
    /// <returns>A JSON value read from the input.</returns>
    /// <exception cref="JsonException">If the JSON is invalid.</exception>
    public JsonValue Read()
    {
        try
        {
            string jsonText;
            if (_stream != null)
            {
                using var reader = new StreamReader(_stream, leaveOpen: true);
                jsonText = reader.ReadToEnd();
            }
            else if (_textReader != null)
            {
                jsonText = _textReader.ReadToEnd();
            }
            else
            {
                throw new InvalidOperationException("No input source available");
            }
            var readerOptions = new JsonDocumentOptions
            {
                AllowTrailingCommas = _options.AllowTrailingCommas,
                CommentHandling = _options.AllowComments 
                    ? JsonCommentHandling.Skip 
                    : JsonCommentHandling.Disallow,
                MaxDepth = _options.MaxDepth
            };
            using var document = JsonDocument.Parse(jsonText, readerOptions);
            return ParseElement(document.RootElement);
        }
        catch (System.Text.Json.JsonException ex)
        {
            throw new JsonException("Failed to parse JSON", ex);
        }
    }

    private static JsonValue ParseElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Object => ParseObject(element),
            JsonValueKind.Array => ParseArray(element),
            JsonValueKind.String => new JsonString(element.GetString()!),
            JsonValueKind.Number => ParseNumber(element),
            JsonValueKind.True => JsonValue.True,
            JsonValueKind.False => JsonValue.False,
            JsonValueKind.Null => JsonValue.Null,
            _ => throw new JsonException($"Unsupported JSON value kind: {element.ValueKind}")
        };
    }

    private static JsonObject ParseObject(JsonElement element)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, JsonValue>();        
        foreach (var property in element.EnumerateObject())
        {
            builder[property.Name] = ParseElement(property.Value);
        }
        return new JsonObject(builder.ToImmutable());
    }

    private static JsonArray ParseArray(JsonElement element)
    {
        var builder = ImmutableArray.CreateBuilder<JsonValue>();        
        foreach (var item in element.EnumerateArray())
        {
            builder.Add(ParseElement(item));
        }
        return new JsonArray(builder.ToImmutable());
    }

    private static JsonNumber ParseNumber(JsonElement element)
    {
        if (element.TryGetInt32(out var intValue))
            return new JsonNumber(intValue);        
        if (element.TryGetInt64(out var longValue))
            return new JsonNumber(longValue);        
        if (element.TryGetDecimal(out var decimalValue))
            return new JsonNumber(decimalValue);        
        return new JsonNumber(element.GetDouble());
    }

    /// <summary>
    /// Closes the reader and releases resources.
    /// </summary>
    public void Close()
    {
        Dispose();
    }

    /// <summary>
    /// Releases all resources used by the <see cref="JsonReader"/>.
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            // Note: We don't dispose the stream or textReader as we don't own them
            // The caller is responsible for disposing the input source
            _disposed = true;
        }
    }
}

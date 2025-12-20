// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using System.Text.Json;

namespace SergeiM.Json.IO;

/// <summary>
/// Writes JSON values to an output stream or text writer.
/// This class implements <see cref="IDisposable"/> and should be disposed after use.
/// </summary>
public sealed class JsonWriter : IDisposable
{
    private readonly Stream? _stream;
    private readonly TextWriter? _textWriter;
    private readonly JsonWriterOptions _options;
    private Utf8JsonWriter? _utf8JsonWriter;
    private bool _disposed;

    private JsonWriter(Stream stream, JsonWriterOptions options)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _options = options ?? JsonWriterOptions.Default;
    }

    private JsonWriter(TextWriter textWriter, JsonWriterOptions options)
    {
        _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        _options = options ?? JsonWriterOptions.Default;
    }

    /// <summary>
    /// Creates a JSON writer that writes to a stream.
    /// </summary>
    /// <param name="stream">The stream to write to.</param>
    /// <param name="options">Writer options. If null, default options are used.</param>
    /// <returns>A new JSON writer.</returns>
    public static JsonWriter Create(Stream stream, JsonWriterOptions? options = null)
    {
        return new JsonWriter(stream, options ?? JsonWriterOptions.Default);
    }

    /// <summary>
    /// Creates a JSON writer that writes to a TextWriter.
    /// </summary>
    /// <param name="textWriter">The text writer to write to.</param>
    /// <param name="options">Writer options. If null, default options are used.</param>
    /// <returns>A new JSON writer.</returns>
    public static JsonWriter Create(TextWriter textWriter, JsonWriterOptions? options = null)
    {
        return new JsonWriter(textWriter, options ?? JsonWriterOptions.Default);
    }

    /// <summary>
    /// Writes a JSON object to the output.
    /// </summary>
    /// <param name="jsonObject">The JSON object to write.</param>
    /// <exception cref="JsonException">If writing fails.</exception>
    public void WriteObject(JsonObject jsonObject)
    {
        ArgumentNullException.ThrowIfNull(jsonObject);
        Write(jsonObject);
    }

    /// <summary>
    /// Writes a JSON array to the output.
    /// </summary>
    /// <param name="jsonArray">The JSON array to write.</param>
    /// <exception cref="JsonException">If writing fails.</exception>
    public void WriteArray(JsonArray jsonArray)
    {
        ArgumentNullException.ThrowIfNull(jsonArray);
        Write(jsonArray);
    }

    /// <summary>
    /// Writes a JSON value to the output.
    /// </summary>
    /// <param name="value">The JSON value to write.</param>
    /// <exception cref="JsonException">If writing fails.</exception>
    public void Write(JsonValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        try
        {
            if (_stream != null)
            {
                WriteToStream(value);
            }
            else if (_textWriter != null)
            {
                WriteToTextWriter(value);
            }
            else
            {
                throw new InvalidOperationException("No output target available");
            }
        }
        catch (Exception ex) when (ex is not JsonException)
        {
            throw new JsonException("Failed to write JSON", ex);
        }
    }

    private void WriteToStream(JsonValue value)
    {
        var writerOptions = new System.Text.Json.JsonWriterOptions
        {
            Indented = _options.IndentOutput,
            Encoder = _options.Encoder
        };
        _utf8JsonWriter = new Utf8JsonWriter(_stream!, writerOptions);
        WriteValue(_utf8JsonWriter, value);
        _utf8JsonWriter.Flush();
    }

    private void WriteToTextWriter(JsonValue value)
    {
        using var memoryStream = new MemoryStream();
        var writerOptions = new System.Text.Json.JsonWriterOptions
        {
            Indented = _options.IndentOutput,
            Encoder = _options.Encoder
        };
        using (var writer = new Utf8JsonWriter(memoryStream, writerOptions))
        {
            WriteValue(writer, value);
            writer.Flush();
        }
        memoryStream.Position = 0;
        using var reader = new StreamReader(memoryStream);
        var json = reader.ReadToEnd();
        _textWriter!.Write(json);
    }

    private static void WriteValue(Utf8JsonWriter writer, JsonValue value)
    {
        switch (value.ValueType)
        {
            case JsonValueType.Object:
                WriteObject(writer, (JsonObject)value);
                break;
            case JsonValueType.Array:
                WriteArray(writer, (JsonArray)value);
                break;
            case JsonValueType.String:
                writer.WriteStringValue(((JsonString)value).Value);
                break;
            case JsonValueType.Number:
                WriteNumber(writer, (JsonNumber)value);
                break;
            case JsonValueType.Boolean:
                writer.WriteBooleanValue(((JsonBoolean)value).Value);
                break;
            case JsonValueType.Null:
                writer.WriteNullValue();
                break;
            default:
                throw new JsonException($"Unsupported JSON value type: {value.ValueType}");
        }
    }

    private static void WriteObject(Utf8JsonWriter writer, JsonObject jsonObject)
    {
        writer.WriteStartObject();
        foreach (var property in jsonObject)
        {
            writer.WritePropertyName(property.Key);
            WriteValue(writer, property.Value);
        }
        writer.WriteEndObject();
    }

    private static void WriteArray(Utf8JsonWriter writer, JsonArray jsonArray)
    {
        writer.WriteStartArray();
        foreach (var item in jsonArray)
        {
            WriteValue(writer, item);
        }
        writer.WriteEndArray();
    }

    private static void WriteNumber(Utf8JsonWriter writer, JsonNumber jsonNumber)
    {
        if (jsonNumber.IsIntegral())
        {
            try
            {
                writer.WriteNumberValue(jsonNumber.LongValue());
                return;
            }
            catch (OverflowException)
            {
                // Fall through to decimal
            }
        }
        writer.WriteNumberValue(jsonNumber.DecimalValue());
    }

    /// <summary>
    /// Flushes any buffered content to the underlying stream or writer.
    /// </summary>
    public void Flush()
    {
        _utf8JsonWriter?.Flush();
        _stream?.Flush();
        _textWriter?.Flush();
    }

    /// <summary>
    /// Closes the writer and releases resources.
    /// </summary>
    public void Close()
    {
        Dispose();
    }

    /// <summary>
    /// Releases all resources used by the <see cref="JsonWriter"/>.
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _utf8JsonWriter?.Dispose();
            _utf8JsonWriter = null;
            // Note: We don't dispose the stream or textWriter as we don't own them
            // The caller is responsible for disposing the output target
            _disposed = true;
        }
    }
}

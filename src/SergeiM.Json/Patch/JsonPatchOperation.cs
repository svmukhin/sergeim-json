// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Patch;

/// <summary>
/// Represents a single operation in a JSON Patch (RFC 6902).
/// </summary>
public sealed class JsonPatchOperation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPatchOperation"/> class.
    /// </summary>
    /// <param name="operationType">The type of operation.</param>
    /// <param name="path">The target location pointer.</param>
    /// <param name="value">The value for add/replace/test operations.</param>
    /// <param name="from">The source location pointer for move/copy operations.</param>
    public JsonPatchOperation(
        JsonPatchOperationType operationType,
        JsonPointer path,
        JsonValue? value = null,
        JsonPointer? from = null)
    {
        OperationType = operationType;
        Path = path ?? throw new ArgumentNullException(nameof(path));
        Value = value;
        From = from;
        switch (operationType)
        {
            case JsonPatchOperationType.Add:
            case JsonPatchOperationType.Replace:
            case JsonPatchOperationType.Test:
                if (value == null)
                    throw new ArgumentException($"{operationType} operation requires a value", nameof(value));
                break;
            case JsonPatchOperationType.Move:
            case JsonPatchOperationType.Copy:
                if (from == null)
                    throw new ArgumentException($"{operationType} operation requires a 'from' pointer", nameof(from));
                break;
        }
    }

    /// <summary>
    /// Gets the type of this operation.
    /// </summary>
    public JsonPatchOperationType OperationType { get; }

    /// <summary>
    /// Gets the target location pointer.
    /// </summary>
    public JsonPointer Path { get; }

    /// <summary>
    /// Gets the value for add/replace/test operations.
    /// </summary>
    public JsonValue? Value { get; }

    /// <summary>
    /// Gets the source location pointer for move/copy operations.
    /// </summary>
    public JsonPointer? From { get; }

    /// <summary>
    /// Creates an Add operation.
    /// </summary>
    public static JsonPatchOperation Add(JsonPointer path, JsonValue value)
        => new(JsonPatchOperationType.Add, path, value);

    /// <summary>
    /// Creates an Add operation with a string path.
    /// </summary>
    public static JsonPatchOperation Add(string path, JsonValue value)
        => new(JsonPatchOperationType.Add, new JsonPointer(path), value);

    /// <summary>
    /// Creates a Remove operation.
    /// </summary>
    public static JsonPatchOperation Remove(JsonPointer path)
        => new(JsonPatchOperationType.Remove, path);

    /// <summary>
    /// Creates a Remove operation with a string path.
    /// </summary>
    public static JsonPatchOperation Remove(string path)
        => new(JsonPatchOperationType.Remove, new JsonPointer(path));

    /// <summary>
    /// Creates a Replace operation.
    /// </summary>
    public static JsonPatchOperation Replace(JsonPointer path, JsonValue value)
        => new(JsonPatchOperationType.Replace, path, value);

    /// <summary>
    /// Creates a Replace operation with a string path.
    /// </summary>
    public static JsonPatchOperation Replace(string path, JsonValue value)
        => new(JsonPatchOperationType.Replace, new JsonPointer(path), value);

    /// <summary>
    /// Creates a Move operation.
    /// </summary>
    public static JsonPatchOperation Move(JsonPointer from, JsonPointer path)
        => new(JsonPatchOperationType.Move, path, from: from);

    /// <summary>
    /// Creates a Move operation with string paths.
    /// </summary>
    public static JsonPatchOperation Move(string from, string path)
        => new(JsonPatchOperationType.Move, new JsonPointer(path), from: new JsonPointer(from));

    /// <summary>
    /// Creates a Copy operation.
    /// </summary>
    public static JsonPatchOperation Copy(JsonPointer from, JsonPointer path)
        => new(JsonPatchOperationType.Copy, path, from: from);

    /// <summary>
    /// Creates a Copy operation with string paths.
    /// </summary>
    public static JsonPatchOperation Copy(string from, string path)
        => new(JsonPatchOperationType.Copy, new JsonPointer(path), from: new JsonPointer(from));

    /// <summary>
    /// Creates a Test operation.
    /// </summary>
    public static JsonPatchOperation Test(JsonPointer path, JsonValue value)
        => new(JsonPatchOperationType.Test, path, value);

    /// <summary>
    /// Creates a Test operation with a string path.
    /// </summary>
    public static JsonPatchOperation Test(string path, JsonValue value)
        => new(JsonPatchOperationType.Test, new JsonPointer(path), value);
}

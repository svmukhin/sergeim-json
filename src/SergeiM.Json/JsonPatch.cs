using System.Collections.Immutable;

namespace SergeiM.Json;

/// <summary>
/// Implements JSON Patch (RFC 6902) for applying modifications to JSON structures.
/// </summary>
public sealed class JsonPatch
{
    private readonly ImmutableArray<JsonPatchOperation> _operations;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPatch"/> class.
    /// </summary>
    /// <param name="operations">The patch operations.</param>
    public JsonPatch(params JsonPatchOperation[] operations)
    {
        ArgumentNullException.ThrowIfNull(operations);
        _operations = operations.ToImmutableArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPatch"/> class.
    /// </summary>
    /// <param name="operations">The patch operations.</param>
    public JsonPatch(IEnumerable<JsonPatchOperation> operations)
    {
        ArgumentNullException.ThrowIfNull(operations);
        _operations = operations.ToImmutableArray();
    }

    /// <summary>
    /// Gets the operations in this patch.
    /// </summary>
    public IReadOnlyList<JsonPatchOperation> Operations => _operations;

    /// <summary>
    /// Applies this patch to a JSON value and returns a new modified JSON value.
    /// </summary>
    /// <param name="target">The JSON value to patch.</param>
    /// <returns>A new JSON value with the patch applied.</returns>
    /// <exception cref="JsonException">If the patch cannot be applied.</exception>
    public JsonValue Apply(JsonValue target)
    {
        ArgumentNullException.ThrowIfNull(target);
        var current = target;
        foreach (var operation in _operations)
        {
            current = ApplyOperation(current, operation);
        }
        return current;
    }

    private static JsonValue ApplyOperation(JsonValue target, JsonPatchOperation operation)
    {
        return operation.OperationType switch
        {
            JsonPatchOperationType.Add => ApplyAdd(target, operation),
            JsonPatchOperationType.Remove => ApplyRemove(target, operation),
            JsonPatchOperationType.Replace => ApplyReplace(target, operation),
            JsonPatchOperationType.Move => ApplyMove(target, operation),
            JsonPatchOperationType.Copy => ApplyCopy(target, operation),
            JsonPatchOperationType.Test => ApplyTest(target, operation),
            _ => throw new JsonException($"Unknown operation type: {operation.OperationType}")
        };
    }

    private static JsonValue ApplyAdd(JsonValue target, JsonPatchOperation operation)
    {
        var path = operation.Path;
        var value = operation.Value!;
        if (path.Tokens.Count == 0)
            return value;
        return ModifyAtPath(target, path, (parent, lastToken) =>
        {
            if (parent is JsonObject obj)
            {
                var builder = Json.CreateObjectBuilder(obj);
                builder.Add(lastToken, value);
                return builder.Build();
            }
            else if (parent is JsonArray arr)
            {
                var builder = Json.CreateArrayBuilder(arr);
                if (lastToken == "-")
                {
                    builder.Add(value);
                }
                else if (int.TryParse(lastToken, out var index) && index >= 0 && index <= arr.Count)
                {
                    var newBuilder = Json.CreateArrayBuilder();
                    for (int i = 0; i < index; i++)
                        newBuilder.Add(arr[i]);
                    newBuilder.Add(value);
                    for (int i = index; i < arr.Count; i++)
                        newBuilder.Add(arr[i]);
                    return newBuilder.Build();
                }
                else
                {
                    throw new JsonException($"Invalid array index: {lastToken}");
                }
                return builder.Build();
            }
            throw new JsonException($"Cannot add to {parent.ValueType}");
        });
    }

    private static JsonValue ApplyRemove(JsonValue target, JsonPatchOperation operation)
    {
        var path = operation.Path;
        if (path.Tokens.Count == 0)
            throw new JsonException("Cannot remove the root element");
        return ModifyAtPath(target, path, (parent, lastToken) =>
        {
            if (parent is JsonObject obj)
            {
                if (!obj.ContainsKey(lastToken))
                    throw new JsonException($"Property '{lastToken}' not found");
                var builder = Json.CreateObjectBuilder(obj);
                builder.Remove(lastToken);
                return builder.Build();
            }
            else if (parent is JsonArray arr)
            {
                if (!int.TryParse(lastToken, out var index) || index < 0 || index >= arr.Count)
                    throw new JsonException($"Invalid array index: {lastToken}");
                var builder = Json.CreateArrayBuilder();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (i != index)
                        builder.Add(arr[i]);
                }
                return builder.Build();
            }
            throw new JsonException($"Cannot remove from {parent.ValueType}");
        });
    }

    private static JsonValue ApplyReplace(JsonValue target, JsonPatchOperation operation)
    {
        var path = operation.Path;
        var value = operation.Value!;
        if (path.Tokens.Count == 0)
            return value;
        return ModifyAtPath(target, path, (parent, lastToken) =>
        {
            if (parent is JsonObject obj)
            {
                if (!obj.ContainsKey(lastToken))
                    throw new JsonException($"Property '{lastToken}' not found");
                var builder = Json.CreateObjectBuilder(obj);
                builder.Add(lastToken, value);
                return builder.Build();
            }
            else if (parent is JsonArray arr)
            {
                if (!int.TryParse(lastToken, out var index) || index < 0 || index >= arr.Count)
                    throw new JsonException($"Invalid array index: {lastToken}");
                var builder = Json.CreateArrayBuilder();
                for (int i = 0; i < arr.Count; i++)
                {
                    builder.Add(i == index ? value : arr[i]);
                }
                return builder.Build();
            }
            throw new JsonException($"Cannot replace in {parent.ValueType}");
        });
    }

    private static JsonValue ApplyMove(JsonValue target, JsonPatchOperation operation)
    {
        var fromPointer = operation.From!;
        var value = fromPointer.GetValue(target);        
        var afterRemove = ApplyOperation(target, JsonPatchOperation.Remove(fromPointer));
        return ApplyOperation(afterRemove, JsonPatchOperation.Add(operation.Path, value));
    }

    private static JsonValue ApplyCopy(JsonValue target, JsonPatchOperation operation)
    {
        var fromPointer = operation.From!;
        var value = fromPointer.GetValue(target);        
        return ApplyOperation(target, JsonPatchOperation.Add(operation.Path, value));
    }

    private static JsonValue ApplyTest(JsonValue target, JsonPatchOperation operation)
    {
        var path = operation.Path;
        var expectedValue = operation.Value!;
        if (!path.TryGetValue(target, out var actualValue))
            throw new JsonException($"Test failed: path '{path}' not found");
        if (!actualValue!.Equals(expectedValue))
            throw new JsonException($"Test failed: value at '{path}' does not match expected value");
        return target;
    }

    private static JsonValue ModifyAtPath(
        JsonValue target,
        JsonPointer path,
        Func<JsonValue, string, JsonValue> modifier)
    {
        var tokens = path.Tokens;
        if (tokens.Count == 0)
            throw new InvalidOperationException("Path has no tokens");
        if (tokens.Count == 1)
        {
            return modifier(target, tokens[0]);
        }
        var parentPath = new JsonPointer("/" + string.Join("/", 
            tokens.Take(tokens.Count - 1).Select(JsonPointer.EscapeToken)));
        var lastToken = tokens[tokens.Count - 1];
        return ModifyAtPath(target, parentPath, (parent, token) =>
        {
            JsonValue child;
            if (parent is JsonObject obj)
            {
                if (!obj.ContainsKey(token))
                    throw new JsonException($"Property '{token}' not found");
                child = obj[token];
            }
            else if (parent is JsonArray arr)
            {
                if (!int.TryParse(token, out var index) || index < 0 || index >= arr.Count)
                    throw new JsonException($"Invalid array index: {token}");
                child = arr[index];
            }
            else
            {
                throw new JsonException($"Cannot navigate into {parent.ValueType}");
            }
            var modifiedChild = modifier(child, lastToken);
            if (parent is JsonObject parentObj)
            {
                var builder = Json.CreateObjectBuilder(parentObj);
                builder.Add(token, modifiedChild);
                return builder.Build();
            }
            else if (parent is JsonArray parentArr)
            {
                if (!int.TryParse(token, out var index))
                    throw new JsonException($"Invalid array index: {token}");
                var builder = Json.CreateArrayBuilder();
                for (int i = 0; i < parentArr.Count; i++)
                {
                    builder.Add(i == index ? modifiedChild : parentArr[i]);
                }
                return builder.Build();
            }
            throw new JsonException($"Unexpected parent type: {parent.ValueType}");
        });
    }
}

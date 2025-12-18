namespace SergeiM.Json.Patch;

/// <summary>
/// Implements JSON Merge Patch (RFC 7386) for applying modifications to JSON objects.
/// This provides a simpler alternative to JSON Patch for common merge scenarios.
/// </summary>
public static class JsonMergePatch
{
    /// <summary>
    /// Applies a merge patch to a target JSON value.
    /// </summary>
    /// <param name="target">The target JSON value to patch.</param>
    /// <param name="patch">The merge patch to apply.</param>
    /// <returns>A new JSON value with the patch applied.</returns>
    /// <remarks>
    /// RFC 7386 rules:
    /// - If the patch is not an object, the result is the patch itself.
    /// - If the patch is an object:
    ///   - If target is not an object, create an empty object.
    ///   - For each property in the patch:
    ///     - If the value is null, remove the property from the target.
    ///     - Otherwise, recursively merge the value.
    /// </remarks>
    public static JsonValue Apply(JsonValue target, JsonValue patch)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(patch);
        return Merge(target, patch);
    }

    private static JsonValue Merge(JsonValue target, JsonValue patch)
    {
        if (patch is not JsonObject patchObj)
            return patch;
        var targetObj = target as JsonObject ?? JsonValue.EmptyJsonObject;
        var builder = Json.CreateObjectBuilder(targetObj);
        foreach (var property in patchObj)
        {
            var key = property.Key;
            var patchValue = property.Value;
            if (patchValue is JsonNull)
            {
                builder.Remove(key);
            }
            else
            {
                if (targetObj.TryGetValue(key, out var targetValue) && 
                    targetValue is JsonObject && 
                    patchValue is JsonObject)
                {
                    builder.Add(key, Merge(targetValue, patchValue));
                }
                else
                {
                    builder.Add(key, patchValue);
                }
            }
        }
        return builder.Build();
    }

    /// <summary>
    /// Creates a merge patch that transforms the source into the target.
    /// </summary>
    /// <param name="source">The source JSON object.</param>
    /// <param name="target">The target JSON object.</param>
    /// <returns>A merge patch that can transform source into target.</returns>
    public static JsonObject CreatePatch(JsonObject source, JsonObject target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);
        var builder = Json.CreateObjectBuilder();
        foreach (var key in source.Keys)
        {
            if (!target.ContainsKey(key))
            {
                builder.AddNull(key);
            }
        }
        foreach (var property in target)
        {
            var key = property.Key;
            var targetValue = property.Value;
            if (!source.TryGetValue(key, out var sourceValue))
            {
                builder.Add(key, targetValue);
            }
            else if (!sourceValue.Equals(targetValue))
            {
                if (sourceValue is JsonObject sourceObj && targetValue is JsonObject targetObj)
                {
                    var nestedPatch = CreatePatch(sourceObj, targetObj);
                    if (nestedPatch.Count > 0)
                    {
                        builder.Add(key, nestedPatch);
                    }
                }
                else
                {
                    builder.Add(key, targetValue);
                }
            }
        }
        return builder.Build();
    }
}

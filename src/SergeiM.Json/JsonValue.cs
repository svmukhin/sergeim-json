namespace SergeiM.Json;

/// <summary>
/// Represents an immutable JSON value. This is the base class for all JSON value types.
/// </summary>
public abstract class JsonValue
{
    /// <summary>
    /// JSON null value.
    /// </summary>
    public static readonly JsonValue Null = JsonNull.Instance;

    /// <summary>
    /// JSON true value.
    /// </summary>
    public static readonly JsonValue True = JsonBoolean.TrueInstance;

    /// <summary>
    /// JSON false value.
    /// </summary>
    public static readonly JsonValue False = JsonBoolean.FalseInstance;

    /// <summary>
    /// An empty JSON object.
    /// </summary>
    public static readonly JsonObject EmptyJsonObject = JsonObject.Empty;

    /// <summary>
    /// An empty JSON array.
    /// </summary>
    public static readonly JsonArray EmptyJsonArray = JsonArray.Empty;

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    /// <returns>The type of this JSON value.</returns>
    public abstract JsonValueType ValueType { get; }

    /// <summary>
    /// Returns this JSON value as a <see cref="JsonObject"/>.
    /// </summary>
    /// <returns>This JSON value cast to <see cref="JsonObject"/>.</returns>
    /// <exception cref="InvalidCastException">If this value is not a JSON object.</exception>
    public virtual JsonObject AsJsonObject()
    {
        if (this is JsonObject jsonObject)
        {
            return jsonObject;
        }
        throw new InvalidCastException($"Cannot cast {ValueType} to JsonObject");
    }

    /// <summary>
    /// Returns this JSON value as a <see cref="JsonArray"/>.
    /// </summary>
    /// <returns>This JSON value cast to <see cref="JsonArray"/>.</returns>
    /// <exception cref="InvalidCastException">If this value is not a JSON array.</exception>
    public virtual JsonArray AsJsonArray()
    {
        if (this is JsonArray jsonArray)
        {
            return jsonArray;
        }
        throw new InvalidCastException($"Cannot cast {ValueType} to JsonArray");
    }

    /// <summary>
    /// Returns a JSON representation of this value.
    /// </summary>
    /// <returns>A JSON string representation.</returns>
    public abstract override string ToString();

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public abstract override bool Equals(object? obj);

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public abstract override int GetHashCode();
}

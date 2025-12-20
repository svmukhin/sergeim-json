// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json;

/// <summary>
/// Represents a JSON boolean value (true or false). This is an immutable value with two singleton instances.
/// </summary>
public sealed class JsonBoolean : JsonValue
{
    /// <summary>
    /// The singleton instance representing JSON true.
    /// </summary>
    internal static readonly JsonBoolean TrueInstance = new(true);

    /// <summary>
    /// The singleton instance representing JSON false.
    /// </summary>
    internal static readonly JsonBoolean FalseInstance = new(false);

    private readonly bool _value;

    private JsonBoolean(bool value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the boolean value.
    /// </summary>
    public bool Value => _value;

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    public override JsonValueType ValueType => JsonValueType.Boolean;

    /// <summary>
    /// Returns a JSON representation of this value.
    /// </summary>
    /// <returns>The string "true" or "false".</returns>
    public override string ToString() => _value ? "true" : "false";

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is a JsonBoolean with the same value; otherwise, false.</returns>
    public override bool Equals(object? obj) => obj is JsonBoolean other && _value == other._value;

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Implicitly converts a JsonBoolean to a bool.
    /// </summary>
    /// <param name="jsonBoolean">The JSON boolean to convert.</param>
    public static implicit operator bool(JsonBoolean jsonBoolean) => jsonBoolean._value;
}

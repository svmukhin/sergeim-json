// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json;

/// <summary>
/// Represents the JSON null value. This is a singleton immutable value.
/// </summary>
public sealed class JsonNull : JsonValue
{
    /// <summary>
    /// The singleton instance of JSON null.
    /// </summary>
    internal static readonly JsonNull Instance = new();

    private JsonNull()
    {
    }

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    public override JsonValueType ValueType => JsonValueType.Null;

    /// <summary>
    /// Returns a JSON representation of this value.
    /// </summary>
    /// <returns>The string "null".</returns>
    public override string ToString() => "null";

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is a JsonNull; otherwise, false.</returns>
    public override bool Equals(object? obj) => obj is JsonNull;

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => 0;
}

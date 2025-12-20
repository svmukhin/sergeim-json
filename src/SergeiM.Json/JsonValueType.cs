// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json;

/// <summary>
/// Represents the type of a JSON value.
/// </summary>
public enum JsonValueType
{
    /// <summary>
    /// JSON object type.
    /// </summary>
    Object,

    /// <summary>
    /// JSON array type.
    /// </summary>
    Array,

    /// <summary>
    /// JSON string type.
    /// </summary>
    String,

    /// <summary>
    /// JSON number type.
    /// </summary>
    Number,

    /// <summary>
    /// JSON boolean type (true or false).
    /// </summary>
    Boolean,

    /// <summary>
    /// JSON null type.
    /// </summary>
    Null
}

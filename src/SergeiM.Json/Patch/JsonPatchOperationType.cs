// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Patch;

/// <summary>
/// Represents the type of a JSON Patch operation (RFC 6902).
/// </summary>
public enum JsonPatchOperationType
{
    /// <summary>
    /// Add operation - adds a value at a location.
    /// </summary>
    Add,

    /// <summary>
    /// Remove operation - removes a value at a location.
    /// </summary>
    Remove,

    /// <summary>
    /// Replace operation - replaces a value at a location.
    /// </summary>
    Replace,

    /// <summary>
    /// Move operation - moves a value from one location to another.
    /// </summary>
    Move,

    /// <summary>
    /// Copy operation - copies a value from one location to another.
    /// </summary>
    Copy,

    /// <summary>
    /// Test operation - tests that a value at a location equals a specified value.
    /// </summary>
    Test
}

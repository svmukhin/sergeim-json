// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.IO;

/// <summary>
/// Options for configuring JSON reader behavior.
/// </summary>
public sealed class JsonReaderOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether comments are allowed in the JSON input.
    /// Default is false.
    /// </summary>
    public bool AllowComments { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether trailing commas are allowed in the JSON input.
    /// Default is false.
    /// </summary>
    public bool AllowTrailingCommas { get; set; }

    /// <summary>
    /// Gets or sets the maximum depth allowed when parsing JSON. A depth of 0 means no maximum depth.
    /// Default is 64.
    /// </summary>
    public int MaxDepth { get; set; } = 64;

    /// <summary>
    /// Gets the default options.
    /// </summary>
    public static JsonReaderOptions Default { get; } = new JsonReaderOptions();
}

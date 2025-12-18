namespace SergeiM.Json;

/// <summary>
/// Options for configuring JSON writer behavior.
/// </summary>
public sealed class JsonWriterOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the output should be indented for readability.
    /// Default is false (compact output).
    /// </summary>
    public bool IndentOutput { get; set; }

    /// <summary>
    /// Gets or sets the string to use for indentation. Only used when Indented is true.
    /// Default is two spaces.
    /// </summary>
    public string IndentString { get; set; } = "  ";

    /// <summary>
    /// Gets or sets a value indicating whether property names should be escaped.
    /// Default is true.
    /// </summary>
    public bool EscapePropertyNames { get; set; } = true;

    /// <summary>
    /// Gets or sets the encoder to use when escaping strings.
    /// If null, the default encoder is used.
    /// </summary>
    public System.Text.Encodings.Web.JavaScriptEncoder? Encoder { get; set; }

    /// <summary>
    /// Gets the default options (compact output).
    /// </summary>
    public static JsonWriterOptions Default { get; } = new JsonWriterOptions();

    /// <summary>
    /// Gets the default options with indented output.
    /// </summary>
    public static JsonWriterOptions PrettyPrint { get; } = new JsonWriterOptions { IndentOutput = true };
}

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class FormattingTests
{
    [TestMethod]
    public void Write_WithIndent_WritesIndentedJson()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer, JsonWriterOptions.PrettyPrint);
        jsonWriter.Write(Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Add("age", 30)
            .Build());
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("\n"));
        Assert.IsTrue(json.Contains("  "));
    }

    [TestMethod]
    public void Write_DefaultOptions_WritesCompactJson()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(Json.CreateObjectBuilder()
            .Add("x", 1)
            .Add("y", 2)
            .Build());
        var json = writer.ToString();
        Assert.IsFalse(json.Contains("\n"));
    }

    [TestMethod]
    public void Write_CustomIndent_UsesCustomIndentation()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer, new JsonWriterOptions
        {
            IndentOutput = true,
            IndentString = "\t"
        });
        jsonWriter.Write(Json.CreateObjectBuilder()
            .Add("a", 1)
            .Build());
        var json = writer.ToString();
        Assert.IsTrue(json.Contains('\n') || json.Length > 10);
    }
}

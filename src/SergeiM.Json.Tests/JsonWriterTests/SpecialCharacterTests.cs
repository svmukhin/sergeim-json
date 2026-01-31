// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class SpecialCharacterTests
{
    [TestMethod]
    public void Write_StringWithQuotes_EscapesQuotes()
    {

        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.Write(new JsonString("She said \"hello\""));
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("She said"));
        Assert.IsTrue(json.Contains("hello"));
    }

    [TestMethod]
    public void Write_StringWithBackslash_EscapesBackslash()
    {
        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.Write(new JsonString("C:\\path\\to\\file"));
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("\\\\"));
    }

    [TestMethod]
    public void Write_StringWithNewline_EscapesNewline()
    {
        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.Write(new JsonString("Line1\nLine2"));
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("\\n"));
    }

    [TestMethod]
    public void Write_EmptyString_WritesEmptyQuotes()
    {
        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.Write(new JsonString(""));
        var json = writer.ToString();
        Assert.AreEqual("\"\"", json);
    }
}

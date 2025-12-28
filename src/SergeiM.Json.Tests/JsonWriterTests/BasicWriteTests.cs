// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class BasicWriteTests
{
    [TestMethod]
    public void Write_SimpleObject_WritesCorrectJson()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonObjectBuilder()
            .Add("name", "Alice")
            .Add("age", 30)
            .Build());
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("\"name\":\"Alice\""));
        Assert.IsTrue(json.Contains("\"age\":30"));
    }

    [TestMethod]
    public void Write_SimpleArray_WritesCorrectJson()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build());
        var json = writer.ToString();
        Assert.AreEqual("[1,2,3]", json);
    }

    [TestMethod]
    public void Write_String_WritesQuotedString()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonString("hello"));
        var json = writer.ToString();
        Assert.AreEqual("\"hello\"", json);
    }

    [TestMethod]
    public void Write_Number_WritesNumber()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonNumber(42));
        var json = writer.ToString();
        Assert.AreEqual("42", json);
    }

    [TestMethod]
    public void Write_Boolean_WritesBoolean()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(JsonValue.True);
        var json = writer.ToString();
        Assert.AreEqual("true", json);
    }

    [TestMethod]
    public void Write_Null_WritesNull()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(JsonValue.Null);
        var json = writer.ToString();
        Assert.AreEqual("null", json);
    }
}

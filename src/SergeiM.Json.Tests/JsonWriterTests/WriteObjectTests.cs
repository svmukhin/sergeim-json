// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class WriteObjectTests
{
    [TestMethod]
    public void WriteObject_EmptyObject_WritesEmptyBraces()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.WriteObject(JsonObject.Empty);
        var json = writer.ToString();
        Assert.AreEqual("{}", json);
    }

    [TestMethod]
    public void WriteObject_WithProperties_WritesAllProperties()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.WriteObject(new JsonObjectBuilder()
            .Add("x", 10)
            .Add("y", 20)
            .Build());
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("\"x\":10"));
        Assert.IsTrue(json.Contains("\"y\":20"));
    }

    [TestMethod]
    public void WriteObject_NestedObject_WritesNestedStructure()
    {
        var obj = new JsonObjectBuilder()
            .Add("outer", new JsonObjectBuilder()
                .Add("inner", "value")
                .Build())
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.WriteObject(obj);
        var json = writer.ToString();
        Assert.IsTrue(json.Contains("\"outer\":{\"inner\":\"value\"}"));
    }
}

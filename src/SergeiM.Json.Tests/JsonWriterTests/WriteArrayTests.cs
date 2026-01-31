// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class WriteArrayTests
{
    [TestMethod]
    public void WriteArray_EmptyArray_WritesEmptyBrackets()
    {
        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.WriteArray(JsonArray.Empty);
        var json = writer.ToString();
        Assert.AreEqual("[]", json);
    }

    [TestMethod]
    public void WriteArray_WithElements_WritesAllElements()
    {
        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.WriteArray(new JsonArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build());
        var json = writer.ToString();
        Assert.AreEqual("[1,2,3]", json);
    }

    [TestMethod]
    public void WriteArray_NestedArray_WritesNestedStructure()
    {
        var arr = new JsonArrayBuilder()
            .Add(new JsonArrayBuilder()
                .Add(1)
                .Add(2)
                .Build())
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = new JsonWriter(writer);
        jsonWriter.WriteArray(arr);
        var json = writer.ToString();
        Assert.AreEqual("[[1,2]]", json);
    }
}

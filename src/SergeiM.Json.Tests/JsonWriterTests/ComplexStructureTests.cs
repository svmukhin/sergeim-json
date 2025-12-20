// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class ComplexStructureTests
{
    [TestMethod]
    public void Write_ComplexNestedStructure_WritesCorrectly()
    {
        var json = Json.CreateObjectBuilder()
            .Add("person", Json.CreateObjectBuilder()
                .Add("name", "John")
                .Add("age", 30)
                .Add("address", Json.CreateObjectBuilder()
                    .Add("street", "Main St")
                    .Add("city", "New York"))
                .Add("phones", Json.CreateArrayBuilder()
                    .Add("555-1234")
                    .Add("555-5678")))
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(json);
        var result = writer.ToString();
        Assert.IsTrue(result.Contains("\"person\":{"));
        Assert.IsTrue(result.Contains("\"name\":\"John\""));
        Assert.IsTrue(result.Contains("\"phones\":[\"555-1234\",\"555-5678\"]"));
    }

    [TestMethod]
    public void Write_ArrayOfObjects_WritesCorrectly()
    {
        var arr = Json.CreateArrayBuilder()
            .Add(Json.CreateObjectBuilder()
                .Add("id", 1)
                .Add("name", "Alice"))
            .Add(Json.CreateObjectBuilder()
                .Add("id", 2)
                .Add("name", "Bob"))
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(arr);
        var result = writer.ToString();
        Assert.IsTrue(result.Contains("{\"id\":1,\"name\":\"Alice\"}") ||
                     result.Contains("{\"name\":\"Alice\",\"id\":1}"));
        Assert.IsTrue(result.Contains("{\"id\":2,\"name\":\"Bob\"}") ||
                     result.Contains("{\"name\":\"Bob\",\"id\":2}"));
    }
}

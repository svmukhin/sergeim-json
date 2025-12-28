// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonBuilderTests;

[TestClass]
public class FluentApiTests
{
    [TestMethod]
    public void ComplexNestedStructure_BuildsCorrectly()
    {
        var json = new JsonObjectBuilder()
            .Add("name", "John")
            .Add("age", 30)
            .Add("address", new JsonObjectBuilder()
                .Add("street", "Main St")
                .Add("city", "New York"))
            .Add("phones", new JsonArrayBuilder()
                .Add("555-1234")
                .Add("555-5678"))
            .Build();
        Assert.AreEqual("John", json.GetString("name"));
        Assert.AreEqual(30, json.GetInt("age"));
        var address = json.GetJsonObject("address");
        Assert.AreEqual("Main St", address!.GetString("street"));
        Assert.AreEqual("New York", address!.GetString("city"));
        var phones = json.GetJsonArray("phones");
        Assert.AreEqual(2, phones!.Count);
        Assert.AreEqual("555-1234", phones.GetString(0));
        Assert.AreEqual("555-5678", phones.GetString(1));
    }

    [TestMethod]
    public void DeepNesting_BuildsCorrectly()
    {
        var json = new JsonObjectBuilder()
            .Add("level1", new JsonObjectBuilder()
                .Add("level2", new JsonObjectBuilder()
                    .Add("level3", new JsonObjectBuilder()
                        .Add("value", 42))))
            .Build();
        var level1 = json.GetJsonObject("level1");
        var level2 = level1!.GetJsonObject("level2");
        var level3 = level2!.GetJsonObject("level3");
        Assert.AreEqual(42, level3!.GetInt("value"));
    }

    [TestMethod]
    public void ArrayOfObjects_BuildsCorrectly()
    {
        var json = new JsonArrayBuilder()
            .Add(new JsonObjectBuilder()
                .Add("id", 1)
                .Add("name", "Alice"))
            .Add(new JsonObjectBuilder()
                .Add("id", 2)
                .Add("name", "Bob"))
            .Build();
        Assert.AreEqual(2, json.Count);
        var first = json.GetJsonObject(0);
        Assert.AreEqual(1, first.GetInt("id"));
        Assert.AreEqual("Alice", first.GetString("name"));
        var second = json.GetJsonObject(1);
        Assert.AreEqual(2, second.GetInt("id"));
        Assert.AreEqual("Bob", second.GetString("name"));
    }
}

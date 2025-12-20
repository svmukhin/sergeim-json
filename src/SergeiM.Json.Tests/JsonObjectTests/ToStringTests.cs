// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class ToStringTests
{
    [TestMethod]
    public void ToString_WithEmptyObject_ReturnsEmptyObjectJson()
    {
        Assert.AreEqual("{}", JsonObject.Empty.ToString());
    }

    [TestMethod]
    public void ToString_WithSimpleValues_ReturnsValidJson()
    {
        var json = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Add("age", 30)
            .Build().ToString();
        Assert.IsTrue(
            json == "{\"name\":\"Alice\",\"age\":30}" ||
            json == "{\"age\":30,\"name\":\"Alice\"}");
    }

    [TestMethod]
    public void ToString_WithNestedObject_ReturnsValidJson()
    {
        var json = Json.CreateObjectBuilder()
            .Add("person", Json.CreateObjectBuilder()
                .Add("name", "Bob")
                .Build())
            .Build().ToString();
        Assert.AreEqual("{\"person\":{\"name\":\"Bob\"}}", json);
    }
}

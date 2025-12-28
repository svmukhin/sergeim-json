// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonArrayTests;

[TestClass]
public class ToStringTests
{
    [TestMethod]
    public void ToString_WithEmptyArray_ReturnsEmptyArrayJson()
    {
        Assert.AreEqual("[]", JsonArray.Empty.ToString());
    }

    [TestMethod]
    public void ToString_WithSimpleValues_ReturnsValidJson()
    {
        var json = new JsonArrayBuilder()
            .Add("Alice")
            .Add(30)
            .Add(true)
            .Build().ToString();
        Assert.AreEqual("[\"Alice\",30,true]", json);
    }

    [TestMethod]
    public void ToString_WithNestedArray_ReturnsValidJson()
    {
        var json = new JsonArrayBuilder()
            .Add(new JsonArrayBuilder()
                .Add(1)
                .Add(2)
                .Build())
            .Build().ToString();
        Assert.AreEqual("[[1,2]]", json);
    }

    [TestMethod]
    public void ToString_WithNestedObject_ReturnsValidJson()
    {
        var json = new JsonArrayBuilder()
            .Add(new JsonObjectBuilder()
                .Add("x", 10)
                .Build())
            .Build().ToString();
        Assert.AreEqual("[{\"x\":10}]", json);
    }
}

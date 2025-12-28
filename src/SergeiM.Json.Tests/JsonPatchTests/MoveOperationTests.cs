// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class MoveOperationTests
{
    [TestMethod]
    public void Move_Property_MovesValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Add("y", 20)
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Move("/x", "/z")).Apply(obj);
        Assert.IsFalse(result.ContainsKey("x"));
        Assert.AreEqual(20, result.GetInt("y"));
        Assert.AreEqual(10, result.GetInt("z"));
    }

    [TestMethod]
    public void Move_ArrayElement_MovesElement()
    {
        var arr = new JsonArrayBuilder()
            .Add("a")
            .Add("b")
            .Add("c")
            .Build();
        var result = (JsonArray)new JsonPatch(JsonPatchOperation.Move("/2", "/0")).Apply(arr);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("c", ((JsonString)result[0]).Value);
        Assert.AreEqual("a", ((JsonString)result[1]).Value);
        Assert.AreEqual("b", ((JsonString)result[2]).Value);
    }

    [TestMethod]
    public void Move_BetweenObjects_MovesValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("source", new JsonObjectBuilder()
                .Add("value", 42))
            .Add("target", new JsonObjectBuilder()
                .Add("other", 10))
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Move("/source/value", "/target/moved")).Apply(obj);
        Assert.IsFalse(result.GetJsonObject("source")!.ContainsKey("value"));
        Assert.AreEqual(42, result.GetJsonObject("target")!.GetInt("moved"));
    }
}

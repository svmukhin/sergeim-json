// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class AddOperationTests
{
    [TestMethod]
    public void Add_PropertyToObject_AddsProperty()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Add("/y", new JsonNumber(20))).Apply(obj);
        Assert.AreEqual(10, result.GetInt("x"));
        Assert.AreEqual(20, result.GetInt("y"));
    }

    [TestMethod]
    public void Add_ToNestedObject_AddsProperty()
    {
        var obj = new JsonObjectBuilder()
            .Add("data", new JsonObjectBuilder()
                .Add("x", 1))
            .Build();
        var data = ((JsonObject)new JsonPatch(JsonPatchOperation.Add("/data/y", new JsonNumber(2))).Apply(obj)).GetJsonObject("data")!;
        Assert.AreEqual(1, data.GetInt("x"));
        Assert.AreEqual(2, data.GetInt("y"));
    }

    [TestMethod]
    public void Add_ElementToArray_InsertsElement()
    {
        var arr = new JsonArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(4)
            .Build();
        var result = (JsonArray)new JsonPatch(JsonPatchOperation.Add("/2", new JsonNumber(3))).Apply(arr);
        Assert.AreEqual(4, result.Count);
        Assert.AreEqual(1, ((JsonNumber)result[0]).IntValue());
        Assert.AreEqual(2, ((JsonNumber)result[1]).IntValue());
        Assert.AreEqual(3, ((JsonNumber)result[2]).IntValue());
        Assert.AreEqual(4, ((JsonNumber)result[3]).IntValue());
    }

    [TestMethod]
    public void Add_ToEndOfArray_AppendsElement()
    {
        var arr = new JsonArrayBuilder()
            .Add(1)
            .Add(2)
            .Build();
        var result = (JsonArray)new JsonPatch(JsonPatchOperation.Add("/-", new JsonNumber(3))).Apply(arr);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(3, ((JsonNumber)result[2]).IntValue());
    }

    [TestMethod]
    public void Add_RootValue_ReplacesRoot()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 1)
            .Build();
        var result = new JsonPatch(JsonPatchOperation.Add("", new JsonString("new"))).Apply(obj);
        Assert.AreEqual("new", ((JsonString)result).Value);
    }

    [TestMethod]
    public void Add_ExistingProperty_ReplacesValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Add("/x", new JsonNumber(20))).Apply(obj);
        Assert.AreEqual(20, result.GetInt("x"));
    }
}

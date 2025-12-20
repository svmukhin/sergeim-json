// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class GetValueTests
{
    [TestMethod]
    public void GetValue_EmptyPointer_ReturnsRoot()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        Assert.AreSame(obj, JsonPointer.Empty.GetValue(obj));
    }

    [TestMethod]
    public void GetValue_SimpleProperty_ReturnsValue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Add("age", 30)
            .Build();
        Assert.AreEqual("Alice", ((JsonString)new JsonPointer("/name").GetValue(obj)).Value);
    }

    [TestMethod]
    public void GetValue_NestedProperty_ReturnsValue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("person", Json.CreateObjectBuilder()
                .Add("name", "Bob")
                .Add("age", 25))
            .Build();
        Assert.AreEqual("Bob", ((JsonString)new JsonPointer("/person/name").GetValue(obj)).Value);
    }

    [TestMethod]
    public void GetValue_ArrayIndex_ReturnsElement()
    {
        var arr = Json.CreateArrayBuilder()
            .Add("first")
            .Add("second")
            .Add("third")
            .Build();
        Assert.AreEqual("second", ((JsonString)new JsonPointer("/1").GetValue(arr)).Value);
    }

    [TestMethod]
    public void GetValue_NestedArray_ReturnsElement()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("items", Json.CreateArrayBuilder()
                .Add(10)
                .Add(20)
                .Add(30))
            .Build();
        Assert.AreEqual(30, ((JsonNumber)new JsonPointer("/items/2").GetValue(obj)).IntValue());
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void GetValue_NonExistentProperty_ThrowsJsonException()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 1)
            .Build();
        new JsonPointer("/missing").GetValue(obj);
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void GetValue_InvalidArrayIndex_ThrowsJsonException()
    {
        var arr = Json.CreateArrayBuilder()
            .Add(1)
            .Build();
        new JsonPointer("/5").GetValue(arr);
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonBuilderTests;

[TestClass]
public class JsonObjectBuilderTests
{
    [TestMethod]
    public void Build_EmptyBuilder_CreatesEmptyObject()
    {
        var obj = new JsonObjectBuilder().Build();
        Assert.AreEqual(0, obj.Count);
        Assert.AreEqual(JsonValueType.Object, obj.ValueType);
    }

    [TestMethod]
    public void Add_WithString_AddsStringValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("name", "Alice")
            .Build();
        Assert.AreEqual("Alice", obj.GetString("name"));
    }

    [TestMethod]
    public void Add_WithInt_AddsNumberValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("age", 30)
            .Build();
        Assert.AreEqual(30, obj.GetInt("age"));
    }

    [TestMethod]
    public void Add_WithLong_AddsNumberValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("big", 9999999999L)
            .Build();
        Assert.AreEqual(9999999999L, obj.GetLong("big"));
    }

    [TestMethod]
    public void Add_WithDouble_AddsNumberValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("pi", 3.14)
            .Build();
        Assert.AreEqual(3.14, obj.GetDouble("pi"), 0.001);
    }

    [TestMethod]
    public void Add_WithDecimal_AddsNumberValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("price", 19.99m)
            .Build();
        Assert.AreEqual(19.99m, ((JsonNumber)obj["price"]).DecimalValue());
    }

    [TestMethod]
    public void Add_WithBoolean_AddsBooleanValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("active", true)
            .Build();
        Assert.IsTrue(obj.GetBoolean("active"));
    }

    [TestMethod]
    public void AddNull_AddsNullValue()
    {
        var obj = new JsonObjectBuilder()
            .AddNull("empty")
            .Build();
        Assert.IsTrue(obj.IsNull("empty"));
    }

    [TestMethod]
    public void Add_WithJsonObject_AddsObjectValue()
    {
        var result = new JsonObjectBuilder()
            .Add("nested", new JsonObjectBuilder().Add("x", 1).Build())
            .Build().GetJsonObject("nested");
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result!.GetInt("x"));
    }

    [TestMethod]
    public void Add_WithJsonArray_AddsArrayValue()
    {
        var result = new JsonObjectBuilder()
            .Add("items", new JsonArrayBuilder().Add(1).Add(2).Build())
            .Build().GetJsonArray("items");
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result!.Count);
    }

    [TestMethod]
    public void Add_WithJsonObjectBuilder_AddsObjectValue()
    {
        var result = new JsonObjectBuilder()
            .Add("nested", new JsonObjectBuilder().Add("y", 2))
            .Build().GetJsonObject("nested");
        Assert.AreEqual(2, result!.GetInt("y"));
    }

    [TestMethod]
    public void Add_WithJsonArrayBuilder_AddsArrayValue()
    {
        var result = new JsonObjectBuilder()
            .Add("items", new JsonArrayBuilder().Add(3).Add(4))
            .Build().GetJsonArray("items");
        Assert.AreEqual(2, result!.Count);
        Assert.AreEqual(3, result.GetInt(0));
    }

    [TestMethod]
    public void Add_MultipleProperties_AllAdded()
    {
        var obj = new JsonObjectBuilder()
            .Add("a", 1)
            .Add("b", 2)
            .Add("c", 3)
            .Build();
        Assert.AreEqual(3, obj.Count);
        Assert.AreEqual(1, obj.GetInt("a"));
        Assert.AreEqual(2, obj.GetInt("b"));
        Assert.AreEqual(3, obj.GetInt("c"));
    }

    [TestMethod]
    public void Add_DuplicateKey_ReplacesValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("key", 1)
            .Add("key", 2)
            .Build();
        Assert.AreEqual(1, obj.Count);
        Assert.AreEqual(2, obj.GetInt("key"));
    }

    [TestMethod]
    public void Remove_ExistingKey_RemovesProperty()
    {
        var obj = new JsonObjectBuilder()
            .Add("a", 1)
            .Add("b", 2)
            .Remove("a")
            .Build();
        Assert.AreEqual(1, obj.Count);
        Assert.IsFalse(obj.ContainsKey("a"));
        Assert.AreEqual(2, obj.GetInt("b"));
    }

    [TestMethod]
    public void Remove_NonExistingKey_NoEffect()
    {
        var obj = new JsonObjectBuilder()
            .Add("a", 1)
            .Remove("missing")
            .Build();
        Assert.AreEqual(1, obj.Count);
    }

    [TestMethod]
    public void Build_MultipleTimes_CreatesIndependentObjects()
    {
        var builder = new JsonObjectBuilder().Add("x", 1);
        var obj1 = builder.Build();
        builder.Add("y", 2);
        var obj2 = builder.Build();
        Assert.AreEqual(1, obj1.Count);
        Assert.AreEqual(2, obj2.Count);
    }

    [TestMethod]
    public void CreateObjectBuilder_WithExistingObject_CopiesProperties()
    {
        var original = new JsonObjectBuilder()
            .Add("a", 1)
            .Add("b", 2)
            .Build();
        var modified = new JsonObjectBuilder(original)
            .Add("c", 3)
            .Build();
        Assert.AreEqual(2, original.Count);
        Assert.AreEqual(3, modified.Count);
        Assert.AreEqual(1, modified.GetInt("a"));
        Assert.AreEqual(2, modified.GetInt("b"));
        Assert.AreEqual(3, modified.GetInt("c"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_WithNullName_ThrowsArgumentNullException()
    {
        new JsonObjectBuilder().Add(null!, "value");
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class BasicReadTests
{
    [TestMethod]
    public void Read_SimpleObject_ReturnsJsonObject()
    {
        using var reader = new JsonReader(new StringReader("{\"name\":\"Alice\",\"age\":30}"));
        var value = reader.Read();
        Assert.IsNotNull(value);
        Assert.AreEqual(JsonValueType.Object, value.ValueType);
        var obj = value.AsJsonObject();
        Assert.AreEqual("Alice", obj.GetString("name"));
        Assert.AreEqual(30, obj.GetInt("age"));
    }

    [TestMethod]
    public void Read_SimpleArray_ReturnsJsonArray()
    {
        using var reader = new JsonReader(new StringReader("[1,2,3]"));
        var value = reader.Read();
        Assert.IsNotNull(value);
        Assert.AreEqual(JsonValueType.Array, value.ValueType);
        var arr = value.AsJsonArray();
        Assert.AreEqual(3, arr.Count);
        Assert.AreEqual(1, arr.GetInt(0));
        Assert.AreEqual(2, arr.GetInt(1));
        Assert.AreEqual(3, arr.GetInt(2));
    }

    [TestMethod]
    public void Read_String_ReturnsJsonString()
    {
        using var reader = new JsonReader(new StringReader("\"hello\""));
        var value = reader.Read();
        Assert.IsNotNull(value);
        Assert.AreEqual(JsonValueType.String, value.ValueType);
        Assert.AreEqual("hello", ((JsonString)value).Value);
    }

    [TestMethod]
    public void Read_Number_ReturnsJsonNumber()
    {
        using var reader = new JsonReader(new StringReader("42"));
        var value = reader.Read();
        Assert.IsNotNull(value);
        Assert.AreEqual(JsonValueType.Number, value.ValueType);
        Assert.AreEqual(42, ((JsonNumber)value).IntValue());
    }

    [TestMethod]
    public void Read_Boolean_ReturnsJsonBoolean()
    {
        using var reader = new JsonReader(new StringReader("true"));
        var value = reader.Read();
        Assert.IsNotNull(value);
        Assert.AreEqual(JsonValueType.Boolean, value.ValueType);
        Assert.IsTrue(((JsonBoolean)value).Value);
    }

    [TestMethod]
    public void Read_Null_ReturnsJsonNull()
    {
        using var reader = new JsonReader(new StringReader("null"));
        var value = reader.Read();
        Assert.IsNotNull(value);
        Assert.AreEqual(JsonValueType.Null, value.ValueType);
    }
}

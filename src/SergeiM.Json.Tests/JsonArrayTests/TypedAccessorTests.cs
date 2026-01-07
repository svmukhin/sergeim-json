// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonArrayTests;

[TestClass]
public class TypedAccessorTests
{
    private JsonArray CreateTestArray()
    {
        return new JsonArrayBuilder()
            .Add("hello")
            .Add(123)
            .Add(true)
            .AddNull()
            .Add(new JsonObjectBuilder()
                .Add("nested", "value")
                .Build())
            .Add(new JsonArrayBuilder()
                .Add(1)
                .Add(2)
                .Build())
            .Build();
    }

    [TestMethod]
    public void GetString_WithStringValue_ReturnsString()
    {
        Assert.AreEqual("hello", CreateTestArray().GetString(0));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void GetString_WithInvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var arr = CreateTestArray();
        _ = arr.GetString(99);
    }

    [TestMethod]
    public void GetInt_WithNumberValue_ReturnsInt()
    {
        Assert.AreEqual(123, CreateTestArray().GetInt(1));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void GetInt_WithInvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var arr = CreateTestArray();
        _ = arr.GetInt(99);
    }

    [TestMethod]
    public void GetBoolean_WithBooleanValue_ReturnsBoolean()
    {
        Assert.IsTrue(CreateTestArray().GetBoolean(2));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void GetBoolean_WithInvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var arr = CreateTestArray();
        _ = arr.GetBoolean(99);
    }

    [TestMethod]
    public void GetJsonObject_WithObjectValue_ReturnsObject()
    {
        var result = CreateTestArray().GetJsonObject(4);
        Assert.IsNotNull(result);
        Assert.AreEqual("value", result.GetString("nested"));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCastException))]
    public void GetJsonObject_WithNonObjectValue_ThrowsInvalidCastException()
    {
        var arr = CreateTestArray();
        _ = arr.GetJsonObject(0);
    }

    [TestMethod]
    public void GetJsonArray_WithArrayValue_ReturnsArray()
    {
        var result = CreateTestArray().GetJsonArray(5);
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, result.GetInt(0));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCastException))]
    public void GetJsonArray_WithNonArrayValue_ThrowsInvalidCastException()
    {
        var arr = CreateTestArray();
        _ = arr.GetJsonArray(0);
    }

    [TestMethod]
    public void IsNull_WithNullValue_ReturnsTrue()
    {
        Assert.IsTrue(CreateTestArray().IsNull(3));
    }

    [TestMethod]
    public void IsNull_WithNonNullValue_ReturnsFalse()
    {
        Assert.IsFalse(CreateTestArray().IsNull(0));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void IsNull_WithInvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var arr = CreateTestArray();
        _ = arr.IsNull(99);
    }
}

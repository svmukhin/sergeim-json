// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class TypedAccessorTests
{
    private JsonObject CreateTestObject()
    {
        return new JsonObjectBuilder()
            .Add("string", "hello")
            .Add("number", 123)
            .Add("boolean", true)
            .AddNull("null")
            .Add("object", new JsonObjectBuilder()
                .Add("nested", "value")
                .Build())
            .Add("array", new JsonArrayBuilder()
                .Add(1)
                .Add(2)
                .Build())
            .Build();
    }

    [TestMethod]
    public void GetString_WithStringValue_ReturnsString()
    {
        Assert.AreEqual("hello", CreateTestObject().GetString("string"));
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void GetString_WithMissingKey_ThrowsKeyNotFoundException()
    {
        var obj = CreateTestObject();
        _ = obj.GetString("missing");
    }

    [TestMethod]
    public void GetString_WithDefaultValue_ReturnsValue()
    {
        Assert.AreEqual("hello", CreateTestObject().GetString("string", "default"));
    }

    [TestMethod]
    public void GetString_WithMissingKeyAndDefault_ReturnsDefault()
    {
        Assert.AreEqual("default", CreateTestObject().GetString("missing", "default"));
    }

    [TestMethod]
    public void GetInt_WithNumberValue_ReturnsInt()
    {
        Assert.AreEqual(123, CreateTestObject().GetInt("number"));
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void GetInt_WithMissingKey_ThrowsKeyNotFoundException()
    {
        var obj = CreateTestObject();
        _ = obj.GetInt("missing");
    }

    [TestMethod]
    public void GetInt_WithDefaultValue_ReturnsDefault()
    {
        Assert.AreEqual(999, CreateTestObject().GetInt("missing", 999));
    }

    [TestMethod]
    public void GetBoolean_WithBooleanValue_ReturnsBoolean()
    {
        Assert.IsTrue(CreateTestObject().GetBoolean("boolean"));
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void GetBoolean_WithMissingKey_ThrowsKeyNotFoundException()
    {
        var obj = CreateTestObject();
        _ = obj.GetBoolean("missing");
    }

    [TestMethod]
    public void GetJsonObject_WithObjectValue_ReturnsObject()
    {
        var result = CreateTestObject().GetJsonObject("object");
        Assert.IsNotNull(result);
        Assert.AreEqual("value", result.GetString("nested"));
    }

    [TestMethod]
    public void GetJsonObject_WithMissingKey_ReturnsNull()
    {
        Assert.IsNull(CreateTestObject().GetJsonObject("missing"));
    }

    [TestMethod]
    public void GetJsonArray_WithArrayValue_ReturnsArray()
    {
        var result = CreateTestObject().GetJsonArray("array");
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, result.GetInt(0));
    }

    [TestMethod]
    public void GetJsonArray_WithMissingKey_ReturnsNull()
    {
        Assert.IsNull(CreateTestObject().GetJsonArray("missing"));
    }

    [TestMethod]
    public void IsNull_WithNullValue_ReturnsTrue()
    {
        Assert.IsTrue(CreateTestObject().IsNull("null"));
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void IsNull_WithMissingKey_ThrowsKeyNotFoundException()
    {
        var obj = CreateTestObject();
        _ = obj.IsNull("missing");
    }

    [TestMethod]
    public void IsNull_WithNonNullValue_ReturnsFalse()
    {
        Assert.IsFalse(CreateTestObject().IsNull("string"));
    }
}

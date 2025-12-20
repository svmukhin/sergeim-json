// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonValueTests;

[TestClass]
public class JsonNullTests
{
    [TestMethod]
    public void Null_ShouldBeSingleton()
    {
        var null1 = JsonValue.Null;
        var null2 = JsonValue.Null;
        Assert.AreSame(null1, null2);
    }

    [TestMethod]
    public void Null_ShouldHaveCorrectValueType()
    {
        Assert.AreEqual(JsonValueType.Null, JsonValue.Null.ValueType);
    }

    [TestMethod]
    public void Null_ToString_ShouldReturnNull()
    {
        Assert.AreEqual("null", JsonValue.Null.ToString());
    }

    [TestMethod]
    public void Null_Equals_ShouldBeTrue()
    {
        Assert.IsTrue(JsonValue.Null.Equals(JsonValue.Null));
    }

    [TestMethod]
    public void Null_GetHashCode_ShouldBeConsistent()
    {
        var hash1 = JsonValue.Null.GetHashCode();
        var hash2 = JsonValue.Null.GetHashCode();
        Assert.AreEqual(hash1, hash2);
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonValueTests;

[TestClass]
public class JsonBooleanTests
{
    [TestMethod]
    public void True_ShouldBeSingleton()
    {
        var true1 = JsonValue.True;
        var true2 = JsonValue.True;
        Assert.AreSame(true1, true2);
    }

    [TestMethod]
    public void False_ShouldBeSingleton()
    {
        var false1 = JsonValue.False;
        var false2 = JsonValue.False;
        Assert.AreSame(false1, false2);
    }

    [TestMethod]
    public void Boolean_ShouldHaveCorrectValueType()
    {
        Assert.AreEqual(JsonValueType.Boolean, JsonValue.True.ValueType);
        Assert.AreEqual(JsonValueType.Boolean, JsonValue.False.ValueType);
    }

    [TestMethod]
    public void True_ToString_ShouldReturnTrue()
    {
        Assert.AreEqual("true", JsonValue.True.ToString());
    }

    [TestMethod]
    public void False_ToString_ShouldReturnFalse()
    {
        Assert.AreEqual("false", JsonValue.False.ToString());
    }

    [TestMethod]
    public void Boolean_Value_ShouldBeCorrect()
    {
        Assert.IsTrue(((JsonBoolean)JsonValue.True).Value);
        Assert.IsFalse(((JsonBoolean)JsonValue.False).Value);
    }

    [TestMethod]
    public void Boolean_ImplicitConversion_ShouldWork()
    {
        bool trueValue = (JsonBoolean)JsonValue.True;
        bool falseValue = (JsonBoolean)JsonValue.False;
        Assert.IsTrue(trueValue);
        Assert.IsFalse(falseValue);
    }

    [TestMethod]
    public void Boolean_Equals_ShouldWorkCorrectly()
    {
        Assert.IsTrue(JsonValue.True.Equals(JsonValue.True));
        Assert.IsTrue(JsonValue.False.Equals(JsonValue.False));
        Assert.IsFalse(JsonValue.True.Equals(JsonValue.False));
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonArrayTests;

[TestClass]
public class BuilderTests
{
    [TestMethod]
    public void EmptyArray_HasZeroCount()
    {
        var arr = JsonArray.Empty;
        Assert.AreEqual(0, arr.Count);
        Assert.AreEqual(JsonValueType.Array, arr.ValueType);
    }

    [TestMethod]
    public void Builder_WithValues_ContainsValues()
    {
        var arr = new JsonArrayBuilder()
            .Add("hello")
            .Add(42)
            .Add(true)
            .Build();
        Assert.AreEqual(3, arr.Count);
        Assert.AreEqual("hello", ((JsonString)arr[0]).Value);
        Assert.AreEqual(42, ((JsonNumber)arr[1]).IntValue());
        Assert.AreEqual(true, ((JsonBoolean)arr[2]).Value);
    }

    [TestMethod]
    public void Builder_Immutable_DoesNotAffectOriginal()
    {
        var builder = new JsonArrayBuilder().Add(1);
        var arr1 = builder.Build();
        builder.Add(2);
        var arr2 = builder.Build();
        Assert.AreEqual(1, arr1.Count);
        Assert.AreEqual(2, arr2.Count);
    }
}

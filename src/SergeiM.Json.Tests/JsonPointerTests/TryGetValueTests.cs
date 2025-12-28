// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class TryGetValueTests
{
    [TestMethod]
    public void TryGetValue_ExistingProperty_ReturnsTrue()
    {
        var obj = new JsonObjectBuilder()
            .Add("value", 42)
            .Build();
        var success = new JsonPointer("/value").TryGetValue(obj, out var result);
        Assert.IsTrue(success);
        Assert.IsNotNull(result);
        Assert.AreEqual(42, ((JsonNumber)result!).IntValue());
    }

    [TestMethod]
    public void TryGetValue_NonExistentProperty_ReturnsFalse()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 1)
            .Build();
        var success = new JsonPointer("/missing").TryGetValue(obj, out var result);
        Assert.IsFalse(success);
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TryGetValue_InvalidArrayIndex_ReturnsFalse()
    {
        var arr = new JsonArrayBuilder()
            .Add(1)
            .Build();
        var success = new JsonPointer("/10").TryGetValue(arr, out var result);
        Assert.IsFalse(success);
        Assert.IsNull(result);
    }
}

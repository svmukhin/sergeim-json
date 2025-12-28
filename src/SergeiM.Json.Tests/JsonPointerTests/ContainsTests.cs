// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class ContainsTests
{
    [TestMethod]
    public void Contains_ExistingProperty_ReturnsTrue()
    {
        var obj = new JsonObjectBuilder()
            .Add("name", "Alice")
            .Build();
        Assert.IsTrue(new JsonPointer("/name").Contains(obj));
    }

    [TestMethod]
    public void Contains_NonExistentProperty_ReturnsFalse()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 1)
            .Build();
        Assert.IsFalse(new JsonPointer("/missing").Contains(obj));
    }

    [TestMethod]
    public void Contains_ValidArrayIndex_ReturnsTrue()
    {
        var arr = new JsonArrayBuilder()
            .Add(1)
            .Add(2)
            .Build();
        Assert.IsTrue(new JsonPointer("/1").Contains(arr));
    }

    [TestMethod]
    public void Contains_InvalidArrayIndex_ReturnsFalse()
    {
        var arr = new JsonArrayBuilder()
            .Add(1)
            .Build();
        Assert.IsFalse(new JsonPointer("/5").Contains(arr));
    }
}

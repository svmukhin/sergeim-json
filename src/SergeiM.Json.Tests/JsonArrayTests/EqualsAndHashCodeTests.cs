// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonArrayTests;

[TestClass]
public class EqualsAndHashCodeTests
{
    [TestMethod]
    public void Equals_WithSameContent_ReturnsTrue()
    {
        var arr1 = new JsonArrayBuilder()
            .Add("Alice")
            .Add(25)
            .Build();
        var arr2 = new JsonArrayBuilder()
            .Add("Alice")
            .Add(25)
            .Build();
        Assert.IsTrue(arr1.Equals(arr2));
        Assert.AreEqual(arr1.GetHashCode(), arr2.GetHashCode());
    }

    [TestMethod]
    public void Equals_WithDifferentContent_ReturnsFalse()
    {
        var arr1 = new JsonArrayBuilder()
            .Add("Alice")
            .Build();
        var arr2 = new JsonArrayBuilder()
            .Add("Bob")
            .Build();
        Assert.IsFalse(arr1.Equals(arr2));
    }

    [TestMethod]
    public void Equals_WithDifferentLength_ReturnsFalse()
    {
        var arr1 = new JsonArrayBuilder()
            .Add(1)
            .Build();
        var arr2 = new JsonArrayBuilder()
            .Add(1)
            .Add(2)
            .Build();
        Assert.IsFalse(arr1.Equals(arr2));
    }

    [TestMethod]
    public void Equals_WithNull_ReturnsFalse()
    {
        Assert.IsFalse(JsonArray.Empty.Equals(null));
    }

    [TestMethod]
    public void Equals_WithSameInstance_ReturnsTrue()
    {
        var arr = new JsonArrayBuilder()
            .Add(1)
            .Build();
        Assert.IsTrue(arr.Equals(arr));
    }
}

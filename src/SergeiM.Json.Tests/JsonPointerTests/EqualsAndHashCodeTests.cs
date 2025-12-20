// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class EqualsAndHashCodeTests
{
    [TestMethod]
    public void Equals_SamePointer_ReturnsTrue()
    {
        var pointer1 = new JsonPointer("/foo/bar");
        var pointer2 = new JsonPointer("/foo/bar");
        Assert.IsTrue(pointer1.Equals(pointer2));
        Assert.AreEqual(pointer1.GetHashCode(), pointer2.GetHashCode());
    }

    [TestMethod]
    public void Equals_DifferentPointer_ReturnsFalse()
    {
        var pointer1 = new JsonPointer("/foo");
        var pointer2 = new JsonPointer("/bar");
        Assert.IsFalse(pointer1.Equals(pointer2));
    }

    [TestMethod]
    public void Equals_WithNull_ReturnsFalse()
    {
        Assert.IsFalse(new JsonPointer("/foo").Equals(null));
    }

    [TestMethod]
    public void Equals_SameInstance_ReturnsTrue()
    {
        Assert.IsTrue(new JsonPointer("/foo").Equals(new JsonPointer("/foo")));
    }
}

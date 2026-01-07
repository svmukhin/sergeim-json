// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class KeysAndValuesTests
{
    [TestMethod]
    public void Keys_ReturnsAllKeys()
    {
        var keys = new JsonObjectBuilder()
                .Add("a", 1)
                .Add("b", 2)
                .Add("c", 3)
                .Build().Keys.ToList();
        Assert.AreEqual(3, keys.Count);
        CollectionAssert.Contains(keys, "a");
        CollectionAssert.Contains(keys, "b");
        CollectionAssert.Contains(keys, "c");
    }

    [TestMethod]
    public void Values_ReturnsAllValues()
    {
        var values = new JsonObjectBuilder()
            .Add("a", 1)
            .Add("b", 2)
            .Build().Values.ToList();
        Assert.AreEqual(2, values.Count);
        Assert.IsTrue(values.Any(v => ((JsonNumber)v).IntValue() == 1));
        Assert.IsTrue(values.Any(v => ((JsonNumber)v).IntValue() == 2));
    }
}

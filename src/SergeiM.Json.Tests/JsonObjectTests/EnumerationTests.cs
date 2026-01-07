// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class EnumerationTests
{
    [TestMethod]
    public void GetEnumerator_IteratesAllKeyValuePairs()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", "X")
            .Add("y", "Y")
            .Build();
        var dict = new Dictionary<string, string>();
        foreach (var kvp in obj)
        {
            dict[kvp.Key] = ((JsonString)kvp.Value).Value;
        }
        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual("X", dict["x"]);
        Assert.AreEqual("Y", dict["y"]);
    }
}

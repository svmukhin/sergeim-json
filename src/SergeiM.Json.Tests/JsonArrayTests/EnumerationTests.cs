// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonArrayTests;

[TestClass]
public class EnumerationTests
{
    [TestMethod]
    public void GetEnumerator_IteratesAllElements()
    {
        var arr = Json.CreateArrayBuilder()
            .Add("a")
            .Add("b")
            .Add("c")
            .Build();
        var list = new List<string>();
        foreach (var value in arr)
        {
            list.Add(((JsonString)value).Value);
        }
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("a", list[0]);
        Assert.AreEqual("b", list[1]);
        Assert.AreEqual("c", list[2]);
    }

    [TestMethod]
    public void Count_ReturnsCorrectCount()
    {
        Assert.AreEqual(3, Json.CreateArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build().Count);
    }

    [TestMethod]
    public void EmptyArray_CountIsZero()
    {
        Assert.AreEqual(0, JsonArray.Empty.Count);
    }
}

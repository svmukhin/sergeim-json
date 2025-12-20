// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class RemoveOperationTests
{
    [TestMethod]
    public void Remove_Property_RemovesProperty()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Add("y", 20)
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Remove("/x")).Apply(obj);
        Assert.IsFalse(result.ContainsKey("x"));
        Assert.AreEqual(20, result.GetInt("y"));
    }

    [TestMethod]
    public void Remove_ArrayElement_RemovesElement()
    {
        var arr = Json.CreateArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build();
        var result = (JsonArray)new JsonPatch(JsonPatchOperation.Remove("/1")).Apply(arr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, ((JsonNumber)result[0]).IntValue());
        Assert.AreEqual(3, ((JsonNumber)result[1]).IntValue());
    }

    [TestMethod]
    public void Remove_NestedProperty_RemovesProperty()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("data", Json.CreateObjectBuilder()
                .Add("x", 1)
                .Add("y", 2))
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Remove("/data/x")).Apply(obj);
        var data = result.GetJsonObject("data")!;
        Assert.IsFalse(data.ContainsKey("x"));
        Assert.AreEqual(2, data.GetInt("y"));
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Remove_NonExistentProperty_ThrowsJsonException()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 1)
            .Build();
        var patch = new JsonPatch(JsonPatchOperation.Remove("/missing"));
        patch.Apply(obj);
    }
}

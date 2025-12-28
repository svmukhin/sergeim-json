// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class MultipleOperationsTests
{
    [TestMethod]
    public void Apply_MultipleOperations_AppliesInOrder()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Build();
        var patch = new JsonPatch(
            JsonPatchOperation.Add("/y", new JsonNumber(20)),
            JsonPatchOperation.Replace("/x", new JsonNumber(15)),
            JsonPatchOperation.Add("/z", new JsonNumber(30))
        );
        var result = (JsonObject)patch.Apply(obj);
        Assert.AreEqual(15, result.GetInt("x"));
        Assert.AreEqual(20, result.GetInt("y"));
        Assert.AreEqual(30, result.GetInt("z"));
    }

    [TestMethod]
    public void Apply_AddAndRemove_ModifiesCorrectly()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Add("y", 20)
            .Build();
        var patch = new JsonPatch(
            JsonPatchOperation.Remove("/x"),
            JsonPatchOperation.Add("/z", new JsonNumber(30))
        );
        var result = (JsonObject)patch.Apply(obj);
        Assert.IsFalse(result.ContainsKey("x"));
        Assert.AreEqual(20, result.GetInt("y"));
        Assert.AreEqual(30, result.GetInt("z"));
    }

    [TestMethod]
    public void Apply_MoveAndAdd_ModifiesCorrectly()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Build();
        var patch = new JsonPatch(
            JsonPatchOperation.Move("/x", "/y"),
            JsonPatchOperation.Add("/x", new JsonNumber(20))
        );
        var result = (JsonObject)patch.Apply(obj);
        Assert.AreEqual(20, result.GetInt("x"));
        Assert.AreEqual(10, result.GetInt("y"));
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Apply_TestFailsInMiddle_ThrowsException()
    {
        var obj = new JsonObjectBuilder()
            .Add("x", 10)
            .Build();
        var patch = new JsonPatch(
            JsonPatchOperation.Add("/y", new JsonNumber(20)),
            JsonPatchOperation.Test("/x", new JsonNumber(99)),
            JsonPatchOperation.Add("/z", new JsonNumber(30)));

        patch.Apply(obj);
    }
}

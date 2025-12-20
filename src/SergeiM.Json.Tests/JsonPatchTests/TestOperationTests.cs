// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class TestOperationTests
{
    [TestMethod]
    public void Test_MatchingValue_DoesNotThrow()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        var result = new JsonPatch(JsonPatchOperation.Test("/x", new JsonNumber(10))).Apply(obj);
        Assert.AreSame(obj, result);
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Test_NonMatchingValue_ThrowsJsonException()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        var patch = new JsonPatch(JsonPatchOperation.Test("/x", new JsonNumber(20)));
        patch.Apply(obj);
    }

    [TestMethod]
    public void Test_MatchingString_DoesNotThrow()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Build();
        var result = new JsonPatch(JsonPatchOperation.Test("/name", new JsonString("Alice"))).Apply(obj);
        Assert.AreSame(obj, result);
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Test_NonExistentProperty_ThrowsJsonException()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 1)
            .Build();
        var patch = new JsonPatch(JsonPatchOperation.Test("/missing", (JsonValue)new JsonNumber(10)));
        patch.Apply(obj);
    }

    [TestMethod]
    public void Test_MatchingNull_DoesNotThrow()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", JsonValue.Null)
            .Build();
        var result = new JsonPatch(JsonPatchOperation.Test("/x", JsonValue.Null)).Apply(obj);
        Assert.AreSame(obj, result);
    }
}

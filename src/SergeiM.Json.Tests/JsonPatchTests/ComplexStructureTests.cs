// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class ComplexStructureTests
{
    [TestMethod]
    public void Apply_ComplexNestedStructure_ModifiesCorrectly()
    {
        var obj = new JsonObjectBuilder()
            .Add("users", new JsonArrayBuilder()
                .Add(new JsonObjectBuilder()
                    .Add("name", "Alice")
                    .Add("age", 30))
                .Add(new JsonObjectBuilder()
                    .Add("name", "Bob")
                    .Add("age", 25)))
            .Build();
        var patch = new JsonPatch(
            JsonPatchOperation.Replace("/users/0/age", new JsonNumber(31)),
            JsonPatchOperation.Add("/users/1/email", new JsonString("bob@example.com"))
        );
        var result = (JsonObject)patch.Apply(obj);
        var users = result.GetJsonArray("users")!;
        Assert.AreEqual(31, users.GetJsonObject(0)!.GetInt("age"));
        Assert.AreEqual("bob@example.com", users.GetJsonObject(1)!.GetString("email"));
    }

    [TestMethod]
    public void Apply_ArrayWithinObject_ModifiesCorrectly()
    {
        var obj = new JsonObjectBuilder()
            .Add("data", new JsonObjectBuilder()
                .Add("items", new JsonArrayBuilder()
                    .Add(1)
                    .Add(2)
                    .Add(3)))
            .Build();
        var patch = new JsonPatch(
            JsonPatchOperation.Add("/data/items/-", new JsonNumber(4)),
            JsonPatchOperation.Remove("/data/items/0")
        );
        var items = ((JsonObject)patch.Apply(obj)).GetJsonObject("data")!.GetJsonArray("items")!;
        Assert.AreEqual(3, items.Count);
        Assert.AreEqual(2, ((JsonNumber)items[0]).IntValue());
        Assert.AreEqual(3, ((JsonNumber)items[1]).IntValue());
        Assert.AreEqual(4, ((JsonNumber)items[2]).IntValue());
    }
}

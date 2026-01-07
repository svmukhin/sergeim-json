// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class ComplexStructureTests
{
    [TestMethod]
    public void GetValue_DeepNesting_ReturnsValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("level1", new JsonObjectBuilder()
                .Add("level2", new JsonObjectBuilder()
                    .Add("level3", new JsonObjectBuilder()
                        .Add("value", 42))))
            .Build();
        var result = new JsonPointer("/level1/level2/level3/value").GetValue(obj);
        Assert.AreEqual(42, ((JsonNumber)result).IntValue());
    }

    [TestMethod]
    public void GetValue_ArrayOfObjects_ReturnsValue()
    {
        var arr = new JsonArrayBuilder()
            .Add(new JsonObjectBuilder()
                .Add("id", 1)
                .Add("name", "Alice"))
            .Add(new JsonObjectBuilder()
                .Add("id", 2)
                .Add("name", "Bob"))
            .Build();
        var result = new JsonPointer("/1/name").GetValue(arr);
        Assert.AreEqual("Bob", ((JsonString)result).Value);
    }

    [TestMethod]
    public void GetValue_ObjectWithArrays_ReturnsValue()
    {
        var obj = new JsonObjectBuilder()
            .Add("users", new JsonArrayBuilder()
                .Add(new JsonObjectBuilder()
                    .Add("name", "Charlie")
                    .Add("roles", new JsonArrayBuilder()
                        .Add("admin")
                        .Add("user"))))
            .Build();
        var result = new JsonPointer("/users/0/roles/1").GetValue(obj);
        Assert.AreEqual("user", ((JsonString)result).Value);
    }
}

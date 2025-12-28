// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class IndexerTests
{
    [TestMethod]
    public void Indexer_WithExistingKey_ReturnsValue()
    {
        var value = new JsonObjectBuilder()
            .Add("name", "Alice")
            .Build()["name"];
        Assert.IsNotNull(value);
        Assert.AreEqual("Alice", ((JsonString)value).Value);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void Indexer_WithNonExistingKey_ThrowsKeyNotFoundException()
    {
        var obj = JsonObject.Empty;
        _ = obj["missing"];
    }
}

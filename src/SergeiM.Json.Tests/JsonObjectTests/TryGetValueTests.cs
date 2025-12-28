// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class TryGetValueTests
{
    [TestMethod]
    public void TryGetValue_WithExistingKey_ReturnsTrueAndValue()
    {
        var result = new JsonObjectBuilder()
            .Add("count", 42)
            .Build().TryGetValue("count", out var value);
        Assert.IsTrue(result);
        Assert.IsNotNull(value);
        Assert.AreEqual(42, ((JsonNumber)value).IntValue());
    }

    [TestMethod]
    public void TryGetValue_WithNonExistingKey_ReturnsFalse()
    {
        var result = JsonObject.Empty.TryGetValue("missing", out var value);
        Assert.IsFalse(result);
        Assert.IsNull(value);
    }
}

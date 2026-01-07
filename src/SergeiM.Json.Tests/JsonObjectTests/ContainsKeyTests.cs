// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class ContainsKeyTests
{
    [TestMethod]
    public void ContainsKey_WithExistingKey_ReturnsTrue()
    {
        Assert.IsTrue(new JsonObjectBuilder()
            .Add("name", "Bob")
            .Build().ContainsKey("name"));
    }

    [TestMethod]
    public void ContainsKey_WithNonExistingKey_ReturnsFalse()
    {
        Assert.IsFalse(JsonObject.Empty.ContainsKey("missing"));
    }
}

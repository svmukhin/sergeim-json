// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class OptionsTests
{
    [TestMethod]
    public void Read_WithComments_AllowCommentsOption()
    {
        using var reader = JsonReader.Create(
            new StringReader("{\"value\":10/* comment */}"),
            new JsonReaderOptions { AllowComments = true });
        Assert.AreEqual(10, reader.ReadObject().GetInt("value"));
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Read_WithComments_DefaultOptions_ThrowsException()
    {
        using var reader = JsonReader.Create(new StringReader("{\"value\":10/* comment */}"));
        reader.ReadObject();
    }

    [TestMethod]
    public void Read_WithTrailingCommas_AllowTrailingCommasOption()
    {
        using var reader = JsonReader.Create(
            new StringReader("[1,2,3,]"),
            new JsonReaderOptions { AllowTrailingCommas = true });
        Assert.AreEqual(3, reader.ReadArray().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Read_WithTrailingCommas_DefaultOptions_ThrowsException()
    {
        using var reader = JsonReader.Create(new StringReader("[1,2,3,]"));
        reader.ReadArray();
    }
}

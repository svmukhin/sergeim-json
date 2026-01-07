// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class NumberFormattingTests
{
    [TestMethod]
    public void Write_Integer_WritesInteger()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonNumber(42));
        var json = writer.ToString();
        Assert.AreEqual("42", json);
    }

    [TestMethod]
    public void Write_Decimal_WritesDecimal()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonNumber(3.14m));
        var json = writer.ToString();
        Assert.IsTrue(json.StartsWith("3.14"));
    }

    [TestMethod]
    public void Write_NegativeNumber_WritesNegativeNumber()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonNumber(-42));
        var json = writer.ToString();
        Assert.AreEqual("-42", json);
    }

    [TestMethod]
    public void Write_LargeNumber_WritesLargeNumber()
    {
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(new JsonNumber(9999999999L));
        var json = writer.ToString();
        Assert.AreEqual("9999999999", json);
    }
}

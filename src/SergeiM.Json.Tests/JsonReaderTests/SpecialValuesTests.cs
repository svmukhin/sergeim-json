// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class SpecialValuesTests
{
    [TestMethod]
    public void Read_EmptyString_ReturnsEmptyJsonString()
    {
        using var reader = new JsonReader(new StringReader("\"\""));
        Assert.AreEqual("", ((JsonString)reader.Read()).Value);
    }

    [TestMethod]
    public void Read_EscapedCharacters_ParsesCorrectly()
    {
        using var reader = new JsonReader(new StringReader("\"Hello\\nWorld\\t!\""));
        Assert.AreEqual("Hello\nWorld\t!", ((JsonString)reader.Read()).Value);
    }

    [TestMethod]
    public void Read_UnicodeEscape_ParsesCorrectly()
    {
        using var reader = new JsonReader(new StringReader("\"\\u0048\\u0065\\u006C\\u006C\\u006F\""));
        Assert.AreEqual("Hello", ((JsonString)reader.Read()).Value);
    }

    [TestMethod]
    public void Read_LargeNumber_ParsesCorrectly()
    {
        using var reader = new JsonReader(new StringReader("9999999999"));
        Assert.AreEqual(9999999999L, ((JsonNumber)reader.Read()).LongValue());
    }

    [TestMethod]
    public void Read_DecimalNumber_ParsesCorrectly()
    {
        using var reader = new JsonReader(new StringReader("3.14159"));
        Assert.AreEqual(3.14159, ((JsonNumber)reader.Read()).DoubleValue(), 0.00001);
    }

    [TestMethod]
    public void Read_NegativeNumber_ParsesCorrectly()
    {
        using var reader = new JsonReader(new StringReader("-42"));
        Assert.AreEqual(-42, ((JsonNumber)reader.Read()).IntValue());
    }

    [TestMethod]
    public void Read_ScientificNotation_ParsesCorrectly()
    {
        using var reader = new JsonReader(new StringReader("1.23e10"));
        Assert.AreEqual(1.23e10, ((JsonNumber)reader.Read()).DoubleValue(), 0.01);
    }
}

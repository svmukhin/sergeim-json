// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonValueTests;

[TestClass]
public class JsonStringTests
{
    [TestMethod]
    public void String_ShouldHaveCorrectValueType()
    {
        var str = new JsonString("test");
        Assert.AreEqual(JsonValueType.String, str.ValueType);
    }

    [TestMethod]
    public void String_ShouldStoreValue()
    {
        var str = new JsonString("hello");
        Assert.AreEqual("hello", str.Value);
    }

    [TestMethod]
    public void String_ToString_ShouldReturnQuotedAndEscaped()
    {
        var str = new JsonString("hello");
        Assert.AreEqual("\"hello\"", str.ToString());
    }

    [TestMethod]
    public void String_ToString_ShouldEscapeSpecialCharacters()
    {
        var str = new JsonString("line1\nline2");
        var json = str.ToString();
        Assert.IsTrue(json.Contains("\\n"));
    }

    [TestMethod]
    public void String_ImplicitConversion_ShouldWork()
    {
        var str = new JsonString("test");
        string value = str;
        Assert.AreEqual("test", value);
    }

    [TestMethod]
    public void String_Equals_ShouldWorkCorrectly()
    {
        var str1 = new JsonString("test");
        var str2 = new JsonString("test");
        var str3 = new JsonString("other");
        Assert.IsTrue(str1.Equals(str2));
        Assert.IsFalse(str1.Equals(str3));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void String_Constructor_ShouldThrowOnNull()
    {
        _ = new JsonString(null!);
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class ReadObjectTests
{
    [TestMethod]
    public void ReadObject_ValidObject_ReturnsJsonObject()
    {
        using var reader = JsonReader.Create(new StringReader("{\"x\":10,\"y\":20}"));
        var obj = reader.ReadObject();
        Assert.IsNotNull(obj);
        Assert.AreEqual(10, obj.GetInt("x"));
        Assert.AreEqual(20, obj.GetInt("y"));
    }

    [TestMethod]
    public void ReadObject_EmptyObject_ReturnsEmptyJsonObject()
    {
        using var reader = JsonReader.Create(new StringReader("{}"));
        var obj = reader.ReadObject();
        Assert.IsNotNull(obj);
        Assert.AreEqual(0, obj.Count);
    }

    [TestMethod]
    public void ReadObject_NestedObject_ReturnsNestedStructure()
    {
        using var reader = JsonReader.Create(new StringReader("{\"outer\":{\"inner\":\"value\"}}"));
        var inner = reader.ReadObject().GetJsonObject("outer");
        Assert.IsNotNull(inner);
        Assert.AreEqual("value", inner!.GetString("inner"));
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void ReadObject_NotAnObject_ThrowsJsonException()
    {
        using var reader = JsonReader.Create(new StringReader("[1,2,3]"));
        reader.ReadObject();
    }
}

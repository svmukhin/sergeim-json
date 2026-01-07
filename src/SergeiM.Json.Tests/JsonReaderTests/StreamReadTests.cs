// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using System.Text;
using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class StreamReadTests
{
    [TestMethod]
    public void Read_FromStream_ReturnsJsonValue()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("{\"test\":123}"));
        using var reader = JsonReader.Create(stream);
        var value = reader.Read();
        Assert.IsNotNull(value);
        var obj = value.AsJsonObject();
        Assert.AreEqual(123, obj.GetInt("test"));
    }

    [TestMethod]
    public void ReadObject_FromStream_ReturnsJsonObject()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("{\"value\":456}"));
        using var reader = JsonReader.Create(stream);
        Assert.AreEqual(456, reader.ReadObject().GetInt("value"));
    }

    [TestMethod]
    public void ReadArray_FromStream_ReturnsJsonArray()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("[7,8,9]"));
        using var reader = JsonReader.Create(stream);
        var arr = reader.ReadArray();
        Assert.AreEqual(3, arr.Count);
        Assert.AreEqual(7, arr.GetInt(0));
    }
}

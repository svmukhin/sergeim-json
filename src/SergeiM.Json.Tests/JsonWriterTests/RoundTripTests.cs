// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class RoundTripTests
{
    [TestMethod]
    public void RoundTrip_Object_PreservesData()
    {
        var original = Json.CreateObjectBuilder()
            .Add("string", "value")
            .Add("number", 42)
            .Add("boolean", true)
            .AddNull("null")
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(original);
        var json = writer.ToString();
        using var reader = JsonReader.Create(new StringReader(json));
        var parsed = reader.ReadObject();
        Assert.AreEqual("value", parsed.GetString("string"));
        Assert.AreEqual(42, parsed.GetInt("number"));
        Assert.IsTrue(parsed.GetBoolean("boolean"));
        Assert.IsTrue(parsed.IsNull("null"));
    }

    [TestMethod]
    public void RoundTrip_Array_PreservesData()
    {
        var original = Json.CreateArrayBuilder()
            .Add("text")
            .Add(123)
            .Add(false)
            .AddNull()
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(original);
        var json = writer.ToString();
        using var reader = JsonReader.Create(new StringReader(json));
        var parsed = reader.ReadArray();
        Assert.AreEqual("text", parsed.GetString(0));
        Assert.AreEqual(123, parsed.GetInt(1));
        Assert.IsFalse(parsed.GetBoolean(2));
        Assert.IsTrue(parsed.IsNull(3));
    }

    [TestMethod]
    public void RoundTrip_ComplexStructure_PreservesData()
    {
        var original = Json.CreateObjectBuilder()
            .Add("nested", Json.CreateObjectBuilder()
                .Add("array", Json.CreateArrayBuilder()
                    .Add(1)
                    .Add(2)
                    .Add(3)))
            .Build();
        using var writer = new StringWriter();
        using var jsonWriter = JsonWriter.Create(writer);
        jsonWriter.Write(original);
        var json = writer.ToString();
        using var reader = JsonReader.Create(new StringReader(json));
        var parsed = reader.ReadObject();
        var nested = parsed.GetJsonObject("nested");
        var array = nested!.GetJsonArray("array");
        Assert.AreEqual(3, array!.Count);
        Assert.AreEqual(1, array.GetInt(0));
        Assert.AreEqual(2, array.GetInt(1));
        Assert.AreEqual(3, array.GetInt(2));
    }
}

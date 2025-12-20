// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class ComplexStructureTests
{
    [TestMethod]
    public void Read_ComplexNestedStructure_ParsesCorrectly()
    {
        var json = @"{
                ""person"": {
                    ""name"": ""John"",
                    ""age"": 30,
                    ""address"": {
                        ""street"": ""Main St"",
                        ""city"": ""New York""
                    },
                    ""phones"": [""555-1234"", ""555-5678""]
                }
            }";
        using var reader = JsonReader.Create(new StringReader(json));
        var person = reader.ReadObject().GetJsonObject("person");
        Assert.IsNotNull(person);
        Assert.AreEqual("John", person!.GetString("name"));
        Assert.AreEqual(30, person.GetInt("age"));
        var address = person.GetJsonObject("address");
        Assert.IsNotNull(address);
        Assert.AreEqual("Main St", address!.GetString("street"));
        Assert.AreEqual("New York", address.GetString("city"));
        var phones = person.GetJsonArray("phones");
        Assert.IsNotNull(phones);
        Assert.AreEqual(2, phones!.Count);
        Assert.AreEqual("555-1234", phones.GetString(0));
        Assert.AreEqual("555-5678", phones.GetString(1));
    }

    [TestMethod]
    public void Read_ArrayOfObjects_ParsesCorrectly()
    {
        var json = @"[
                {""id"": 1, ""name"": ""Alice""},
                {""id"": 2, ""name"": ""Bob""}
            ]";
        using var reader = JsonReader.Create(new StringReader(json));
        var arr = reader.ReadArray();
        Assert.AreEqual(2, arr.Count);
        var first = arr.GetJsonObject(0);
        Assert.AreEqual(1, first.GetInt("id"));
        Assert.AreEqual("Alice", first.GetString("name"));
        var second = arr.GetJsonObject(1);
        Assert.AreEqual(2, second.GetInt("id"));
        Assert.AreEqual("Bob", second.GetString("name"));
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class ErrorHandlingTests
{
    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Read_InvalidJson_ThrowsJsonException()
    {
        using var reader = new JsonReader(new StringReader("{invalid}"));
        reader.Read();
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Read_UnterminatedString_ThrowsJsonException()
    {
        using var reader = new JsonReader(new StringReader("\"unterminated"));
        reader.Read();
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Read_UnterminatedArray_ThrowsJsonException()
    {
        using var reader = new JsonReader(new StringReader("[1,2,3"));
        reader.Read();
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Read_UnterminatedObject_ThrowsJsonException()
    {
        using var reader = new JsonReader(new StringReader("{\"key\":\"value\""));
        reader.Read();
    }
}

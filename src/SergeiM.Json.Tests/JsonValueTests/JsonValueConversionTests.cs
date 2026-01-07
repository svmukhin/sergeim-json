// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

namespace SergeiM.Json.Tests.JsonValueTests;

[TestClass]
public class JsonValueConversionTests
{
    [TestMethod]
    public void AsJsonObject_ShouldWork_ForJsonObject()
    {
        var obj = JsonValue.EmptyJsonObject;
        var result = obj.AsJsonObject();
        Assert.AreSame(obj, result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCastException))]
    public void AsJsonObject_ShouldThrow_ForNonObject()
    {
        JsonValue.Null.AsJsonObject();
    }

    [TestMethod]
    public void AsJsonArray_ShouldWork_ForJsonArray()
    {
        var arr = JsonValue.EmptyJsonArray;
        var result = arr.AsJsonArray();
        Assert.AreSame(arr, result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCastException))]
    public void AsJsonArray_ShouldThrow_ForNonArray()
    {
        JsonValue.Null.AsJsonArray();
    }
}

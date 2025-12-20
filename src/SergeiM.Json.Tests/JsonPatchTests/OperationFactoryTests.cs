// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class OperationFactoryTests
{
    [TestMethod]
    public void Add_WithStringPath_CreatesOperation()
    {
        var operation = JsonPatchOperation.Add("/x", new JsonNumber(10));
        Assert.AreEqual(JsonPatchOperationType.Add, operation.OperationType);
        Assert.AreEqual("/x", operation.Path.Pointer);
        Assert.AreEqual(10, ((JsonNumber)operation.Value!).IntValue());
    }

    [TestMethod]
    public void Remove_WithStringPath_CreatesOperation()
    {
        var operation = JsonPatchOperation.Remove("/x");
        Assert.AreEqual(JsonPatchOperationType.Remove, operation.OperationType);
        Assert.AreEqual("/x", operation.Path.Pointer);
    }

    [TestMethod]
    public void Replace_WithStringPath_CreatesOperation()
    {
        var operation = JsonPatchOperation.Replace("/x", new JsonNumber(20));
        Assert.AreEqual(JsonPatchOperationType.Replace, operation.OperationType);
        Assert.AreEqual("/x", operation.Path.Pointer);
        Assert.AreEqual(20, ((JsonNumber)operation.Value!).IntValue());
    }

    [TestMethod]
    public void Move_WithStringPaths_CreatesOperation()
    {
        var operation = JsonPatchOperation.Move("/x", "/y");
        Assert.AreEqual(JsonPatchOperationType.Move, operation.OperationType);
        Assert.AreEqual("/x", operation.From!.Pointer);
        Assert.AreEqual("/y", operation.Path.Pointer);
    }

    [TestMethod]
    public void Copy_WithStringPaths_CreatesOperation()
    {
        var operation = JsonPatchOperation.Copy("/x", "/y");
        Assert.AreEqual(JsonPatchOperationType.Copy, operation.OperationType);
        Assert.AreEqual("/x", operation.From!.Pointer);
        Assert.AreEqual("/y", operation.Path.Pointer);
    }

    [TestMethod]
    public void Test_WithStringPath_CreatesOperation()
    {
        var operation = JsonPatchOperation.Test("/x", new JsonNumber(10));
        Assert.AreEqual(JsonPatchOperationType.Test, operation.OperationType);
        Assert.AreEqual("/x", operation.Path.Pointer);
        Assert.AreEqual(10, ((JsonNumber)operation.Value!).IntValue());
    }
}

// SPDX-FileCopyrightText: Copyright (c) [2025] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class ImmutabilityTests
{
    [TestMethod]
    public void Apply_DoesNotModifyOriginal()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        var result = new JsonPatch(JsonPatchOperation.Add("/y", new JsonNumber(20))).Apply(obj);
        Assert.IsFalse(ReferenceEquals(obj, result));
        var originalObj = obj;
        Assert.IsFalse(originalObj.ContainsKey("y"));
    }

    [TestMethod]
    public void Apply_MultiplePatches_DoesNotModifyOriginal()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        var patch1 = new JsonPatch(JsonPatchOperation.Add("/y", new JsonNumber(20)));
        var patch2 = new JsonPatch(JsonPatchOperation.Add("/z", new JsonNumber(30)));
        var result1 = patch1.Apply(obj);
        var result2 = patch2.Apply(result1);
        var originalObj = obj;
        var result1Obj = (JsonObject)result1;
        var result2Obj = (JsonObject)result2;
        Assert.IsFalse(originalObj.ContainsKey("y"));
        Assert.IsFalse(originalObj.ContainsKey("z"));
        Assert.IsTrue(result1Obj.ContainsKey("y"));
        Assert.IsFalse(result1Obj.ContainsKey("z"));
        Assert.IsTrue(result2Obj.ContainsKey("y"));
        Assert.IsTrue(result2Obj.ContainsKey("z"));
    }
}

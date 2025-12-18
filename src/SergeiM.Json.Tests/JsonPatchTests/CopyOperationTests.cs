using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class CopyOperationTests
{
    [TestMethod]
    public void Copy_Property_CopiesValue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Copy("/x", "/y")).Apply(obj);
        Assert.AreEqual(10, result.GetInt("x"));
        Assert.AreEqual(10, result.GetInt("y"));
    }

    [TestMethod]
    public void Copy_ArrayElement_CopiesElement()
    {
        var arr = Json.CreateArrayBuilder()
            .Add("a")
            .Add("b")
            .Build();
        var result = (JsonArray)new JsonPatch(JsonPatchOperation.Copy("/0", "/-")).Apply(arr);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("a", ((JsonString)result[0]).Value);
        Assert.AreEqual("b", ((JsonString)result[1]).Value);
        Assert.AreEqual("a", ((JsonString)result[2]).Value);
    }

    [TestMethod]
    public void Copy_NestedValue_CopiesValue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("data", Json.CreateObjectBuilder()
                .Add("value", 42))
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Copy("/data/value", "/copied")).Apply(obj);
        Assert.AreEqual(42, result.GetInt("copied"));
        var data = result.GetJsonObject("data")!;
        Assert.AreEqual(42, data.GetInt("value"));
    }
}

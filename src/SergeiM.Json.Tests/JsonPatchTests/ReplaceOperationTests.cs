using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPatchTests;

[TestClass]
public class ReplaceOperationTests
{
    [TestMethod]
    public void Replace_Property_ReplacesValue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 10)
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Replace("/x", new JsonNumber(20))).Apply(obj);
        Assert.AreEqual(20, result.GetInt("x"));
    }

    [TestMethod]
    public void Replace_ArrayElement_ReplacesElement()
    {
        var arr = Json.CreateArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build();
        var result = (JsonArray)new JsonPatch(JsonPatchOperation.Replace("/1", new JsonNumber(99))).Apply(arr);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(99, ((JsonNumber)result[1]).IntValue());
    }

    [TestMethod]
    public void Replace_NestedProperty_ReplacesValue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("data", Json.CreateObjectBuilder()
                .Add("value", 10))
            .Build();
        var result = (JsonObject)new JsonPatch(JsonPatchOperation.Replace("/data/value", new JsonNumber(20))).Apply(obj);
        var data = result.GetJsonObject("data")!;
        Assert.AreEqual(20, data.GetInt("value"));
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void Replace_NonExistentProperty_ThrowsJsonException()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 1)
            .Build();
        var patch = new JsonPatch(JsonPatchOperation.Replace("/missing", new JsonNumber(10)));
        patch.Apply(obj);
    }
}

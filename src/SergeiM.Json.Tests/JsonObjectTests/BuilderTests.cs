namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class BuilderTests
{
    [TestMethod]
    public void EmptyObject_HasZeroCount()
    {
        Assert.AreEqual(0, JsonObject.Empty.Count);
        Assert.AreEqual(JsonValueType.Object, JsonObject.Empty.ValueType);
    }

    [TestMethod]
    public void Builder_WithValues_ContainsValues()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("name", "John")
            .Add("age", 30)
            .Add("active", true)
            .Build();
        Assert.AreEqual(3, obj.Count);
        Assert.AreEqual("John", ((JsonString)obj["name"]).Value);
        Assert.AreEqual(30, ((JsonNumber)obj["age"]).IntValue());
        Assert.AreEqual(true, ((JsonBoolean)obj["active"]).Value);
    }

    [TestMethod]
    public void Builder_Immutable_DoesNotAffectOriginal()
    {
        var builder = Json.CreateObjectBuilder().Add("x", 1);
        var obj1 = builder.Build();
        builder.Add("y", 2);
        var obj2 = builder.Build();
        Assert.AreEqual(1, obj1.Count);
        Assert.AreEqual(2, obj2.Count);
    }
}

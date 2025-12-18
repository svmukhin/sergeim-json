using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class EscapingTests
{
    [TestMethod]
    public void GetValue_PropertyWithSlash_HandlesEscaping()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("a/b", "value")
            .Build();
        Assert.AreEqual("value", ((JsonString)new JsonPointer("/a~1b").GetValue(obj)).Value);
    }

    [TestMethod]
    public void GetValue_PropertyWithTilde_HandlesEscaping()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("a~b", "value")
            .Build();
        Assert.AreEqual("value", ((JsonString)new JsonPointer("/a~0b").GetValue(obj)).Value);
    }

    [TestMethod]
    public void GetValue_PropertyWithBothEscapes_HandlesEscaping()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("a~/b", "value")
            .Build();
        Assert.AreEqual("value", ((JsonString)new JsonPointer("/a~0~1b").GetValue(obj)).Value);
    }
}

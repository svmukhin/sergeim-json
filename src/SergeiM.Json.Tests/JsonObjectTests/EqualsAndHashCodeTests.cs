namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class EqualsAndHashCodeTests
{
    [TestMethod]
    public void Equals_WithSameContent_ReturnsTrue()
    {
        var obj1 = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Add("age", 25)
            .Build();
        var obj2 = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Add("age", 25)
            .Build();
        Assert.IsTrue(obj1.Equals(obj2));
        Assert.AreEqual(obj1.GetHashCode(), obj2.GetHashCode());
    }

    [TestMethod]
    public void Equals_WithDifferentContent_ReturnsFalse()
    {
        var obj1 = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Build();
        var obj2 = Json.CreateObjectBuilder()
            .Add("name", "Bob")
            .Build();
        Assert.IsFalse(obj1.Equals(obj2));
    }

    [TestMethod]
    public void Equals_WithDifferentKeys_ReturnsFalse()
    {
        var obj1 = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Build();
        var obj2 = Json.CreateObjectBuilder()
            .Add("age", 25)
            .Build();
        Assert.IsFalse(obj1.Equals(obj2));
    }

    [TestMethod]
    public void Equals_WithNull_ReturnsFalse()
    {
        Assert.IsFalse(JsonObject.Empty.Equals(null));
    }

    [TestMethod]
    public void Equals_WithSameInstance_ReturnsTrue()
    {
        var obj = Json.CreateObjectBuilder()
            .Add("x", 1)
            .Build();
        Assert.IsTrue(obj.Equals(obj));
    }
}

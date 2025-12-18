namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class ContainsKeyTests
{
    [TestMethod]
    public void ContainsKey_WithExistingKey_ReturnsTrue()
    {
        Assert.IsTrue(Json.CreateObjectBuilder()
            .Add("name", "Bob")
            .Build().ContainsKey("name"));
    }

    [TestMethod]
    public void ContainsKey_WithNonExistingKey_ReturnsFalse()
    {
        Assert.IsFalse(JsonObject.Empty.ContainsKey("missing"));
    }
}

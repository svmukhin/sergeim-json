namespace SergeiM.Json.Tests.JsonObjectTests;

[TestClass]
public class IndexerTests
{
    [TestMethod]
    public void Indexer_WithExistingKey_ReturnsValue()
    {
        var value = Json.CreateObjectBuilder()
            .Add("name", "Alice")
            .Build()["name"];
        Assert.IsNotNull(value);
        Assert.AreEqual("Alice", ((JsonString)value).Value);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void Indexer_WithNonExistingKey_ThrowsKeyNotFoundException()
    {
        var obj = JsonObject.Empty;
        _ = obj["missing"];
    }
}

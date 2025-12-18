namespace SergeiM.Json.Tests.JsonArrayTests;

[TestClass]
public class IndexerTests
{
    [TestMethod]
    public void Indexer_WithValidIndex_ReturnsValue()
    {
        var value = Json.CreateArrayBuilder()
            .Add("test")
            .Build()[0];
        Assert.IsNotNull(value);
        Assert.AreEqual("test", ((JsonString)value).Value);
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void Indexer_WithNegativeIndex_ThrowsIndexOutOfRangeException()
    {
        var arr = Json.CreateArrayBuilder().Add(1).Build();
        _ = arr[-1];
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void Indexer_WithIndexOutOfRange_ThrowsIndexOutOfRangeException()
    {
        var arr = JsonArray.Empty;
        _ = arr[0];
    }
}

using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonReaderTests;

[TestClass]
public class ReadArrayTests
{
    [TestMethod]
    public void ReadArray_ValidArray_ReturnsJsonArray()
    {
        using var reader = JsonReader.Create(new StringReader("[10,20,30]"));
        var arr = reader.ReadArray();
        Assert.IsNotNull(arr);
        Assert.AreEqual(3, arr.Count);
        Assert.AreEqual(10, arr.GetInt(0));
        Assert.AreEqual(20, arr.GetInt(1));
        Assert.AreEqual(30, arr.GetInt(2));
    }

    [TestMethod]
    public void ReadArray_EmptyArray_ReturnsEmptyJsonArray()
    {
        using var reader = JsonReader.Create(new StringReader("[]"));
        var arr = reader.ReadArray();
        Assert.IsNotNull(arr);
        Assert.AreEqual(0, arr.Count);
    }

    [TestMethod]
    public void ReadArray_NestedArray_ReturnsNestedStructure()
    {
        using var reader = JsonReader.Create(new StringReader("[[1,2],[3,4]]"));
        var arr = reader.ReadArray();
        Assert.AreEqual(2, arr.Count);
        var first = arr.GetJsonArray(0);
        Assert.AreEqual(2, first.Count);
        Assert.AreEqual(1, first.GetInt(0));
    }

    [TestMethod]
    [ExpectedException(typeof(JsonException))]
    public void ReadArray_NotAnArray_ThrowsJsonException()
    {
        using var reader = JsonReader.Create(new StringReader("{\"key\":\"value\"}"));
        reader.ReadArray();
    }
}

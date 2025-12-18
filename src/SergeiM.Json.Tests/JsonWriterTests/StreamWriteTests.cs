using System.Text;
using SergeiM.Json.IO;

namespace SergeiM.Json.Tests.JsonWriterTests;

[TestClass]
public class StreamWriteTests
{
    [TestMethod]
    public void Write_ToStream_WritesCorrectBytes()
    {
        using var stream = new MemoryStream();
        using var jsonWriter = JsonWriter.Create(stream);
        jsonWriter.Write(Json.CreateObjectBuilder()
            .Add("test", 123)
            .Build());
        jsonWriter.Flush();
        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("{\"test\":123}", json);
    }

    [TestMethod]
    public void WriteObject_ToStream_WritesCorrectBytes()
    {
        using var stream = new MemoryStream();
        using var jsonWriter = JsonWriter.Create(stream);
        jsonWriter.WriteObject(Json.CreateObjectBuilder()
            .Add("value", 456)
            .Build());
        jsonWriter.Flush();
        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("{\"value\":456}", json);
    }

    [TestMethod]
    public void WriteArray_ToStream_WritesCorrectBytes()
    {
        using var stream = new MemoryStream();
        using var jsonWriter = JsonWriter.Create(stream);
        jsonWriter.WriteArray(Json.CreateArrayBuilder()
            .Add(7)
            .Add(8)
            .Add(9)
            .Build());
        jsonWriter.Flush();
        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual("[7,8,9]", json);
    }
}

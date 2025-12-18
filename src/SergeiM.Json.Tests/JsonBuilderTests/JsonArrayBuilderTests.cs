namespace SergeiM.Json.Tests;

[TestClass]
public class JsonArrayBuilderTests
{
    [TestMethod]
    public void Build_EmptyBuilder_CreatesEmptyArray()
    {
        var arr = Json.CreateArrayBuilder().Build();
        Assert.AreEqual(0, arr.Count);
        Assert.AreEqual(JsonValueType.Array, arr.ValueType);
    }

    [TestMethod]
    public void Add_WithString_AddsStringValue()
    {
        Assert.AreEqual("hello", Json.CreateArrayBuilder()
            .Add("hello")
            .Build().GetString(0));
    }

    [TestMethod]
    public void Add_WithInt_AddsNumberValue()
    {
        Assert.AreEqual(42, Json.CreateArrayBuilder()
            .Add(42)
            .Build().GetInt(0));
    }

    [TestMethod]
    public void Add_WithLong_AddsNumberValue()
    {
        Assert.AreEqual(9999999999L, Json.CreateArrayBuilder()
            .Add(9999999999L)
            .Build().GetLong(0));
    }

    [TestMethod]
    public void Add_WithDouble_AddsNumberValue()
    {
        Assert.AreEqual(2.71828, Json.CreateArrayBuilder()
            .Add(2.71828)
            .Build().GetDouble(0), 0.00001);
    }

    [TestMethod]
    public void Add_WithDecimal_AddsNumberValue()
    {
        Assert.AreEqual(9.99m, ((JsonNumber)Json.CreateArrayBuilder()
            .Add(9.99m)
            .Build()[0]).DecimalValue());
    }

    [TestMethod]
    public void Add_WithBoolean_AddsBooleanValue()
    {
        Assert.IsFalse(Json.CreateArrayBuilder()
            .Add(false)
            .Build().GetBoolean(0));
    }

    [TestMethod]
    public void AddNull_AddsNullValue()
    {
        Assert.IsTrue(Json.CreateArrayBuilder()
            .AddNull()
            .Build().IsNull(0));
    }

    [TestMethod]
    public void Add_WithJsonObject_AddsObjectValue()
    {
        var result = Json.CreateArrayBuilder()
            .Add(Json.CreateObjectBuilder().Add("key", "value").Build())
            .Build().GetJsonObject(0);
        Assert.AreEqual("value", result.GetString("key"));
    }

    [TestMethod]
    public void Add_WithJsonArray_AddsArrayValue()
    {
        var result = Json.CreateArrayBuilder()
            .Add(Json.CreateArrayBuilder().Add(1).Build())
            .Build().GetJsonArray(0);
        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    public void Add_WithJsonObjectBuilder_AddsObjectValue()
    {
        var result = Json.CreateArrayBuilder()
            .Add(Json.CreateObjectBuilder().Add("x", 5))
            .Build().GetJsonObject(0);
        Assert.AreEqual(5, result.GetInt("x"));
    }

    [TestMethod]
    public void Add_WithJsonArrayBuilder_AddsArrayValue()
    {
        var result = Json.CreateArrayBuilder()
            .Add(Json.CreateArrayBuilder().Add(7).Add(8))
            .Build().GetJsonArray(0);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(7, result.GetInt(0));
    }

    [TestMethod]
    public void Add_MultipleElements_AllAdded()
    {
        var arr = Json.CreateArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build();
        Assert.AreEqual(3, arr.Count);
        Assert.AreEqual(1, arr.GetInt(0));
        Assert.AreEqual(2, arr.GetInt(1));
        Assert.AreEqual(3, arr.GetInt(2));
    }


    [TestMethod]
    public void RemoveAt_ExistingIndex_RemovesElement()
    {
        var arr = Json.CreateArrayBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .RemoveAt(1)
            .Build();
        Assert.AreEqual(2, arr.Count);
        Assert.AreEqual(1, arr.GetInt(0));
        Assert.AreEqual(3, arr.GetInt(1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void RemoveAt_InvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        Json.CreateArrayBuilder()
            .Add(1)
            .RemoveAt(5);
    }

    [TestMethod]
    public void Build_MultipleTimes_CreatesIndependentArrays()
    {
        var builder = Json.CreateArrayBuilder().Add(1);
        var arr1 = builder.Build();
        builder.Add(2);
        var arr2 = builder.Build();
        Assert.AreEqual(1, arr1.Count);
        Assert.AreEqual(2, arr2.Count);
    }

    [TestMethod]
    public void CreateArrayBuilder_WithExistingArray_CopiesElements()
    {
        var original = Json.CreateArrayBuilder()
            .Add(1)
            .Add(2)
            .Build();
        var modified = Json.CreateArrayBuilder(original)
            .Add(3)
            .Build();
        Assert.AreEqual(2, original.Count);
        Assert.AreEqual(3, modified.Count);
        Assert.AreEqual(1, modified.GetInt(0));
        Assert.AreEqual(2, modified.GetInt(1));
        Assert.AreEqual(3, modified.GetInt(2));
    }
}

namespace SergeiM.Json.Tests.JsonValueTests;

[TestClass]
public class JsonNumberTests
{
    [TestMethod]
    public void Number_ShouldHaveCorrectValueType()
    {
        var num = new JsonNumber(42);
        Assert.AreEqual(JsonValueType.Number, num.ValueType);
    }

    [TestMethod]
    public void Number_IntConstructor_ShouldWork()
    {
        var num = new JsonNumber(42);
        Assert.AreEqual(42, num.IntValue());
    }

    [TestMethod]
    public void Number_LongConstructor_ShouldWork()
    {
        var num = new JsonNumber(123456789012L);
        Assert.AreEqual(123456789012L, num.LongValue());
    }

    [TestMethod]
    public void Number_DoubleConstructor_ShouldWork()
    {
        var num = new JsonNumber(3.14);
        Assert.AreEqual(3.14, num.DoubleValue(), 0.0001);
    }

    [TestMethod]
    public void Number_DecimalConstructor_ShouldWork()
    {
        var num = new JsonNumber(12.34m);
        Assert.AreEqual(12.34m, num.DecimalValue());
    }

    [TestMethod]
    public void Number_IsIntegral_ShouldReturnTrue_ForIntegers()
    {
        var num = new JsonNumber(42);
        Assert.IsTrue(num.IsIntegral());
    }

    [TestMethod]
    public void Number_IsIntegral_ShouldReturnFalse_ForDecimals()
    {
        var num = new JsonNumber(3.14);
        Assert.IsFalse(num.IsIntegral());
    }

    [TestMethod]
    public void Number_ToString_ShouldReturnNumberString()
    {
        var num = new JsonNumber(42);
        Assert.AreEqual("42", num.ToString());
    }

    [TestMethod]
    public void Number_ToString_ShouldUseInvariantCulture()
    {
        var num = new JsonNumber(3.14);
        var str = num.ToString();
        Assert.IsTrue(str.Contains("."));
        Assert.IsFalse(str.Contains(","));
    }

    [TestMethod]
    public void Number_ImplicitConversions_ShouldWork()
    {
        var num = new JsonNumber(42);
        int intValue = num;
        long longValue = num;
        double doubleValue = num;
        decimal decimalValue = num;
        Assert.AreEqual(42, intValue);
        Assert.AreEqual(42L, longValue);
        Assert.AreEqual(42.0, doubleValue);
        Assert.AreEqual(42m, decimalValue);
    }

    [TestMethod]
    public void Number_Equals_ShouldWorkCorrectly()
    {
        var num1 = new JsonNumber(42);
        var num2 = new JsonNumber(42);
        var num3 = new JsonNumber(43);
        Assert.IsTrue(num1.Equals(num2));
        Assert.IsFalse(num1.Equals(num3));
    }

    [TestMethod]
    public void Number_BigIntegerValue_ShouldWork()
    {
        var num = new JsonNumber(123456789);
        var bigInt = num.BigIntegerValue();
        Assert.AreEqual(new System.Numerics.BigInteger(123456789), bigInt);
    }
}

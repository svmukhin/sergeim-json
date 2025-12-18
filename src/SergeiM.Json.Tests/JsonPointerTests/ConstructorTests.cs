using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class ConstructorTests
{
    [TestMethod]
    public void Constructor_EmptyString_CreatesEmptyPointer()
    {
        var pointer = new JsonPointer("");
        Assert.AreEqual("", pointer.Pointer);
        Assert.AreEqual(0, pointer.Tokens.Count);
    }

    [TestMethod]
    public void Constructor_ValidPointer_CreatesPointer()
    {
        var pointer = new JsonPointer("/foo/bar");
        Assert.AreEqual("/foo/bar", pointer.Pointer);
        Assert.AreEqual(2, pointer.Tokens.Count);
        Assert.AreEqual("foo", pointer.Tokens[0]);
        Assert.AreEqual("bar", pointer.Tokens[1]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_InvalidPointer_ThrowsArgumentException()
    {
        _ = new JsonPointer("invalid");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_NullPointer_ThrowsArgumentNullException()
    {
        _ = new JsonPointer(null!);
    }
}

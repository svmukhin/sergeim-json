using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class AppendTests
{
    [TestMethod]
    public void Append_ToEmptyPointer_CreatesNewPointer()
    {
        var result = JsonPointer.Empty.Append("foo");
        Assert.AreEqual("/foo", result.Pointer);
        Assert.AreEqual(1, result.Tokens.Count);
        Assert.AreEqual("foo", result.Tokens[0]);
    }

    [TestMethod]
    public void Append_ToExistingPointer_ExtendsPointer()
    {
        var result = new JsonPointer("/foo").Append("bar");
        Assert.AreEqual("/foo/bar", result.Pointer);
        Assert.AreEqual(2, result.Tokens.Count);
        Assert.AreEqual("foo", result.Tokens[0]);
        Assert.AreEqual("bar", result.Tokens[1]);
    }

    [TestMethod]
    public void Append_TokenWithSlash_EscapesSlash()
    {
        var result = JsonPointer.Empty.Append("a/b");
        Assert.AreEqual("/a~1b", result.Pointer);
    }

    [TestMethod]
    public void Append_TokenWithTilde_EscapesTilde()
    {
        var result = JsonPointer.Empty.Append("a~b");
        Assert.AreEqual("/a~0b", result.Pointer);
    }

    [TestMethod]
    public void Append_MultipleTokens_CreatesCorrectPointer()
    {
        var result = JsonPointer.Empty.Append("a").Append("b").Append("c");
        Assert.AreEqual("/a/b/c", result.Pointer);
        Assert.AreEqual(3, result.Tokens.Count);
    }
}

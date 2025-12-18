using SergeiM.Json.Patch;

namespace SergeiM.Json.Tests.JsonPointerTests;

[TestClass]
public class ToStringTests
{
    [TestMethod]
    public void ToString_ReturnsPointerString()
    {
        Assert.AreEqual("/foo/bar", new JsonPointer("/foo/bar").ToString());
    }

    [TestMethod]
    public void ToString_EmptyPointer_ReturnsEmptyString()
    {
        Assert.AreEqual("", JsonPointer.Empty.ToString());
    }
}

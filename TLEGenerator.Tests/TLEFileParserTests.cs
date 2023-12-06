namespace TLEGenerator.Tests;

[TestClass]
public class TLEFileParserTests
{
    const string TestFile = "./TestData/weather.txt";

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void ShouldReturnExceptionWhenPathIsInvalid(string testPath)
    {
        TLEFileParser sut = new();

        Assert.ThrowsException<ArgumentException>(() => sut.ParseFile(testPath));
    }

    [TestMethod]
    public void ShouldReturnExceptionWhenFileDoesNotExist()
    {
        TLEFileParser sut = new();

        Assert.ThrowsException<FileNotFoundException>(() => sut.ParseFile("./fake.txt"));
    }

    [TestMethod]
    public void ShouldParseTheSpecifiedFile()
    {
        TLEFileParser sut = new();

        sut.ParseFile(TestFile);

        Assert.AreEqual(10, sut.Size());
    }

    [TestMethod]
    public void ShouldAvoidDuplicates()
    {
        TLEFileParser sut = new();

        sut.ParseFile(TestFile);
        sut.ParseFile(TestFile);

        Assert.AreEqual(10, sut.Size());
    }

    [TestMethod]
    public void ShouldReturnNullWhenTheRequestedDataDoesNotExist()
    {
        TLEFileParser sut = new();

        sut.ParseFile(TestFile);

        Assert.AreEqual(10, sut.Size());

        var tle = sut.Get("0");

        Assert.IsNull(tle);
    }

    [TestMethod]
    public void ShouldRetrieveTheRequestedDataIfExists()
    {
        TLEFileParser sut = new();

        sut.ParseFile(TestFile);

        Assert.AreEqual(10, sut.Size());

        var tle = sut.Get("33591");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 19"));
        Assert.AreEqual(24, tle.Title.Length);
        Assert.IsTrue(tle.Line1.StartsWith("1 33591U"));
        Assert.IsTrue(tle.Line2.StartsWith("2 33591"));
    }
}

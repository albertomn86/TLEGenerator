using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TleGenerator.Tests;

[TestClass]
public class TLEFileParserTests
{
    const string TestFile = "./TestData/test.txt";
    private TleFileParser tleFileParser;
    private TleDataCarrier tleDataCarrier;

    [TestInitialize]
    public void Setup()
    {
        tleFileParser = new(new FileStorage());
        tleDataCarrier = new();
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void ShouldReturnExceptionWhenPathIsInvalid(string testPath)
    {
        Assert.ThrowsExceptionAsync<ArgumentException>(async () => await tleFileParser.ParseFileAsync(testPath, tleDataCarrier));
    }

    [TestMethod]
    public void ShouldReturnExceptionWhenFileDoesNotExist()
    {
        Assert.ThrowsExceptionAsync<FileNotFoundException>(async () => await tleFileParser.ParseFileAsync("./fake.txt", tleDataCarrier));
    }

    [TestMethod]
    public async Task ShouldParseTheSpecifiedFile()
    {
        await tleFileParser.ParseFileAsync(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Count);
    }

    [TestMethod]
    public async Task ShouldAvoidDuplicates()
    {
        await tleFileParser.ParseFileAsync(TestFile, tleDataCarrier);
        await tleFileParser.ParseFileAsync(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Count);
    }

    [TestMethod]
    public async Task ShouldReturnNullWhenTheRequestedDataDoesNotExist()
    {
        await tleFileParser.ParseFileAsync(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Count);

        var tle = tleDataCarrier.Get("0");

        Assert.IsNull(tle);
    }

    [TestMethod]
    public async Task ShouldRetrieveTheRequestedDataIfExists()
    {
        await tleFileParser.ParseFileAsync(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Count);

        var tle = tleDataCarrier.Get("33591");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 19"));
        Assert.AreEqual(24, tle.Title.Length);
        Assert.IsTrue(tle.Line1.StartsWith("1 33591U"));
        Assert.IsTrue(tle.Line2.StartsWith("2 33591"));
    }
}

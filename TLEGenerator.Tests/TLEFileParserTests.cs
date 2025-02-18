namespace TleGenerator.Tests;

[TestClass]
public class TLEFileParserTests
{
    const string TestFile = "./TestData/test.txt";

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void ShouldReturnExceptionWhenPathIsInvalid(string testPath)
    {
        TleDataCarrier tleDataCarrier = new();

        Assert.ThrowsException<ArgumentException>(() => TleHandler.ParseFile(testPath, tleDataCarrier));
    }

    [TestMethod]
    public void ShouldReturnExceptionWhenFileDoesNotExist()
    {
        TleDataCarrier tleDataCarrier = new();

        Assert.ThrowsException<FileNotFoundException>(() => TleHandler.ParseFile("./fake.txt", tleDataCarrier));
    }

    [TestMethod]
    public void ShouldParseTheSpecifiedFile()
    {
        TleDataCarrier tleDataCarrier = new();

        TleHandler.ParseFile(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Size());
    }

    [TestMethod]
    public void ShouldAvoidDuplicates()
    {
        TleDataCarrier tleDataCarrier = new();

        TleHandler.ParseFile(TestFile, tleDataCarrier);
        TleHandler.ParseFile(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Size());
    }

    [TestMethod]
    public void ShouldReturnNullWhenTheRequestedDataDoesNotExist()
    {
        TleDataCarrier tleDataCarrier = new();

        TleHandler.ParseFile(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Size());

        var tle = tleDataCarrier.Get("0");

        Assert.IsNull(tle);
    }

    [TestMethod]
    public void ShouldRetrieveTheRequestedDataIfExists()
    {
        TleDataCarrier tleDataCarrier = new();

        TleHandler.ParseFile(TestFile, tleDataCarrier);

        Assert.AreEqual(10, tleDataCarrier.Size());

        var tle = tleDataCarrier.Get("33591");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 19"));
        Assert.AreEqual(24, tle.Title.Length);
        Assert.IsTrue(tle.Line1.StartsWith("1 33591U"));
        Assert.IsTrue(tle.Line2.StartsWith("2 33591"));
    }
}

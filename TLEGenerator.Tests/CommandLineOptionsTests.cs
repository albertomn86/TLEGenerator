namespace TleGenerator.Tests;

[TestClass]
public class CommandLineOptionsTests
{
    [TestMethod]
    [DataRow(["-o", "-i", "25338,28654,33591"])]
    [DataRow(["-i", "25338,28654,33591", "-o"])]
    [DataRow(["-i", "25338,28654,33591"])]
    public void ShouldRetrieveSatellitesListIfPresent(string[] args)
    {
        CommandLineOptions sut = new();

        sut.ParseArgs(args);

        Assert.IsNotNull(sut.Input);
    }

    [TestMethod]
    [DataRow(["-i", "-o", "test"])]
    [DataRow(["-o", "test", "-i"])]
    [DataRow(["-o", "test"])]
    public void ShouldRetrieveOutputFilePathIfPresent(string[] args)
    {
        CommandLineOptions sut = new();

        sut.ParseArgs(args);

        Assert.IsNotNull(sut.OutputFilePath);
    }

    [TestMethod]
    [DataRow([])]
    [DataRow(["-a", "test", "-i"])]
    [DataRow(["-oo", "test"])]
    [DataRow(["-o"])]
    public void ShouldNotParseInvalidArgs(string[] args)
    {
        CommandLineOptions sut = new();

        sut.ParseArgs(args);

        Assert.IsNull(sut.Input);
        Assert.IsNull(sut.OutputFilePath);
    }
}

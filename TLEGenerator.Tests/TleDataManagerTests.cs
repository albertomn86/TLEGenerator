using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TleGenerator.Tests;

[TestClass]
public class TLEDataManagerTest
{
    private Config config;
    private Mock<ITleDataDownloader> mockTleDataDownloader;
    private Mock<ITleDataCarrier> mockTleDataCarrier;
    private Mock<IFileManager> mockFileManager;
    private Mock<ITleFileParser> mockTleFileParser;

    [TestInitialize]
    public void Setup()
    {
        config = new Config
        {
            NoradUrl = "fakeUrl",
            Groups = [ "test" ],
            TempFolder = "TestData",
            TempFilesDays = 65000
        };

        mockTleDataDownloader = new Mock<ITleDataDownloader>();
        mockTleDataCarrier = new Mock<ITleDataCarrier>();
        mockFileManager = new Mock<IFileManager>();
        mockTleFileParser = new Mock<ITleFileParser>();

        mockTleDataCarrier.Setup(c => c.Get("33591")).Returns(new Tle { Title = "NOAA 19", Line1 = "", Line2 = "" });
        mockTleDataCarrier.Setup(c => c.Get("43013")).Returns(new Tle { Title = "NOAA 20", Line1 = "", Line2 = "" });
        mockTleDataCarrier.Setup(c => c.Get("54234")).Returns(new Tle { Title = "NOAA 21", Line1 = "", Line2 = "" });
        mockTleDataCarrier.Setup(c => c.Get("0101")).Returns((Tle)null);
    }

    [TestMethod]
    public async Task ShouldReturnTLEWhenCatalogNumberIsPresentInGroups()
    {
        var sut = CreateTleDataManager();

        await sut.RetrieveGroupsDataAsync(config.Groups);

        var tle = await sut.GetTLEAsync("33591");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 19"));
    }

    [TestMethod]
    public async Task ShouldReturnTLEWhenCatalogNumberIsPresentInTempFolder()
    {
        var sut = CreateTleDataManager();

        await sut.RetrieveGroupsDataAsync(config.Groups);

        var tle = await sut.GetTLEAsync("43013");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 20"));
    }

    [TestMethod]
    public async Task ShouldReturnTLEWhenCatalogNumberIsNotPresentInTempFolder()
    {
        var sut = CreateTleDataManager();

        await sut.RetrieveGroupsDataAsync(config.Groups);

        var tle = await sut.GetTLEAsync("54234");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 21"));
    }

    [TestMethod]
    public async Task ShouldReturnNullWhenCatalogNumberIsNotFound()
    {
        var sut = CreateTleDataManager();

        await sut.RetrieveGroupsDataAsync(config.Groups);

        var tle = await sut.GetTLEAsync("0101");

        Assert.IsNull(tle);
    }

    private TleDataManager CreateTleDataManager()
    {
        return new TleDataManager(mockFileManager.Object, mockTleDataCarrier.Object, mockTleDataDownloader.Object, mockTleFileParser.Object);
    }
}
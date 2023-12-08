namespace TLEGenerator.Tests;

[TestClass]
public class TLEDataManagerTest
{
    Config config = new()
    {
        NoradUrl = "fakeUrl",
        Groups = ["test"],
        TempFolder = "TestData",
        TempFilesDays = 65000
    };

    [TestMethod]
    public void ShouldReturnTLEWhenCatalogNumberIsPresentInGroups()
    {
        TLEDataManager sut = new(config);

        sut.RetrieveGroupsData();

        var tle = sut.GetTLE("33591");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 19"));
    }

    [TestMethod]
    public void ShouldReturnTLEWhenCatalogNumberIsPresentInTempFolder()
    {
        TLEDataManager sut = new(config);

        sut.RetrieveGroupsData();

        var tle = sut.GetTLE("43013");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 20"));
    }

    [TestMethod]
    public void ShouldReturnTLEWhenCatalogNumberIsNotPresentInTempFolder()
    {
        config.NoradUrl = "https://celestrak.com/NORAD/elements/gp.php";

        TLEDataManager sut = new(config);

        sut.RetrieveGroupsData();

        var tle = sut.GetTLE("54234");

        Assert.IsNotNull(tle);
        Assert.IsTrue(tle.Title.StartsWith("NOAA 21"));
    }

    [TestMethod]
    public void ShouldReturnNullWhenCatalogNumberIsNotFound()
    {
        config.NoradUrl = "https://celestrak.com/NORAD/elements/gp.php";

        TLEDataManager sut = new(config);

        sut.RetrieveGroupsData();

        var tle = sut.GetTLE("0101");

        Assert.IsNull(tle);
    }
}

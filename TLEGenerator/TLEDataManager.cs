namespace TleGenerator;

public class TLEDataManager(Config config)
{
    private readonly Config _config = config;
    private readonly TleDataCarrier tleDataCarrier = new();
    private readonly TLEDataDownloader tleDataDownloader = new(config.NoradUrl);

    public Tle? GetTLE(string catNumber)
    {
        var tle = tleDataCarrier.Get(catNumber);

        if (tle == null)
        {
            RetrieveDataByCatalogNumber(catNumber);
            tle = tleDataCarrier.Get(catNumber);
        }

        return tle;
    }

    public void RetrieveDataByCatalogNumber(string catNumber)
    {
        string path = Path.Combine(_config.TempFolder, $"{catNumber}.txt");

        if (!File.Exists(path) || IsOldFile(path))
        {
            tleDataDownloader.DownloadByCatalogNumber(catNumber, path);
        }

        TleHandler.ParseFile(path, tleDataCarrier);
    }

    public void RetrieveGroupsData()
    {
        foreach (var group in _config.Groups)
        {
            string path = Path.Combine(_config.TempFolder, $"{group}.txt");

            if (!File.Exists(path) || IsOldFile(path))
            {
                tleDataDownloader.DownloadGroupFile(group, path);
            }

            TleHandler.ParseFile(path, tleDataCarrier);
        }
    }

    private bool IsOldFile(string path)
    {
        DateTime lastModified = File.GetLastWriteTime(path);

        return (DateTime.Now - lastModified).TotalDays >= _config.TempFilesDays;
    }
}

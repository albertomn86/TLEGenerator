namespace TLEGenerator;

public class TLEDataManager
{
    private readonly Config config;
    private readonly TLEFileParser tleFileParser;
    private readonly TLEDataDownloader tleDataDownloader;

    public TLEDataManager(Config config)
    {
        this.config = config;
        tleFileParser = new();
        tleDataDownloader = new(config);
    }

    public TLE? GetTLE(string catNumber)
    {
        var tle = tleFileParser.Get(catNumber);

        if (tle == null)
        {
            RetrieveDataByCatalogNumber(catNumber);
            tle = tleFileParser.Get(catNumber);
        }

        return tle;
    }

    public void RetrieveDataByCatalogNumber(string catNumber)
    {
        string path = Path.Combine(config.TempFolder, $"{catNumber}.txt");

        if (!File.Exists(path) || IsOldFile(path))
        {
            tleDataDownloader.DownloadByCatalogNumber(catNumber, path);
        }

        tleFileParser.ParseFile(path);
    }

    public void RetrieveGroupsData()
    {
        foreach (var group in config.Groups)
        {
            string path = Path.Combine(config.TempFolder, $"{group}.txt");

            if (!File.Exists(path) || IsOldFile(path))
            {
                tleDataDownloader.DownloadGroupFile(group, path);
            }

            tleFileParser.ParseFile(path);
        }
    }

    private bool IsOldFile(string path)
    {

        DateTime lastModified = File.GetLastWriteTime(path);

        return (DateTime.Now - lastModified).TotalDays >= config.TempFilesDays;
    }

    public int GetApiRequestsNumber()
    {
        return tleDataDownloader.GetApiRequestsNumber();
    }
}

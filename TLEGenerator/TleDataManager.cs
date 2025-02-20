namespace TleGenerator;

public class TleDataManager(
    IFileManager fileManager,
    ITleDataCarrier tleDataCarrier,
    ITleDataDownloader tleDataDownloader,
    ITleFileParser tleFileParser) 
{
    private readonly IFileManager _fileManager = fileManager;
    private readonly ITleDataCarrier _tleDataCarrier = tleDataCarrier;
    private readonly ITleDataDownloader _tleDataDownloader = tleDataDownloader;
    private readonly ITleFileParser _tleFileParser = tleFileParser;

    public async Task<Tle?> GetTLEAsync(string catNumber)
    {
        var tle = _tleDataCarrier.Get(catNumber);

        if (tle == null)
        {
            await RetrieveDataByCatalogNumberAsync(catNumber);
            tle = _tleDataCarrier.Get(catNumber);
        }

        return tle;
    }

    public async Task RetrieveDataByCatalogNumberAsync(string catNumber)
    {
        string path = _fileManager.GetFilePath(catNumber);

        if (!File.Exists(path) || _fileManager.IsOldFile(path))
        {
            await _tleDataDownloader.DownloadByCatalogNumberAsync(catNumber, path);
        }

        await _tleFileParser.ParseFileAsync(path, _tleDataCarrier);
    }

    public async Task RetrieveGroupsDataAsync(IEnumerable<string> groups)
    {
        foreach (var group in groups)
        {
            string path = _fileManager.GetFilePath(group);

            if (!File.Exists(path) || _fileManager.IsOldFile(path))
            {
                await _tleDataDownloader.DownloadGroupFileAsync(group, path);
            }

            await _tleFileParser.ParseFileAsync(path, _tleDataCarrier);
        }
    }
}

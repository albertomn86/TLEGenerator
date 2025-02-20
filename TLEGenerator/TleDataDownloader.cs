namespace TleGenerator;

public class TleDataDownloader(string tleProviderUrl, IHttpClientWrapper httpClient, IFileStorage fileStorage) : ITleDataDownloader
{
    private readonly string _tleProviderUrl = tleProviderUrl;
    private readonly IHttpClientWrapper _httpClient = httpClient;
    private readonly IFileStorage _fileStorage = fileStorage;

    public async Task DownloadGroupFileAsync(string group, string path)
    {
        var url = $"{_tleProviderUrl}?GROUP={group}&FORMAT=TLE";
        await DownloadAndSaveAsync(url, path);
    }

    public async Task DownloadByCatalogNumberAsync(string catNumber, string path)
    {
        var url = $"{_tleProviderUrl}?CATNR={catNumber}&FORMAT=TLE";
        await DownloadAndSaveAsync(url, path);
    }

    private async Task DownloadAndSaveAsync(string url, string destinationPath)
    {
        var fileStream = await _httpClient.GetStreamAsync(url);

        if (fileStream != Stream.Null)
        {
            await _fileStorage.SaveStreamAsync(fileStream, destinationPath);
        }
    }
}

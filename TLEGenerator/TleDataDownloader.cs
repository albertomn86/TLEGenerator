namespace TleGenerator;

public class TLEDataDownloader(string tleProviderUrl)
{
    private readonly string _tleProviderUrl = tleProviderUrl;
    private readonly Lazy<HttpClient> _httpClient = new();
    private HttpClient HttpClient => _httpClient.Value;

    public void DownloadGroupFile(string group, string path)
    {
        var url = $"{_tleProviderUrl}?GROUP={group}&FORMAT=TLE";

        var task = Task.Run(() => DownloadAndSaveAsync(url, path));
        task.Wait();
    }

    public void DownloadByCatalogNumber(string catNumber, string path)
    {
        var url = $"{_tleProviderUrl}?CATNR={catNumber}&FORMAT=TLE";

        var task = Task.Run(() => DownloadAndSaveAsync(url, path));
        task.Wait();
    }

    private async Task DownloadAndSaveAsync(string url, string destinationPath)
    {
        var fileStream = await GetFileStream(url);

        if (fileStream != Stream.Null)
        {
            await SaveStream(fileStream, destinationPath);
        }
    }

    private async Task<Stream> GetFileStream(string url)
    {
        var fileStream = await HttpClient.GetStreamAsync(url);

        return fileStream;
    }

    private static async Task SaveStream(Stream fileStream, string destinationPath)
    {
        using FileStream outputFileStream = new(destinationPath, FileMode.Create);

        await fileStream.CopyToAsync(outputFileStream);
    }
}

namespace TLEGenerator.Program;

public class TLEDataDownloader
{
    private readonly HttpClient httpClient;
    private readonly Config config;

    public TLEDataDownloader(Config config)
    {
        httpClient ??= new();
        this.config = config;
    }

    public void DownloadGroupFile(string group, string path)
    {
        var url = $"{config.NoradUrl}?GROUP={group}&FORMAT=TLE";

        var task = Task.Run(() => DownloadAndSaveAsync(url, path));
        task.Wait();
    }

    public void DownloadByCatalogNumber(string catNumber, string path)
    {
        var url = $"{config.NoradUrl}?CATNR={catNumber}&FORMAT=TLE";

        var task = Task.Run(() => DownloadAndSaveAsync(url, path));
        task.Wait();
    }

    private async Task DownloadAndSaveAsync(string url, string destinationPath)
    {
        Stream fileStream = await GetFileStream(url);

        if (fileStream != Stream.Null)
        {
            await SaveStream(fileStream, destinationPath);
        }
    }

    private async Task<Stream> GetFileStream(string url)
    {
        Stream fileStream = await httpClient.GetStreamAsync(url);

        return fileStream;
    }

    private static async Task SaveStream(Stream fileStream, string destinationPath)
    {
        using FileStream outputFileStream = new(destinationPath, FileMode.Create);

        await fileStream.CopyToAsync(outputFileStream);
    }
}

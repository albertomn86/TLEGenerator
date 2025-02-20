namespace TleGenerator;

public interface ITleDataDownloader
{
    Task DownloadGroupFileAsync(string group, string path);
    Task DownloadByCatalogNumberAsync(string catNumber, string path);
}
namespace TleGenerator;

public interface IHttpClientWrapper
{
    Task<Stream> GetStreamAsync(string url);
}
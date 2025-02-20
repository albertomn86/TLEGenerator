namespace TleGenerator;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new();

    public async Task<Stream> GetStreamAsync(string url)
    {
        return await _httpClient.GetStreamAsync(url);
    }
}
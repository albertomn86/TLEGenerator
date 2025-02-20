namespace TleGenerator;

public class TleFileParser(IFileStorage fileStorage) : ITleFileParser
{
    private const int TITLE_LINE_LENGTH = 24;
    private readonly IFileStorage _fileStorage = fileStorage;

    public async Task ParseFileAsync(string filePath, ITleDataCarrier tleDataCarrier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        await foreach (var tle in ReadTleFromFileAsync(filePath))
        {
            tleDataCarrier.Add(tle.GetId(), tle);
        }
    }

    private async IAsyncEnumerable<Tle> ReadTleFromFileAsync(string filePath)
    {
        await using Stream stream = _fileStorage.GetFileStream(filePath);
        using StreamReader sr = new(stream);

        string? line;
        while ((line = await sr.ReadLineAsync()) != null)
        {
            if (line.Length == TITLE_LINE_LENGTH)
            {
                string? line1 = await sr.ReadLineAsync();
                string? line2 = await sr.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line1) || string.IsNullOrWhiteSpace(line2))
                {
                    continue;
                }

                yield return new Tle
                {
                    Title = line,
                    Line1 = line1,
                    Line2 = line2
                };
            }
        }
    }
}

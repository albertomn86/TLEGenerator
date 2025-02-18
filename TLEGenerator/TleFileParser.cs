using System.Data;

namespace TleGenerator;

public static class TleHandler
{
    private const int TITLE_LINE_LENGTH = 24;

    public static void ParseFile(string filePath, TleDataCarrier tleDataCarrier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        using StreamReader sr = File.OpenText(filePath);
        string? line;

        while ((line = sr.ReadLine()) != null)
        {
            if (line.Length == TITLE_LINE_LENGTH)
            {
                Tle tle = new()
                {
                    Title = line,
                    Line1 = sr.ReadLine() ?? throw new NoNullAllowedException(),
                    Line2 = sr.ReadLine() ?? throw new NoNullAllowedException()
                };
                tleDataCarrier.Add(tle.GetId(), tle);
            }
        }
    }
}

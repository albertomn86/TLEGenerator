using System.Data;

namespace TLEGenerator;

public class TLEFileParser
{
    private const int TITLE_LINE_LENGTH = 24;
    private readonly Dictionary<string, TLE> TLEData;

    public TLEFileParser()
    {
        TLEData = [];
    }

    public void ParseFile(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        using StreamReader sr = File.OpenText(filePath);
        string? line;

        while ((line = sr.ReadLine()) != null)
        {
            if (line.Length == TITLE_LINE_LENGTH)
            {
                TLE tle = new()
                {
                    Title = line,
                    Line1 = sr.ReadLine() ?? throw new NoNullAllowedException(),
                    Line2 = sr.ReadLine() ?? throw new NoNullAllowedException()
                };
                TLEData.TryAdd(tle.GetId(), tle);
            }
        }
    }

    public int Size()
    {
        return TLEData.Count;
    }

    public TLE? Get(string value)
    {
        if (TLEData.TryGetValue(value, out var tle))
        {
            return tle;
        }

        return null;
    }
}

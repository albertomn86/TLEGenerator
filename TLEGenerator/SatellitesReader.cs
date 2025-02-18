using System.Text.Json;

namespace TleGenerator;

public static class SatellitesReader
{
    public static List<string> ReadList(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(filePath);
        }

        using StreamReader reader = File.OpenText(filePath);

        string json = reader.ReadToEnd();

        return JsonSerializer.Deserialize<List<string>>(json) ?? [];
    }
}

using System.Text.Json;

namespace TLEGenerator;

public class Config
{
    public string NoradUrl { get; set; } = "https://celestrak.com/NORAD/elements/gp.php";
    public List<string> Groups { get; set; } = [];
    public string SatellitesListPath { get; set; } = "satellites.json";
    public string OutputFilePath { get; set; } = "custom_TLE.txt";
    public string TempFolder { get; set; } = Path.GetTempPath();
    public int TempFilesDays { get; set; } = 1;

    public void ReadConfigFile(string configFile = "config.json")
    {
        if (File.Exists(configFile))
        {
            using StreamReader reader = File.OpenText(configFile);

            string json = reader.ReadToEnd();
            var items = JsonSerializer.Deserialize<Config>(json);

            if (items != null)
            {
                NoradUrl = items.NoradUrl;
                Groups = items.Groups;
                SatellitesListPath = items.SatellitesListPath;
                TempFolder = items.TempFolder;
                TempFilesDays = items.TempFilesDays;
            }
        }
    }
}

using System.Text.Json;

namespace TLEGenerator.Program;

public class Config
{
    public string NoradUrl { get; set; } = "https://celestrak.com/NORAD/elements/gp.php";
    public List<string> Groups { get; set; } = [];
    public string TempFolder { get; set; } = "/tmp";
    public int TempFilesDays { get; set; } = 1;

    public void ReadConfigFile(string configFile = "./config.json")
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
                TempFolder = items.TempFolder;
                TempFilesDays = items.TempFilesDays;
            }
        }
    }
}

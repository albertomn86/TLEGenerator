using System.Text;

namespace TLEGenerator.Program;

class Program
{
    static void Main(string[] args)
    {
        Config config = new();
        config.ReadConfigFile(); 

        List<string> satellites = SatellitesReader.ReadList(config.SatellitesListPath);
    
        TLEDataManager tleDataManager = new(config);
        tleDataManager.RetrieveGroupsData();

        int satellitesFound = 0;
        using StreamWriter outputFile = new StreamWriter(config.OutputFilePath);

        foreach (var satellite in satellites)
        {
            var tle = tleDataManager.GetTLE(satellite);

            if (tle != null) {
                Console.WriteLine($"✓ Saved TLE for {tle.Title.Trim()} ({satellite})");
                satellitesFound += 1;
                outputFile.WriteLine(tle.ToString());
                continue;
            }

            Console.WriteLine($"✗ Could not find TLE for {satellite}");
        }

        Console.WriteLine($"TLEs retrieved: {satellitesFound}/{satellites.Count}");
        Console.WriteLine($"API requests: {tleDataManager.GetApiRequestsNumber()}");
        Console.WriteLine($"Output TLE file: {Path.GetFullPath(config.OutputFilePath)}");
    }
}

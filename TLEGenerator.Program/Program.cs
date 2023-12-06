namespace TLEGenerator.Program;

class Program
{
    static void Main(string[] args)
    {
        Config config = new();
        config.ReadConfigFile();

        List<string> satellites = new(["25338", "28654", "33591"]);
        TLEDataManager tleDataManager = new(config);
        tleDataManager.RetrieveGroupsData();

        foreach (var satellite in satellites)
        {
            var tle = tleDataManager.GetTLE(satellite);
            Console.WriteLine(tle?.Title);
        }
    }
}

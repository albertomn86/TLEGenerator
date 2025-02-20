using Microsoft.Extensions.DependencyInjection;

namespace TleGenerator;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var config = serviceProvider.GetRequiredService<Config>();
        var commandLineOptions = serviceProvider.GetRequiredService<ICommandLineOptions>();
        var tleDataManager = serviceProvider.GetRequiredService<TleDataManager>();

        config.ReadConfigFile();

        await commandLineOptions.ParseArgsAsync(args);

        await tleDataManager.RetrieveGroupsDataAsync(config.Groups);

        List<string> satellites = GetSatelliteList(commandLineOptions, config);

        await ProcessAndSaveTLEsAsync(satellites, tleDataManager, commandLineOptions.OutputFilePath ?? config.OutputFilePath);
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<Config>();
        services.AddSingleton<ICommandLineOptions, CommandLineOptions>();
        services.AddSingleton<IFileStorage, FileStorage>();
        services.AddSingleton<ITleDataCarrier, TleDataCarrier>();
        services.AddSingleton<ITleDataDownloader, TleDataDownloader>();
        services.AddSingleton<ITleFileParser, TleFileParser>();
        services.AddSingleton<TleDataManager>();

        return services.BuildServiceProvider();
    }

    private static List<string> GetSatelliteList(ICommandLineOptions commandLineOptions, Config config)
    {
        return commandLineOptions.Input == null
            ? SatellitesReader.ReadList(config.SatellitesListPath)
            : [.. commandLineOptions.Input.Split(',')];
    }

    private static async Task ProcessAndSaveTLEsAsync(List<string> satellites, TleDataManager tleDataManager, string outputFilePath)
    {
        int satellitesFound = 0;
        await using StreamWriter outputFile = new(outputFilePath);

        foreach (var satellite in satellites)
        {
            var tle = await tleDataManager.GetTLEAsync(satellite);

            if (tle != null)
            {
                Console.WriteLine($"✓ Saved TLE for {tle.Title.Trim()} ({satellite})");
                satellitesFound++;
                await outputFile.WriteLineAsync(tle.ToString());
            }
            else
            {
                Console.WriteLine($"✗ Could not find TLE for {satellite}");
            }
        }

        Console.WriteLine($"TLEs retrieved: {satellitesFound}/{satellites.Count}");
        Console.WriteLine($"Output TLE file: {Path.GetFullPath(outputFilePath)}");
    }
}

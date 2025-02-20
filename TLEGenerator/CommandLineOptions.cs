using System.CommandLine;

namespace TleGenerator;

public class CommandLineOptions : ICommandLineOptions
{
    public string? Input { get; private set; }
    public string? OutputFilePath { get; private set; }

    public async Task ParseArgsAsync(string[] args)
    {
        var inputOption = new Option<string?>(
            aliases: ["--input", "-i"],
            description: "List of catalog numbers of satellites separated by commas.");

        var outputOption = new Option<string?>(
            aliases: ["--output", "-o"],
            description: "Output file path.",
            getDefaultValue: () => "custom_TLE.txt");

        var rootCommand = new RootCommand
        {
            inputOption,
            outputOption
        };

        rootCommand.SetHandler((input, output) =>
        {
            Input = input;
            OutputFilePath = output;
        }, inputOption, outputOption);

        await rootCommand.InvokeAsync(args);
    }
}

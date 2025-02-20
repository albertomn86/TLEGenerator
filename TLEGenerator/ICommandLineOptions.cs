namespace TleGenerator;

public interface ICommandLineOptions
{
    string? Input { get; }
    string? OutputFilePath { get; }
    Task ParseArgsAsync(string[] args);
}
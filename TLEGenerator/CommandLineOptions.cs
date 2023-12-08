namespace TLEGenerator;

public class CommandLineOptions
{
    public string? Input { get; set; }

    public string? OutputFilePath { get; set; }

    public void ParseArgs(string[] args)
    {
        if (args.Length < 2) return;

        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "-i")
            {
                Input = args[i + 1];
            }
            else if (args[i] == "-o")
            {
                OutputFilePath = args[i + 1];
            }
        }
    }
}

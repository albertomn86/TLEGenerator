namespace TleGenerator;

public class FileManager(Config config) : IFileManager
{
    private readonly Config _config = config;

    public string GetFilePath(string identifier) => 
        Path.Combine(_config.TempFolder, $"{identifier}.txt");

    public bool IsOldFile(string path)
    {
        if (!File.Exists(path)) return true;
        DateTime lastModified = File.GetLastWriteTime(path);
        return (DateTime.Now - lastModified).TotalDays >= _config.TempFilesDays;
    }
}
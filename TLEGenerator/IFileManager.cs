namespace TleGenerator;

public interface IFileManager
{
    bool IsOldFile(string path);
    string GetFilePath(string identifier);
}
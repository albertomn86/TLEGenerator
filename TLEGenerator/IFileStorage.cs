namespace TleGenerator;

public interface IFileStorage
{
    Task SaveStreamAsync(Stream stream, string destinationPath);
    Stream GetFileStream(string filePath);
}
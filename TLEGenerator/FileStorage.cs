namespace TleGenerator;

public class FileStorage : IFileStorage
{
    public async Task SaveStreamAsync(Stream stream, string destinationPath)
    {
        using FileStream outputFileStream = new(destinationPath, FileMode.Create);
        await stream.CopyToAsync(outputFileStream);
    }

    public Stream GetFileStream(string filePath)
    {
        return File.Exists(filePath)
            ? new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
            : Stream.Null;
    }
}
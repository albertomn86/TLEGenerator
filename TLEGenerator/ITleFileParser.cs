namespace TleGenerator;

public interface ITleFileParser
{
    Task ParseFileAsync(string filePath, ITleDataCarrier tleDataCarrier);
}
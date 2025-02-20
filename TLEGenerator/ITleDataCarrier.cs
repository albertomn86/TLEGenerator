namespace TleGenerator;

public interface ITleDataCarrier
{
    void Add(string id, Tle tle);
    int Count { get; }
    Tle? Get(string id);
}
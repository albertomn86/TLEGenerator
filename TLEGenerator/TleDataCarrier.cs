namespace TleGenerator;

public class TleDataCarrier : ITleDataCarrier
{
    private readonly Dictionary<string, Tle> _elements = [];

    public void Add(string id, Tle tle) => _elements.TryAdd(id, tle);

    public int Count => _elements.Count;

    public Tle? Get(string id) => _elements.TryGetValue(id, out var tle) ? tle : null;
}

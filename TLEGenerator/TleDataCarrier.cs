namespace TleGenerator;

public class TleDataCarrier
{
    private readonly Dictionary<string, Tle> _elements;

    public TleDataCarrier()
    {
        _elements ??= [];
    }

    public void Add(string id, Tle tle)
    {
        _elements.TryAdd(id, tle);
    }

    public int Size() => _elements.Count;

    public Tle? Get(string id)
    {
        if (_elements.TryGetValue(id, out var tle))
        {
            return tle;
        }

        return null;
    }
}
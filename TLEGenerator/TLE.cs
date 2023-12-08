namespace TLEGenerator;

public sealed class TLE
{
    public required string Title { get; set; }
    public required string Line1 { get; set; }
    public required string Line2 { get; set; }

    public string GetId()
    {
        return Line2.Split()[1];
    }

    public override string ToString()
    {
        return $"{Title}\r\n{Line1}\r\n{Line2}";
    }
}

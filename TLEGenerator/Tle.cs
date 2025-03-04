namespace TleGenerator;

public sealed class Tle
{
    public required string Title { get; set; }
    public required string Line1 { get; set; }
    public required string Line2 { get; set; }

    public string GetId() => Line2.Split()[1];

    public override string ToString() =>  $"{Title}\r\n{Line1}\r\n{Line2}";
}

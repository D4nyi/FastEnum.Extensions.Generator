namespace FastEnum.Extensions.Generator.Data;

public static class ColorNames
{
    public const string Red   = "Crimson red";
    public const string Green = "Pine";
    public const string Black = "Vantablack";
    public const string Blue  = "Sky";

    public static readonly IReadOnlyDictionary<Color, string> Lookup = new Dictionary<Color, string>
    {
        { Color.Red,   Red },
        { Color.Green, Green },
        { Color.Black, Black },
        { Color.Blue,  Blue }
    };
}

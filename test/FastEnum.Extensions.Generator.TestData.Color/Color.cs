using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FastEnum.Extensions.Generator.TestData;

[FastEnum.Extensions]
#pragma warning disable CA1028
#pragma warning disable CA1027
public enum Color : short
#pragma warning restore CA1027
#pragma warning restore CA1028
{
    [Description(ColorNames.Red), EnumMember(Value = ColorNames.Red), Display(Name = ColorNames.Red, Description = ColorNames.Red)]
    Red = 4,
    [Description(ColorNames.Green), EnumMember(Value = ColorNames.Green), Display(Name = ColorNames.Green, Description = ColorNames.Green)]
    Green = 2,
    [Description(ColorNames.Black), EnumMember(Value = ColorNames.Black), Display(Name = ColorNames.Black, Description = ColorNames.Black)]
    Black = 0,
    [Description(ColorNames.Blue), EnumMember(Value = ColorNames.Blue), Display(Name = ColorNames.Blue, Description = ColorNames.Blue)]
    Blue = 1,
}

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
        { Color.Blue,  Blue },
    };
}

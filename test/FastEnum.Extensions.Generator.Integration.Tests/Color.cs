using System.ComponentModel;

namespace FastEnum.Extensions.Generator.IntegrationTests;

[Extensions]
public enum Color : short
{
    [Description("Bright Red")]
    Red = 4,
    [Description("Deep Green")]
    Green = 2,
    [Description("Vantablack")]
    Black = 0,
    [Description("Teal")]
    Blue = 1,
}

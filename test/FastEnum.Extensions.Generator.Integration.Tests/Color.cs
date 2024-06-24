using System.ComponentModel;

namespace FastEnum.Extensions.Generator.IntegrationTests;

[Extensions]
#pragma warning disable CA1028
#pragma warning disable CA1027
public enum Color : short
#pragma warning restore CA1027
#pragma warning restore CA1028
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

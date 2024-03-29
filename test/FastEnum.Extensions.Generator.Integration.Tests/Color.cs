using System.ComponentModel;

namespace FastEnum.Extensions.Generator.IntegrationTests;

[Extensions]
public enum Color
{
    [Description("Bright Red")]
    Red = 1,
    [Description("Deep Green")]
    Green = 2,
    [Description("Teal")]
    Blue = 4,
}

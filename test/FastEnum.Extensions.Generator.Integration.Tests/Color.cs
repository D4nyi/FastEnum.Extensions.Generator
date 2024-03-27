using System.ComponentModel;

namespace FastEnum.Extensions.Generator.IntegrationTests;

[Extensions]
public enum Color
{
    [Description("Not red")]
    Red = 1,
    Green = 2,
    Blue = 4,
}

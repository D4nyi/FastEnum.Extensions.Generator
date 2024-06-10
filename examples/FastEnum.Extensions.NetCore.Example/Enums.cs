using System.ComponentModel;

namespace FastEnum.Extensions.NetCore.Example;

public class NestingClass
{
    [Extensions]
    public enum NestedInClassEnum
    {
        None,
        First,
        Second,
        Third,
        Fourth,
    }
}

[Extensions, Flags]
public enum Color
{
    [Description("Crimson Red")]
    Red = 0x990000,
    [Description("Pine")]
    Green = 0x166138,
    [Description("Sky")]
    Blue = 0x87CEEB,
}

[Extensions, Flags]
public enum Options : byte
{
    None = 0,
    ToString = 1,
    [Description("Parsssse")]
    Parse = 2,
    HasFlag = 4,
}

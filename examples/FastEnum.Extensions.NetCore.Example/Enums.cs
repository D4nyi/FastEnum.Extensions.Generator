using System.ComponentModel;

namespace FastEnum.Extensions.NetCore.Example;

#pragma warning disable CA1028
#pragma warning disable CA2217

public static class NestingClass
{
    [Extensions]
    public enum NestedInClass
    {
        None,
        First,
        Second,
        Third,
        Fourth
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
    Blue = 0x87CEEB
}

[Extensions, Flags]
public enum GenerationOptions : byte
{
    None = 0,
    ToString = 1,
    [Description("Parsssse")]
    Parse = 2,
    HasFlag = 4
}

#pragma warning restore CA1028
#pragma warning restore CA2217

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FastEnum.Extensions.NetCore.Example;

#pragma warning disable CA1028
#pragma warning disable CA2217

public static class NestingClass
{
    [Extensions]
    public enum NestedInClass
    {
        None = 0,
        One = 1,
        One2 = One
    }
}

[Extensions, Flags]
public enum Color
{
    [Description("Red")]
    Red = 0xFF0000,

    [Display(Name = "Green", Description = "Green")]
    Green = 0x00FF00,

    [EnumMember(Value = "Blue")]
    Blue = 0x0000FF
}

[Extensions, Flags]
public enum GenerationOptions : SByte
{
    None = 0,
    ToString = 1,
    Parse = 2,
    HasFlag = 4
}

#pragma warning restore CA1028
#pragma warning restore CA2217

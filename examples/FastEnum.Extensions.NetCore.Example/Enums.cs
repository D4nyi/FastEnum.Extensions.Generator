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
        None
    }
}

[Extensions, Flags]
public enum Color
{
    [Description("Crimson Red")]
    Red = 0x990000,

    [Display(Name = "Pine", Description = "Pine")]
    Green = 0x166138,

    [EnumMember(Value = "Sky")]
    Blue = 0x87CEEB
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

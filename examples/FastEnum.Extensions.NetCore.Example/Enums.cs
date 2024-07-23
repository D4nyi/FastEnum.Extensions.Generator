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
    [Description("Crimson Red"), EnumMember(Value = "Crimson Red"), Display(Name = "Crimson Red", Description = "Crimson Red")]
    Red = 0x990000,
    [Description("Pine"), EnumMember(Value = "Pine"), Display(Name = "Pine", Description = "Pine")]
    Green = 0x166138,
    [Description("Sky"), EnumMember(Value = "Sky"), Display(Name = "Sky", Description = "Sky")]
    Blue = 0x87CEEB
}

[Extensions, Flags]
public enum GenerationOptions : byte
{
    [Display(Name = "None", Description = "Nonee")]
    None = 0,
    [EnumMember(Value = "")]
    ToString = 1,
    [Description("Parsssse")]
    Parse = 2,
    HasFlag = 4
}

#pragma warning restore CA1028
#pragma warning restore CA2217

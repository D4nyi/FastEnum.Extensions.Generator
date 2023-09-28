using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FastEnum.Extensions.Generator.Data;

[Extensions, Flags]
#pragma warning disable CA1028
public enum Color : short
#pragma warning restore CA1028
{
    [Description(ColorNames.Red), EnumMember(Value = ColorNames.Red), Display(Name = ColorNames.Red, Description = ColorNames.Red)]
    Red = 4,
    [Description(ColorNames.Green), EnumMember(Value = ColorNames.Green), Display(Name = ColorNames.Green, Description = ColorNames.Green)]
    Green = 2,
    [Description(ColorNames.Black), EnumMember(Value = ColorNames.Black), Display(Name = ColorNames.Black, Description = ColorNames.Black)]
#pragma warning disable CA1008
    Black = 0,
#pragma warning restore CA1008 // Enums should have zero value; here its complaining that this is not called as 'None' 😑
    [Description(ColorNames.Blue), EnumMember(Value = ColorNames.Blue), Display(Name = ColorNames.Blue, Description = ColorNames.Blue)]
    Blue = 1,
}

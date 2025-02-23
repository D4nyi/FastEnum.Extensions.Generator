using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using FastEnum.Attributes;

namespace FastEnum.Extensions.Generator.Data;

[Extensions]
public enum GenerationOption
{
    [EnumMember(Value = GenerationOptionNames.None)]
    None = 0,

    [Description(GenerationOptionNames.ToStringKey)]
    ToString = 1,

    [Description(GenerationOptionNames.ToStringFormat)]
    ToStringFormat = ToString,

    [Display(Name = GenerationOptionNames.Parse, Description = GenerationOptionNames.Parse)]
    Parse = 2
}

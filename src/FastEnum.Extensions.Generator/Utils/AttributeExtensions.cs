using FastEnum.Extensions.Generator.Specs;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Utils;

internal static class AttributeExtensions
{
    public static AttributeValues GetAttributeValues(this AttributeInternalsSpec[] enumField)
    {
        AttributeValues data = new();

#pragma warning disable CA1307
#pragma warning disable CA1309 // No need for checking cultures and casing
        foreach (AttributeInternalsSpec spec in enumField)
        {
            string? metadataName = spec.MetadataName;

            if (Constants.DisplayAttributeName.Equals(metadataName) && spec.NamedArguments.Length > 0)
            {
                foreach (KeyValuePair<string, TypedConstant> argument in spec.NamedArguments)
                {
                    switch (argument.Key)
                    {
                        case "Name":
                            data.DisplayName = argument.Value.Value?.ToString();
                            break;
                        case "Description":
                            data.DisplayDescription = argument.Value.Value?.ToString();
                            break;
                    }
                }
            }
            else if (Constants.EnumMemberAttributeName.Equals(metadataName) && spec.NamedArguments.Length == 1)
            {
                data.EnumMemberValue = spec.NamedArguments[0].Value.Value?.ToString();
            }
            else if (Constants.DescriptionAttributeName.Equals(metadataName) && spec.ConstructorArgument is not null)
            {
                data.Description = spec.ConstructorArgument.ToString();
            }
        }
#pragma warning restore CA1309
#pragma warning restore CA1307

        return data;
    }
}

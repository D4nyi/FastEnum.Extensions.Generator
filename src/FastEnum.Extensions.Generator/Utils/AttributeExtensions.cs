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

            if (Constants.DisplayAttributeName.Equals(metadataName) && spec.NamedArguments.Count > 0)
            {
                spec.NamedArguments.TryGetValue("Name", out TypedConstant name);
                spec.NamedArguments.TryGetValue("Description", out TypedConstant description);

                data.DisplayName = name.Value?.ToString();
                data.DisplayDescription = description.Value?.ToString();
            }
            else if (Constants.EnumMemberAttributeName.Equals(metadataName) && spec.NamedArguments.TryGetValue("Value", out TypedConstant value))
            {
                data.EnumMemberValue = value.Value?.ToString();
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

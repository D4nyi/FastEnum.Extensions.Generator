using FastEnum.Extensions.Generator.Specs;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Utils;

internal static class AttributeExtensions
{
    public static AttributeValues GetAttributeValues(this IFieldSymbol enumField)
    {
        AttributeValues data = new();

        foreach (AttributeData a in enumField.GetAttributes())
        {
            if (a.AttributeDataIs(Constants.DisplayAttributeFullName) && a.NamedArguments.Length > 1)
            {
                foreach (KeyValuePair<string, TypedConstant> argument in a.NamedArguments)
                {
                    if (argument.Key == "Name")
                    {
                        data.DisplayName = argument.Value.Value?.ToString();
                    }
                    else if (argument.Key == "Description")
                    {
                        data.DisplayDescription = argument.Value.Value?.ToString();
                    }
                }
            }
            else if (a.AttributeDataIs(Constants.EnumMemberAttributeFullName) && a.NamedArguments.Length == 1)
            {
                data.EnumMemberValue = a.NamedArguments[0].Value.Value?.ToString();
            }
            else if (a.AttributeDataIs(Constants.DescriptionAttributeFullName) && a.ConstructorArguments.Length == 1)
            {
                data.Description = a.ConstructorArguments[0].Value?.ToString();
            }
        }

        return data;
    }

    internal static bool AttributeDataIs(this AttributeData attributeData, string attributeFullName) =>
        attributeData.AttributeClass?.ToDisplayString() == attributeFullName;
}

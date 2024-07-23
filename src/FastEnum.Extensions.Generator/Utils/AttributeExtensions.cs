using FastEnum.Extensions.Generator.Specs;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Utils;

internal static class AttributeExtensions
{
    public static AttributeValues ReadAttributeValues(
        this IFieldSymbol enumField,
        INamedTypeSymbol displayAttributeSymbol,
        INamedTypeSymbol enumMemberAttributeSymbol,
        INamedTypeSymbol descriptionAttribute)
    {
        AttributeValues data = new();

        foreach (AttributeData a in enumField.GetAttributes())
        {
            if (SymbolEqualityComparer.Default.Equals(a.AttributeClass, displayAttributeSymbol) && a.NamedArguments.Length > 1)
            {
                foreach (var argument in a.NamedArguments)
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
            else if (SymbolEqualityComparer.Default.Equals(a.AttributeClass, enumMemberAttributeSymbol) && a.NamedArguments.Length == 1)
            {
                data.EnumMemberValue = a.NamedArguments[0].Value.Value?.ToString();
            }
            else if (SymbolEqualityComparer.Default.Equals(a.AttributeClass, descriptionAttribute) && a.ConstructorArguments.Length == 1)
            {
                data.Description = a.ConstructorArguments[0].Value?.ToString();
            }
        }

        return data;
    }
}

using System.Linq;
using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Utils;

internal static class AttributeExtensions
{
    public static string? ReadDescriptionValue(this IFieldSymbol enumField, INamedTypeSymbol descriptionAttribute)
    {
        AttributeData? attributeData = GetAttributeData(enumField, descriptionAttribute);

        return 
            attributeData is { ConstructorArguments.Length: 1 }
                ? attributeData.ConstructorArguments[0].Value?.ToString()
                : null;
    }
    
    private static AttributeData? GetAttributeData(IFieldSymbol symbol, INamedTypeSymbol attributeType)
    {
        return symbol.GetAttributes()
            .FirstOrDefault(a =>
                SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeType));
    }
}

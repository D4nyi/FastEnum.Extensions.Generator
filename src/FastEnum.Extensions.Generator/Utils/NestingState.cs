using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Utils;

[Flags]
internal enum NestingState
{
    Namespace = 0,
    SingleParentType = 1,
    MultipleParentType = 2,
    GenericParentType = 4,
    InternalSingleParentType = 8
}

internal static class NestingStateExtensions
{
    internal static bool HasFlagFast(this NestingState value, NestingState flag) => (value & flag) != 0;

    internal static NestingState GetNesting(this ISymbol typeSymbol)
    {
        INamedTypeSymbol containingType = typeSymbol.ContainingType;

        int nestingLevel = 0;
        NestingState nestingState = NestingState.Namespace;

        while (containingType is not null && containingType.Kind != SymbolKind.Namespace)
        {
            nestingLevel++;

            if (containingType is
                {
                    Kind: SymbolKind.NamedType,
                    TypeKind: TypeKind.Interface or TypeKind.Struct or TypeKind.Class,
                    TypeArguments.IsDefaultOrEmpty: false
                })
            {
                nestingState |= NestingState.GenericParentType;
            }

            containingType = containingType.ContainingType;
        }

        if (nestingLevel == 1)
        {
            nestingState |= NestingState.SingleParentType;

            Accessibility accessibility = typeSymbol.ContainingType.DeclaredAccessibility;
            if (accessibility != Accessibility.Public)
            {
                nestingState |= NestingState.InternalSingleParentType;
            }
        }
        else if (nestingLevel > 1)
        {
            nestingState |= NestingState.MultipleParentType;
        }

        return nestingState;
    }
}

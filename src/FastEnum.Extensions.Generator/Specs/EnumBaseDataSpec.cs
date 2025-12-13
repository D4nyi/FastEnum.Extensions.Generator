using System.Collections.Immutable;

using FastEnum.Extensions.Generator.Utils;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Specs;

internal readonly struct EnumBaseDataSpec : IEquatable<EnumBaseDataSpec>
{
    internal bool HasFlags { get; }
    internal NestingState NestingState { get; }
    internal string Modifier { get; }
    internal string TypeName { get; }
    internal string UnderlyingTypeName { get; }
    internal string Namespace { get; }
    internal bool IsGlobalNamespace { get; }
    internal EnumFieldSpec[] Members { get; }
    internal Location Location { get; }

    public EnumBaseDataSpec(bool hasFlags, NestingState nestingState, string modifier, string typeName, string underlyingTypeName, string ns,
        bool isGlobalNamespace, EnumFieldSpec[] members, Location location)
    {
        HasFlags = hasFlags;
        NestingState = nestingState;
        Modifier = modifier;
        TypeName = typeName;
        UnderlyingTypeName = underlyingTypeName;
        Namespace = ns;
        IsGlobalNamespace = isGlobalNamespace;
        Members = members;
        Location = location;
    }

    public override string ToString() => TypeName;

    public static bool operator !=(EnumBaseDataSpec left, EnumBaseDataSpec right) => !(left == right);

    public static bool operator ==(EnumBaseDataSpec left, EnumBaseDataSpec right) => left.Equals(right);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = HasFlags.GetHashCode();
            hashCode = (hashCode * 397) ^ NestingState.GetHashCode();
            hashCode = (hashCode * 397) ^ Modifier.GetHashCode();
            hashCode = (hashCode * 397) ^ TypeName.GetHashCode();
            hashCode = (hashCode * 397) ^ UnderlyingTypeName.GetHashCode();
            hashCode = (hashCode * 397) ^ Namespace.GetHashCode();
            hashCode = (hashCode * 397) ^ IsGlobalNamespace.GetHashCode();
            hashCode = (hashCode * 397) ^ Members.GetHashCode();
            hashCode = (hashCode * 397) ^ Location.GetHashCode();
            return hashCode;
        }
    }

    public override bool Equals(object? obj) => obj is EnumBaseDataSpec spec && Equals(spec);

    public bool Equals(EnumBaseDataSpec other)
    {
        bool notArrayFields =
            HasFlags == other.HasFlags &&
            NestingState == other.NestingState &&
            Modifier == other.Modifier &&
            TypeName == other.TypeName &&
            UnderlyingTypeName == other.UnderlyingTypeName &&
            Namespace == other.Namespace &&
            IsGlobalNamespace == other.IsGlobalNamespace &&
            Location.Equals(other.Location);

        return notArrayFields &&
               new ObjectArraySequenceEqualityComparer<EnumFieldSpec>().Equals(Members, other.Members);
    }
}

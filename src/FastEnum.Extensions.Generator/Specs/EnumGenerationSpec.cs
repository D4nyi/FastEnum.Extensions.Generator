using System.Collections.Immutable;
using System.Diagnostics;

using FastEnum.Extensions.Generator.Emitters;
using FastEnum.Extensions.Generator.Utils;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
internal readonly struct EnumGenerationSpec : IEquatable<EnumGenerationSpec>
{
    internal string Namespace { get; }
    internal bool IsGlobalNamespace { get; }
    internal string FullName { get; }
    internal string Name { get; }
    internal string Modifier { get; }
    internal string UnderlyingType { get; }
    internal string OriginalUnderlyingType { get; }
    internal string ToStringFormat { get; }

    internal ImmutableArray<EnumMemberSpec> Members { get; }
    internal ImmutableArray<EnumMemberSpec> DistinctFlagMembers { get; }
    internal ImmutableArray<EnumMemberSpec> DistinctMembers { get; }

    internal EnumGenerationSpec(
        string fullName,
        string modifier,
        ImmutableArray<EnumMemberSpec> members,
        bool isGlobalNamespace,
        string @namespace,
        string underlyingTypeName,
        bool hasFlags)
    {
        FullName = fullName;

        int lastIndexOfDot = FullName.LastIndexOf('.');
        Name = FullName.Substring(lastIndexOfDot + 1);

        Modifier = modifier;
        Members = members;
        DistinctFlagMembers = members.ToDistinct(true);
        DistinctMembers = hasFlags ? DistinctFlagMembers : members.ToDistinct(false);

        Namespace = @namespace;
        OriginalUnderlyingType = underlyingTypeName;
        IsGlobalNamespace = isGlobalNamespace;

        if (members.IsDefaultOrEmpty)
        {
            ToStringFormat = null!;
            UnderlyingType = null!;
        }
        else
        {
            Type type = members[0].Value.GetType();

            ToStringFormat = type.GetFormat();
            UnderlyingType = "global::" + type.FullName;
        }
    }

    public override string ToString() => FullName;

    public static bool operator !=(EnumGenerationSpec left, EnumGenerationSpec right) => !(left == right);

    public static bool operator ==(EnumGenerationSpec left, EnumGenerationSpec right) => left.Equals(right);

    public override int GetHashCode()
    {
        const int multiplier = -1521134295;

        return (((((EqualityComparer<string>.Default.GetHashCode(ToStringFormat) * multiplier +
                    EqualityComparer<string>.Default.GetHashCode(FullName)) * multiplier +
                   EqualityComparer<string>.Default.GetHashCode(Modifier)) * multiplier +
                  EqualityComparer<string>.Default.GetHashCode(UnderlyingType)) * multiplier +
                 EqualityComparer<string>.Default.GetHashCode(OriginalUnderlyingType)) * multiplier +
                EqualityComparer<ImmutableArray<EnumMemberSpec>>.Default.GetHashCode(Members)) * multiplier;
    }

    public override bool Equals(object? obj) => obj is EnumGenerationSpec spec && Equals(spec);

    private static readonly ObjectImmutableArraySequenceEqualityComparer<EnumMemberSpec> _comparer = new();

    public bool Equals(EnumGenerationSpec other) =>
        EqualityComparer<string>.Default.Equals(ToStringFormat, other.ToStringFormat)
        && EqualityComparer<string>.Default.Equals(FullName, other.FullName)
        && EqualityComparer<string>.Default.Equals(Modifier, other.Modifier)
        && EqualityComparer<string>.Default.Equals(UnderlyingType, other.UnderlyingType)
        && EqualityComparer<string>.Default.Equals(OriginalUnderlyingType, other.OriginalUnderlyingType)
        && _comparer.Equals(Members, other.Members);
}

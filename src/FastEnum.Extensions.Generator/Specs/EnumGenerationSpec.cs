using System.Collections.Immutable;
using System.Diagnostics;

using FastEnum.Extensions.Generator.Utils;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
internal readonly struct EnumGenerationSpec
{
    private const string GlobalNamespace = "<global namespace>";

    internal Type Type { get; }
    internal string Namespace { get; }
    internal bool IsGlobalNamespace { get; }
    internal string FullName { get; }
    internal string Name { get; }
    internal string Modifier { get; }
    internal string UnderlyingType { get; }
    internal string OriginalUnderlyingType { get; }

    internal ImmutableArray<EnumMemberSpec> Members { get; }
    internal ImmutableArray<EnumMemberSpec> DistinctFlagMembers { get; }
    internal ImmutableArray<EnumMemberSpec> DistinctMembers { get; }

    internal EnumGenerationSpec(
        string fullName,
        string modifier,
        ImmutableArray<EnumMemberSpec> members,
        string @namespace,
        string underlyingTypeName,
        bool hasFlags)
    {
        FullName = fullName;
        Modifier = modifier;

        Members = members;
        DistinctFlagMembers = members.ToDistinct(true);
        DistinctMembers = hasFlags ? DistinctFlagMembers : members.ToDistinct(false);

        Namespace = @namespace;
        OriginalUnderlyingType = underlyingTypeName;
        IsGlobalNamespace = Namespace == GlobalNamespace;

        if (members.IsEmpty)
        {
            Type = null!;
            UnderlyingType = null!;
        }
        else
        {
            Type = members[0].Value.GetType();
            UnderlyingType = "global::" + Type.FullName;
        }

        int lastIndexOfDot = FullName.LastIndexOf('.');
        Name = FullName.Substring(lastIndexOfDot + 1);
    }

    public override string ToString() => FullName;
}

using System.Collections.Immutable;
using System.Diagnostics;

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

    internal EnumGenerationSpec(
        string fullName,
        string modifier,
        ImmutableArray<EnumMemberSpec> members,
        string @namespace,
        string underlyingTypeName)
    {
        FullName = fullName;
        Modifier = modifier;
        Members = members;
        Namespace = @namespace;
        OriginalUnderlyingType = underlyingTypeName;
        IsGlobalNamespace = Namespace == GlobalNamespace;

        Type = members[0].Value.GetType();
        UnderlyingType = "global::" + Type.FullName;

        int lastIndexOfDot = FullName.LastIndexOf('.');
        Name = FullName.Substring(lastIndexOfDot + 1);
    }

    public override string ToString() => FullName;
}

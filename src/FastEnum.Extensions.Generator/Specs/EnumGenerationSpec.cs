using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

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
    internal bool IsFlags { get; }
    internal int AverageMemberLength { get; }
    internal ImmutableArray<EnumMemberSpec> Members { get; }

    internal EnumGenerationSpec(
        string fullName,
        string modifier,
        ImmutableArray<EnumMemberSpec> members,
        string @namespace,
        string underlyingTypeName,
        bool isFlags) 
    {
        FullName = fullName;
        Modifier = modifier;
        Members = members;
        Namespace = @namespace;
        OriginalUnderlyingType = underlyingTypeName;
        IsFlags = isFlags;
        IsGlobalNamespace = Namespace == GlobalNamespace;

        Type = members[0].Value.GetType();
        UnderlyingType = "global::" + Type.FullName;
        // GetGeneratorFriendlyTypeName(Type);

        int lastIndexOfDot = FullName.LastIndexOf('.');
        Name = FullName.Substring(lastIndexOfDot + 1);

        AverageMemberLength = (int)Math.Round(members.Average(x => x.MemberLength));
    }

    private static void GetGeneratorFriendlyTypeName(Type underlyingType)
    {
        // byte, sbyte, short, ushort, int, uint, long, or ulong

        string underlyingTypeName = underlyingType.Name switch
        {
            nameof(Byte) => "byte",
            nameof(SByte) => "sbyte",
            nameof(Int16) => "short",
            nameof(UInt16) => "ushort",
            nameof(Int32) => "int",
            nameof(UInt32) => "uint",
            nameof(Int64) => "long",
            nameof(UInt64) => "ulong",
            _ => throw new UnreachableException("Unknown enum backing type!")
        };
    }

    public override string ToString() => FullName;
}

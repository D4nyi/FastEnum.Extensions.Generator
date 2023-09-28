using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
internal readonly struct EnumGenerationSpec
{
    private const string GlobalNamespace = "<global namespace>";

    internal string Namespace { get; }
    internal bool IsGlobalNamespace { get; }
    internal string FullName { get; }
    internal string Name { get; }
    internal string CommentCompatibleFullName { get; }
    internal string Modifier { get; }
    internal string GenericTypeParameters { get; }
    internal string GenericTypeConstraints { get; }
    internal int DefaultValue { get; }
    internal string UnderlyingType { get; }
    internal string OriginalUnderlyingType { get; }
    internal bool IsFlags { get; }
    internal List<EnumMemberSpec> Members { get; }

    internal EnumGenerationSpec(
        string fullName,
        string modifier,
        string genericTypeParameters,
        string genericTypeConstraints,
        int defaultValue,
        List<EnumMemberSpec> members,
        string @namespace,
        string underlyingTypeName,
        bool isFlags)
    {
        FullName = fullName;
        Modifier = modifier;
        GenericTypeParameters = genericTypeParameters;
        GenericTypeConstraints = genericTypeConstraints;
        DefaultValue = defaultValue;
        Members = members;
        Namespace = @namespace;
        UnderlyingType = GetGeneratorFriendlyTypeName(underlyingTypeName);
        OriginalUnderlyingType = underlyingTypeName;
        IsFlags = isFlags;

        IsGlobalNamespace = Namespace == GlobalNamespace;

        int lastIndexOfDot = FullName.LastIndexOf('.');
        Name = FullName.Substring(lastIndexOfDot + 1);

        if (GenericTypeParameters.Length != 0)
        {
            CommentCompatibleFullName = FullName
                .Replace('<', '{')
                .Replace('>', '}');
        }
        else
        {
            CommentCompatibleFullName = FullName;
        }
    }

    private static string GetGeneratorFriendlyTypeName(string underlyingTypeName)
    {
        // byte, sbyte, short, ushort, int, uint, long, or ulong

        return underlyingTypeName switch
        {
            "byte" or nameof(Byte) => "global::System." + nameof(Byte),
            "sbyte" or nameof(SByte) => "global::System." + nameof(SByte),
            "short" or nameof(Int16) => "global::System." + nameof(Int16),
            "ushort" or nameof(UInt16) => "global::System." + nameof(UInt16),
            "int" or nameof(Int32) => "global::System." + nameof(Int32),
            "uint" or nameof(UInt32) => "global::System." + nameof(UInt32),
            "long" or nameof(Int64) => "global::System." + nameof(Int64),
            "ulong" or nameof(UInt64) => "global::System." + nameof(UInt64),
            string a when a.Contains("System") => GetTypePart(underlyingTypeName),
            _ => underlyingTypeName
        };
    }

    private static string GetTypePart(string type)
    {
        const string system = "System";

        int idx = type.IndexOf(system, StringComparison.Ordinal);
        string part = type.Substring(idx + system.Length + 1); // +1 so the dot will also be removed
        string result = GetGeneratorFriendlyTypeName(part);

        // 
        return part == result
            ? type
            : result;
    }

    private string GetDebuggerDisplay()
    {
        return FullName;
    }
}
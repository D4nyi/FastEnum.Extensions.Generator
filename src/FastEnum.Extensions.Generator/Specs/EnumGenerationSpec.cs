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

    //public bool HasFlags { get; }
    public EnumOrderSpec Order { get; }
    internal string ToStringFormat { get; }

    internal EnumMemberSpec[] Members { get; }
    internal EnumMemberSpec[] DistinctFlagMembers { get; }
    internal EnumMemberSpec[] DistinctMembers { get; }

    internal EnumGenerationSpec(string fullName,
        string modifier,
        EnumMemberSpec[] members,
        bool isGlobalNamespace,
        string @namespace,
        string underlyingTypeName,
        bool hasFlags,
        EnumOrderSpec order)
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
        //HasFlags = hasFlags;
        Order = order;
        IsGlobalNamespace = isGlobalNamespace;

        if (members.IsEmpty)
        {
            ToStringFormat = null!;
            UnderlyingType = null!;
        }
        else
        {
            Type type = members[0].UnderlyingType;

            ToStringFormat = type.GetFormat();
            UnderlyingType = "global::" + type.FullName;
        }
    }

    public override string ToString() => FullName;

    public static bool operator !=(EnumGenerationSpec left, EnumGenerationSpec right) => !(left == right);

    public static bool operator ==(EnumGenerationSpec left, EnumGenerationSpec right) => left.Equals(right);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = FullName.GetHashCode();
            hashCode = (hashCode * 397) ^ Modifier.GetHashCode();
            hashCode = (hashCode * 397) ^ UnderlyingType.GetHashCode();
            hashCode = (hashCode * 397) ^ ToStringFormat.GetHashCode();
            //hashCode = (hashCode * 397) ^ HasFlags.GetHashCode();
            hashCode = (hashCode * 397) ^ Members.GetHashCode();
            return hashCode;
        }
    }

    public override bool Equals(object? obj) => obj is EnumGenerationSpec spec && Equals(spec);

#pragma warning disable CA1307
#pragma warning disable CA1309 // No need for checking cultures and casing
    public bool Equals(EnumGenerationSpec other)
    {
        bool notArrayFields =
            FullName.Equals(other.FullName) &&
            Modifier.Equals(other.Modifier) &&
            UnderlyingType.Equals(other.UnderlyingType) &&
            ToStringFormat.Equals(other.ToStringFormat);
        // HasFlags.Equals(other.HasFlags);

        return notArrayFields &&
               new ObjectArraySequenceEqualityComparer<EnumMemberSpec>().Equals(Members, other.Members);
    }
#pragma warning restore CA1309
#pragma warning restore CA1307
}

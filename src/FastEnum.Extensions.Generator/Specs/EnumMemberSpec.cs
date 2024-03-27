using System;
using System.Diagnostics;
using System.Globalization;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
internal readonly struct EnumMemberSpec
{
    internal int MemberLength { get; }
    internal string FullName { get; }
    internal object Value { get; }
    internal string? Description { get; }

    internal EnumMemberSpec(string typeName, string name, object value, string? description)
    {
        FullName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", typeName, name);
        Value = value;
        MemberLength = name.Length;
        Description = description;
    }

    public override string ToString() => $"{FullName}({Value})";
}

using System.Diagnostics;
using System.Globalization;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
internal readonly struct EnumMemberSpec
{
    internal string FullName { get; }
    internal object Value { get; }
    internal AttributeValues Data { get; }

    internal EnumMemberSpec(string typeName, string name, object value, AttributeValues data)
    {
        FullName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", typeName, name);
        Value = value;
        Data = data;
    }

    public override string ToString() => $"{FullName}({Value})";
}

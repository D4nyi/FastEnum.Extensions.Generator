using System.Diagnostics;
using System.Globalization;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
internal readonly struct EnumMemberSpec : IEquatable<EnumMemberSpec>
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

    public static bool operator !=(EnumMemberSpec left, EnumMemberSpec right) => !(left == right);

    public static bool operator ==(EnumMemberSpec left, EnumMemberSpec right) => left.Equals(right);

    public override int GetHashCode() => EqualityComparer<object>.Default.GetHashCode(Value);

    public override bool Equals(object? obj) => obj is EnumMemberSpec spec && Equals(spec);

    public bool Equals(EnumMemberSpec other) => EqualityComparer<object>.Default.Equals(Value, other.Value);
}

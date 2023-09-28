using System.Diagnostics;

namespace FastEnum.Extensions.Generator.Specs;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
internal readonly struct EnumMemberSpec
{
    internal string Name { get; }

    internal string Value { get; }

    internal EnumMemberSpec(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString() => $"{Name}::{Value}";
}

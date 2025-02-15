using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Specs;

internal readonly struct AttributeInternalsSpec : IEquatable<AttributeInternalsSpec>
{
    internal string? MetadataName { get; }
    internal Dictionary<string, TypedConstant> NamedArguments { get; }
    internal object? ConstructorArgument { get; }

    internal AttributeInternalsSpec(string? metadataName, Dictionary<string, TypedConstant> namedArguments, object? constructorArgument)
    {
        MetadataName = metadataName;
        NamedArguments = namedArguments;
        ConstructorArgument = constructorArgument;
    }

    public static bool operator ==(AttributeInternalsSpec left, AttributeInternalsSpec right) => left.Equals(right);

    public static bool operator !=(AttributeInternalsSpec left, AttributeInternalsSpec right) => !left.Equals(right);

    public bool Equals(AttributeInternalsSpec other)
    {
        return
            MetadataName == other.MetadataName &&
            Equals(ConstructorArgument, other.ConstructorArgument) &&
            DictionaryEqual(NamedArguments, other.NamedArguments);

        [MethodImpl(MethodImplOptions.NoInlining)]
        static bool DictionaryEqual(Dictionary<string, TypedConstant> first, Dictionary<string, TypedConstant> second)
        {
            if (ReferenceEquals(first, second)) return true;
            if (first.Count != second.Count) return false;

            foreach (KeyValuePair<string, TypedConstant> kvp in first)
            {
                if (!second.TryGetValue(kvp.Key, out TypedConstant secondValue)) return false;
                if (!kvp.Value.Equals(secondValue)) return false;
            }
            return true;
        }
    }

    public override bool Equals(object? obj) => obj is AttributeInternalsSpec other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = MetadataName?.GetHashCode() ?? 0;
            hashCode = (hashCode * 397) ^ NamedArguments.GetHashCode();
            hashCode = (hashCode * 397) ^ (ConstructorArgument?.GetHashCode() ?? 0);
            return hashCode;
        }
    }
}


internal readonly struct EnumFieldSpec : IEquatable<EnumFieldSpec>
{
    internal string Name { get; }
    internal object Value { get; }
    internal AttributeInternalsSpec[] AttributesData { get; }

    internal EnumFieldSpec(string name, object value, AttributeInternalsSpec[] attributesData)
    {
        Name = name;
        Value = value;
        AttributesData = attributesData;
    }

    public static bool operator ==(EnumFieldSpec left, EnumFieldSpec right) => left.Equals(right);

    public static bool operator !=(EnumFieldSpec left, EnumFieldSpec right) => !left.Equals(right);

    public bool Equals(EnumFieldSpec other)
    {
        return Name == other.Name
               && Value.Equals(other.Value)
               && AttributesData.SequenceEqual(other.AttributesData);
    }

    public override bool Equals(object? obj) => obj is EnumFieldSpec other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ Value.GetHashCode();
            hashCode = (hashCode * 397) ^ AttributesData.GetHashCode();
            return hashCode;
        }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class EnumMemberValueDataGenerator : AttributeDataGenerator<EnumMemberAttribute>
{
    public EnumMemberValueDataGenerator() : base(x => x?.Value)
    {
    }
}

internal sealed class DisplayNameDataGenerator : AttributeDataGenerator<DisplayAttribute>
{
    public DisplayNameDataGenerator() : base(x => x?.Name)
    {
    }
}

internal sealed class DisplayDescriptionDataGenerator : AttributeDataGenerator<DisplayAttribute>
{
    public DisplayDescriptionDataGenerator() : base(x => x?.Description)
    {
    }
}

internal sealed class DescriptionDataGenerators : AttributeDataGenerator<DescriptionAttribute>
{
    public DescriptionDataGenerators() : base(x => x?.Description)
    {
    }
}

internal abstract class AttributeDataGenerator<T> : TheoryData<Color, string?> where T : Attribute
{
    private static readonly Type _colorType = typeof(Color);
    private static readonly Color[] _testValues = [Color.Red, Color.Green, Color.Black, Color.Blue, (Color)15];

    protected AttributeDataGenerator(Func<T?, string?> accessor)
    {
        foreach (Color color in _testValues)
        {
            T? attribute = GetAttribute(color);

            Add(color, accessor(attribute));
        }
    }

    private static T? GetAttribute(Color color)
    {
        T[]? attributes = _colorType
            .GetField(color.ToString())
            ?.GetCustomAttributes(typeof(T), false) as T[];

        return attributes is { Length: > 0} ? attributes[0] : null;
    }
}

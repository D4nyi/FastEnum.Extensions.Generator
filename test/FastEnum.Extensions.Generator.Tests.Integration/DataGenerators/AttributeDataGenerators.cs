using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class EnumMemberValueDataGenerator : AttributeDataGenerator<EnumMemberAttribute>
{
    public EnumMemberValueDataGenerator() : base(x => x.Value)
    {
    }
}

internal sealed class DisplayNameDataGenerator : AttributeDataGenerator<DisplayAttribute>
{
    public DisplayNameDataGenerator() : base(x => x.Name)
    {
    }
}

internal sealed class DisplayDescriptionDataGenerator : AttributeDataGenerator<DisplayAttribute>
{
    public DisplayDescriptionDataGenerator() : base(x => x.Description)
    {
    }
}

internal sealed class DescriptionDataGenerators : AttributeDataGenerator<DescriptionAttribute>
{
    public DescriptionDataGenerators() : base(x => x.Description)
    {
    }
}

internal abstract class AttributeDataGenerator<T> : IEnumerable<object?[]> where T : Attribute
{
    private static readonly Type _colorType = typeof(Color);
    private static readonly Color[] _testValues = [Color.Red, Color.Green, Color.Blue, (Color)15, 0];

    private readonly List<object?[]> _testData;

    private readonly Func<T, string?> _accessor;

    protected AttributeDataGenerator(Func<T, string?> accessor)
    {
        _accessor = accessor;
        _testData = _testValues.Select(GetAttributeData).ToList();
    }

    public IEnumerator<object?[]> GetEnumerator() => _testData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private object?[] GetAttributeData(Color color)
    {
        T[]? attributes = _colorType
            .GetField(color.ToString())
            ?.GetCustomAttributes(typeof(T), false) as T[];

        object?[] arr = new object?[2];
        arr[0] = color;

        if (attributes is { Length: > 0 })
        {
            arr[1] = _accessor(attributes[0]);
        }

        return arr;
    }
}

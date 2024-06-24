using System.Collections;
using System.ComponentModel;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class DescriptionDataGenerator : IEnumerable<object?[]>
{
    private static readonly Type _colorType = typeof(Color);

    private static readonly List<object?[]> _testData =
        new[] { Color.Red, Color.Green, Color.Blue, (Color)15, (Color)0 }
            .Select(GetEnumDescription).ToList();

    public IEnumerator<object?[]> GetEnumerator() => _testData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static object?[] GetEnumDescription(Color color)
    {
        var attributes = _colorType
            .GetField(color.ToString())
            ?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes is { Length: > 0 })
        {
            return new object?[] { color, attributes[0].Description };
        }

        return new object?[] { color, null };
    }
}

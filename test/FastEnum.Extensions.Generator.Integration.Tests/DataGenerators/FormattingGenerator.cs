using System.Collections;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class FormattingGenerator : IEnumerable<object?[]>
{
    private static readonly List<object?[]> _testData;

    static FormattingGenerator()
    {
        var colors = new[] { Color.Red, Color.Green, Color.Blue, (Color)15, (Color)0 };
        var formats = new[] { "G", "g", "D", "d", "X", "x", "F", "f" };

        _testData = colors
            .SelectMany(color => formats.Select(format => new object?[] { color, format }))
            .ToList();
    }

    public IEnumerator<object?[]> GetEnumerator() => _testData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using System.Collections;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class DefaultGenerator : IEnumerable<object?[]>
{
    private static readonly List<object?[]> _testData = new()
    {
        new object?[] { Color.Red },
        new object?[] { Color.Blue },
        new object?[] { Color.Green },
        new object?[] { (Color)15 },
        new object?[] { (Color)0 },
    };

    public IEnumerator<object?[]> GetEnumerator() => _testData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

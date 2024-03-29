using System.Collections;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class EnumStringGenerator : IEnumerable<object?[]>
{
    private static readonly List<object?[]> _testData = new()
    {
        new object?[] { nameof(Color.Red), Color.Red },
        new object?[] { nameof(Color.Green), Color.Green },
        new object?[] { nameof(Color.Blue), Color.Blue },
        new object?[] { (Color.Red | Color.Blue).ToString(), Color.Red | Color.Blue },
        new object?[] { (Color.Red | Color.Green).ToString(), Color.Red | Color.Green },
        new object?[] { (Color.Blue | Color.Green).ToString(), Color.Blue | Color.Green },
        new object?[] { "1", Color.Red },
        new object?[] { "2", Color.Green },
        new object?[] { "4", Color.Blue },
        new object?[] { "3", Color.Red | Color.Green },
        new object?[] { "5", Color.Red | Color.Blue },
        new object?[] { "6", Color.Blue | Color.Green },
        new object?[] { "15", (Color)15 },
    };

    public IEnumerator<object?[]> GetEnumerator() => _testData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

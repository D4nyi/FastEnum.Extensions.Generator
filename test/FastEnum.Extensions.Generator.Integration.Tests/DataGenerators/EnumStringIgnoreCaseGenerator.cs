using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

[SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase")]
[SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
internal sealed class EnumStringIgnoreCaseGenerator : IEnumerable<object?[]>
{
    private static readonly List<object?[]> _testData = new()
    {
        new object?[] { nameof(Color.Red).ToLowerInvariant(), Color.Red },
        new object?[] { nameof(Color.Green).ToLowerInvariant(), Color.Green },
        new object?[] { nameof(Color.Blue).ToLowerInvariant(), Color.Blue },
        new object?[] { (Color.Red | Color.Blue).ToString().ToLowerInvariant(), Color.Red | Color.Blue },
        new object?[] { (Color.Red | Color.Green).ToString().ToLowerInvariant(), Color.Red | Color.Green },
        new object?[] { (Color.Blue | Color.Green).ToString().ToLowerInvariant(), Color.Blue | Color.Green },

        new object?[] { nameof(Color.Red).ToUpperInvariant(), Color.Red },
        new object?[] { nameof(Color.Green).ToUpperInvariant(), Color.Green },
        new object?[] { nameof(Color.Blue).ToUpperInvariant(), Color.Blue },
        new object?[] { (Color.Red | Color.Blue).ToString().ToUpperInvariant(), Color.Red | Color.Blue },
        new object?[] { (Color.Red | Color.Green).ToString().ToUpperInvariant(), Color.Red | Color.Green },
        new object?[] { (Color.Blue | Color.Green).ToString().ToUpperInvariant(), Color.Blue | Color.Green },

        new object?[] { "1", Color.Blue },
        new object?[] { "2", Color.Green },
        new object?[] { "4", Color.Red },
        new object?[] { "3", Color.Blue | Color.Green },
        new object?[] { "5", Color.Red | Color.Blue },
        new object?[] { "6", Color.Red | Color.Green },
        new object?[] { "15", (Color)15 },
    };

    public IEnumerator<object?[]> GetEnumerator() => _testData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

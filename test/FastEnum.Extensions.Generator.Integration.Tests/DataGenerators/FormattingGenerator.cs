using System.Collections;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class FormattingGenerator : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return [Color.Red, "G"];
        yield return [Color.Blue, "G"];
        yield return [Color.Green, "G"];
        yield return [(Color)15, "G"];
        yield return [(Color)0, "G"];
        
        // ---------------------------

        yield return [Color.Red, "D"];
        yield return [Color.Blue, "D"];
        yield return [Color.Green, "D"];
        yield return [(Color)15, "D"];
        yield return [(Color)0, "D"];
        
        // ---------------------------

        yield return [Color.Red, "X"];
        yield return [Color.Blue, "X"];
        yield return [Color.Green, "X"];
        yield return [(Color)15, "X"];
        yield return [(Color)0, "X"];
        
        // ---------------------------
        
        yield return [Color.Red, "F"];
        yield return [Color.Blue, "F"];
        yield return [Color.Green, "F"];
        yield return [(Color)15, "F"];
        yield return [(Color)0, "F"];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

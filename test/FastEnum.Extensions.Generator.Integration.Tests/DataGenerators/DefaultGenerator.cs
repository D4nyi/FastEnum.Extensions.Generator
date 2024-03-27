using System.Collections;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class DefaultGenerator : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return [Color.Red];
        yield return [Color.Blue];
        yield return [Color.Green];
        yield return [(Color)15];
        yield return [(Color)0];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

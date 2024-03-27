using System.Collections;

namespace FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

internal sealed class EnumStringGenerator : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return [nameof(Color.Red), Color.Red];
        yield return [nameof(Color.Green), Color.Green];
        yield return [nameof(Color.Blue), Color.Blue];
        yield return [(Color.Red | Color.Blue).ToString(), Color.Red | Color.Blue];
        yield return [(Color.Red | Color.Green).ToString(), Color.Red | Color.Green];
        yield return [(Color.Blue | Color.Green).ToString(), Color.Blue | Color.Green];
        yield return ["1", Color.Red];
        yield return ["2", Color.Green];
        yield return ["4", Color.Blue];
        yield return ["3", Color.Red | Color.Green];
        yield return ["5", Color.Red | Color.Blue];
        yield return ["6", Color.Blue | Color.Green];
        yield return ["15", (Color)15];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

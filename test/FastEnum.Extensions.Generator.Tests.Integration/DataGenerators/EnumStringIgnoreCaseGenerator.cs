using System.Diagnostics.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

[SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase")]
internal sealed class EnumStringIgnoreCaseGenerator : TheoryData<string, Color>
{
    public EnumStringIgnoreCaseGenerator()
    {
        Add(nameof(Color.Red).ToLowerInvariant(), Color.Red);
        Add(nameof(Color.Green).ToLowerInvariant(), Color.Green);
        Add(nameof(Color.Blue).ToLowerInvariant(), Color.Blue);
        Add(nameof(Color.Black).ToLowerInvariant(), Color.Black);
        Add((Color.Red | Color.Blue).ToString().ToLowerInvariant(), Color.Red | Color.Blue);
        Add((Color.Red | Color.Green).ToString().ToLowerInvariant(), Color.Red | Color.Green);
        Add((Color.Blue | Color.Green).ToString().ToLowerInvariant(), Color.Blue | Color.Green);

        Add(nameof(Color.Red).ToUpperInvariant(), Color.Red);
        Add(nameof(Color.Green).ToUpperInvariant(), Color.Green);
        Add(nameof(Color.Blue).ToUpperInvariant(), Color.Blue);
        Add((Color.Red | Color.Blue).ToString().ToUpperInvariant(), Color.Red | Color.Blue);
        Add((Color.Red | Color.Green).ToString().ToUpperInvariant(), Color.Red | Color.Green);
        Add((Color.Blue | Color.Green).ToString().ToUpperInvariant(), Color.Blue | Color.Green);

        Add("0", Color.Black);
        Add("1", Color.Blue);
        Add("2", Color.Green);
        Add("4", Color.Red);
        Add("3", Color.Blue | Color.Green);
        Add("5", Color.Red | Color.Blue);
        Add("6", Color.Red | Color.Green);
        Add("15", (Color)15);
    }
}

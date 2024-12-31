namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class ColorFormattingGenerator : TheoryData<Color, string?>
{
    public ColorFormattingGenerator()
    {
        IEnumerable<Color> colors = ColorExtensions.GetValues().Append((Color)15);
        string[] formats = ["G", "g", "D", "d", "X", "x", "F", "f"];

        foreach (Color color in colors)
        {
            foreach (string format in formats)
            {
                Add(color, format);
            }

            Add(color, "");
            Add(color, null);
        }

        Add(Color.Red | Color.Green, "F");
        Add(Color.Red | Color.Blue, "f");
        Add(Color.Green | Color.Blue, "F");
    }
}

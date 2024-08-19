namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class FormattingGenerator : TheoryData<Color, string>
{
    public FormattingGenerator()
    {
        Color[] colors = [Color.Red, Color.Green, Color.Blue, (Color)15, 0];
        string[] formats = ["G", "g", "D", "d", "X", "x", "F", "f"];

        foreach (Color color in colors)
        {
            foreach (string format in formats)
            {
                Add(color, format);
            }
        }
    }
}

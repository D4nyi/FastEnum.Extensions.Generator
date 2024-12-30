namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class ColorStringGenerator : TheoryData<string, Color>
{
    public ColorStringGenerator()
    {
        Add(nameof(Color.Red), Color.Red);
        Add(nameof(Color.Green), Color.Green);
        Add(nameof(Color.Blue), Color.Blue);
        Add(nameof(Color.Black), Color.Black);
        Add((Color.Red | Color.Blue).ToString(), Color.Red | Color.Blue);
        Add((Color.Red | Color.Green).ToString(), Color.Red | Color.Green);
        Add((Color.Blue | Color.Green).ToString(), Color.Blue | Color.Green);
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

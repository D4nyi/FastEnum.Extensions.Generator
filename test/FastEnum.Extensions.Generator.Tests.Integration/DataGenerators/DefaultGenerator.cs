namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class DefaultGenerator : TheoryData<Color>
{
    public DefaultGenerator()
    {
        Add(Color.Red);
        Add(Color.Blue);
        Add(Color.Green);
        Add((Color)15);
        Add(0);
    }
}

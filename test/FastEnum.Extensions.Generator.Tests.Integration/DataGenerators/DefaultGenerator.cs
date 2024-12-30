namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class DefaultGenerator : TheoryData<short>
{
    public DefaultGenerator()
    {
        foreach (short value in Constants.TestValues)
        {
            Add(value);
        }
    }
}

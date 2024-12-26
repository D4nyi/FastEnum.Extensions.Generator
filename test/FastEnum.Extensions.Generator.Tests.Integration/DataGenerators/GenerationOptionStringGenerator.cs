namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class GenerationOptionStringGenerator : TheoryData<string, GenerationOption>
{
    public GenerationOptionStringGenerator()
    {
        Add(nameof(GenerationOption.ToString), GenerationOption.ToString);
        //Add(nameof(GenerationOption.ToStringFormat), GenerationOption.ToString);
        Add(nameof(GenerationOption.Parse), GenerationOption.Parse);
        Add((GenerationOption.ToString | GenerationOption.Parse).ToString(), GenerationOption.ToString | GenerationOption.Parse);
        //Add((GenerationOption.ToString | GenerationOption.ToStringFormat).ToString(), GenerationOption.ToString);
        Add("0", GenerationOption.None);
        Add("1", GenerationOption.ToString);
        Add("2", GenerationOption.Parse);
        Add("15", (GenerationOption)15);
    }
}

using System.Diagnostics.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

[SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase")]
internal sealed class GenerationOptionStringIgnoreCaseGenerator : TheoryData<string, GenerationOption>
{
    public GenerationOptionStringIgnoreCaseGenerator()
    {
        Add(nameof(GenerationOption.ToString).ToLowerInvariant(), GenerationOption.ToString);
        //Add(nameof(GenerationOption.ToStringFormat).ToLowerInvariant(), GenerationOption.ToString);
        Add(nameof(GenerationOption.Parse).ToLowerInvariant(), GenerationOption.Parse);
        Add((GenerationOption.ToString | GenerationOption.Parse).ToString().ToLowerInvariant(), GenerationOption.ToString | GenerationOption.Parse);
        //Add((GenerationOption.ToString | GenerationOption.ToStringFormat).ToString().ToLowerInvariant(), GenerationOption.ToString);
        Add("0", GenerationOption.None);
        Add("1", GenerationOption.ToString);
        Add("2", GenerationOption.Parse);
        Add("15", (GenerationOption)15);
    }
}

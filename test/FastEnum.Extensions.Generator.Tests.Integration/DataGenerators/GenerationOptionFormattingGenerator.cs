namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class GenerationOptionFormattingGenerator : TheoryData<GenerationOption, string?>
{
    public GenerationOptionFormattingGenerator()
    {
        IEnumerable<GenerationOption> options = GenerationOptionExtensions.GetValues().Append((GenerationOption)15);
        string[] formats = ["G", "g", "D", "d", "X", "x", "F", "f"];

        foreach (GenerationOption option in options)
        {
            foreach (string format in formats)
            {
                Add(option, format);
            }

            Add(option, "");
            Add(option, null);
        }

        Add(GenerationOption.ToString | GenerationOption.Parse, "F");
        Add(GenerationOption.ToStringFormat | GenerationOption.Parse, "f");
    }
}

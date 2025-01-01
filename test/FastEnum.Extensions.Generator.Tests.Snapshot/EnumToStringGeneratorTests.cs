using FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

using Microsoft.CodeAnalysis;

using Xunit.Abstractions;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public sealed class EnumToStringGeneratorTests
{
    private readonly ITestOutputHelper _output;

    public EnumToStringGeneratorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // Arrange
        Dictionary<string, string> enums = SnapshotEnumGenerator.Enums();
        GeneratorDriver generator = Setups.CreateGeneratorDriver(enums, _output);

        // Act
        GeneratorDriverRunResult result = generator.GetRunResult();

        // Assert
        Assert.Single(result.Results, assertedResult =>
        {
            Assert.Equal(enums.ExpectedSourceCount(), assertedResult.GeneratedSources.Length);

            return true;
        });

        return Verify(result, Setups.VerifySettings);
    }
}

using FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public sealed class EnumToStringGeneratorTests
{
    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // Arrange
        Dictionary<string, string> enums = SnapshotEnumGenerator.Enums();
        GeneratorDriver generator = Setups.CreateGeneratorDriver(enums);

        // Act
        GeneratorDriverRunResult result = generator.GetRunResult();

        // Assert
        Assert.Single(result.Results, assertedResult => assertedResult.HasCorrectSourceCount(enums.Count));

        return Verify(result, Setups.VerifySettings);
    }
}

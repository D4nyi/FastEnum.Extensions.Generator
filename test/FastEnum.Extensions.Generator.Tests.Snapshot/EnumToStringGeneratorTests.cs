using FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public sealed class EnumToStringGeneratorTests
{
    [Fact]
    public async Task Snapshots()
    {
        // Arrange
        Dictionary<string, string> enums = SnapshotEnumGenerator.Enums();
        int expectedSourceCount = enums.ExpectedSourceCount();

        GeneratorDriver driver = Setups.CreateGeneratorDriver();
        CSharpCompilation compilation = Setups.CreateCompilation(enums);
        CSharpCompilation clone = compilation.Clone();

        // Act
        driver = driver.RunGenerators(compilation);

        GeneratorDriverRunResult runResult = driver.GetRunResult();

        GeneratorDriverRunResult runResult2 = driver.RunGenerators(clone).GetRunResult();

        // Assert
        await Verify(runResult, Setups.VerifySettings);

        Assert.Single(runResult.Results, x => AssertRunResult(expectedSourceCount, x));
        Assert.Single(runResult2.Results, x => AssertRunResult(expectedSourceCount, x));

        GeneratorInternalsTestHelper.AssertRunsEqual(runResult, runResult2);
    }

    private static bool AssertRunResult(int expectedSourceCount, GeneratorRunResult assertedResult)
    {
        Assert.Equal(expectedSourceCount, assertedResult.GeneratedSources.Length);

        return true;
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

using FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

using Microsoft.CodeAnalysis;

using Xunit.Abstractions;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public sealed class EnumToStringGeneratorTests
{
    [Fact]
    public async Task Log()
    {
        await using FileStream fs = File.OpenRead(
            "/home/runner/work/FastEnum.Extensions.Generator/FastEnum.Extensions.Generator/test/" +
            $"FastEnum.Extensions.Generator.Tests.Snapshot/bin/Debug/output-{Random.Shared.Next()}.txt");

        await fs.WriteAsync(Encoding.UTF8.GetBytes(typeof(object).Assembly.Location));
        await fs.WriteAsync(Encoding.UTF8.GetBytes(typeof(DescriptionAttribute).Assembly.Location));
        await fs.WriteAsync(Encoding.UTF8.GetBytes(typeof(EnumMemberAttribute).Assembly.Location));
        await fs.WriteAsync(Encoding.UTF8.GetBytes(typeof(DisplayAttribute).Assembly.Location));
    }

    // [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // Arrange
        Dictionary<string, string> enums = SnapshotEnumGenerator.Enums();
        GeneratorDriver generator = Setups.CreateGeneratorDriver(enums);

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

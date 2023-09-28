namespace FastEnumToString.Tests;

[UsesVerify]
public sealed class EnumToStringGeneratorTests
{
    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // The source code to test
        const string source =
"""
namespace SnapshotTesting
{
    [FastEnum.Extensions]
    public enum Color
    {
        Red = 0,
        Green = 1
        Blue = 2,
    }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}

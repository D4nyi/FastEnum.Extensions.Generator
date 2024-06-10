namespace FastEnum.Extensions.Generator.Tests;

public sealed class EnumToStringGeneratorTests
{
    // The source code to test
    private const string Source =
/*lang=csharp*/
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

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(Source);
    }
}

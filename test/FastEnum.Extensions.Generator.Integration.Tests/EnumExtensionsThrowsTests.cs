namespace FastEnum.Extensions.Generator.IntegrationTests;

public sealed class EnumExtensionsThrowsTests
{
    [Theory]
    [InlineData("XX", Color.Blue)]
    [InlineData("A", Color.Green)]
    public void Extension_FastToStringFormat_ThrowsWhenFormatIsNotOneChar(string format, Color color)
    {
        // Act, Assert
        var expected = Assert.Throws<FormatException>(() => color.ToString(format));
        var actual = Assert.Throws<FormatException>(() => color.FastToString(format));

        Assert.Equal(expected.Message, actual.Message);
    }
}

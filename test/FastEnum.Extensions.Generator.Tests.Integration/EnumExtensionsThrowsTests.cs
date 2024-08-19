namespace FastEnum.Extensions.Generator.Tests.Integration;

public sealed class EnumExtensionsThrowsTests
{
    [Theory]
    [InlineData("XX", Color.Blue)]
    [InlineData("A", Color.Green)]
    public void Extension_FastToStringFormat_ThrowsWhenFormatIsNotOneChar(string format, Color color)
    {
        // Act, Assert
        FormatException expected = Assert.Throws<FormatException>(() => color.ToString(format));
        FormatException actual = Assert.Throws<FormatException>(() => color.FastToString(format));

        Assert.Equal(expected.Message, actual.Message);
    }
}

using System.Globalization;
using FastEnum.Extensions.Generator.IntegrationTests.DataGenerators;

namespace FastEnum.Extensions.Generator.IntegrationTests;

public sealed class EnumExtensionsHappyCaseTests
{
    [Theory]
    [ClassData(typeof(DefaultGenerator))]
    public void Extension_FastToString_GeneratesTheSameResultAsToString(Color value)
    {
        // Arrange
        string expected = value.ToString();

        // Act
        string actual = value.FastToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(FormattingGenerator))]
    public void Extension_ToStringFormat_GeneratesTheSameResultAsToStringFormat(Color value, string format)
    {
        // Arrange
        string expected = value.ToString(format);

        // Act
        string actual = value.FastToString(format);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(Color.Red | Color.Blue)]
    [InlineData(Color.Red | Color.Green)]
    [InlineData(Color.Blue | Color.Green)]
    public void Extension_HasFlag_GeneratesTheSameResultAsHasFlag(Color value)
    {
        // Arrange
        const Color color = Color.Blue;
        
        bool expected = value.HasFlag(flag: color);

        // Act
        bool actual = value.HasFlag(flags: color);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(DefaultGenerator))]
    public void Extension_IsDefined_GeneratesTheSameResultAsIsDefined(Color value)
    {
        // Arrange
        bool expected = Enum.IsDefined(value);

        // Act
        bool actual = value.IsDefined();

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Extension_GetValues_GeneratesTheSameResultAsGetValues()
    {
        // Arrange
        Color[] expected = Enum.GetValues<Color>();

        // Act
        Color[] actual = ColorExtensions.GetValues();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Extension_GetNames_GeneratesTheSameResultAsGetNames()
    {
        // Arrange
        string[] expected = Enum.GetNames<Color>();

        // Act
        string[] actual = ColorExtensions.GetNames();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Extension_GetUnderlyingValues_GeneratesTheSameResultAsGetValuesAsUnderlyingType()
    {
        // Arrange
#if NET7_0_OR_GREATER
        Array expected = Enum.GetValuesAsUnderlyingType<Color>();
#else
        Array expected = GetValuesAsUnderlyingType();
#endif

        // Act
        var actual = ColorExtensions.GetUnderlyingValues();

        // Assert
        Assert.Equal(expected, actual);

#if NET6_0
        static object[] GetValuesAsUnderlyingType()
        {
            Type underlyingType = Enum.GetUnderlyingType(typeof(Color));

            Color[] values = Enum.GetValues<Color>();

            return values
                .Select(x => Convert.ChangeType(x, underlyingType, CultureInfo.InvariantCulture))
                .ToArray();
        }
#endif
    }

    [Fact]
    public void Extension_MembersCount_GetsTheCorrectAmountOfMembersCount()
    {
        // Arrange
        int expected = Enum.GetValues<Color>().Length;

        // Act
        int actual = ColorExtensions.MembersCount;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(EnumStringGenerator))]
    public void Extension_TryParseString_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
    {
        // Act
        bool success1 = ColorExtensions.TryParse(name, out Color color1);
        bool success2 = Enum.TryParse(name, out Color color2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(EnumStringIgnoreCaseGenerator))]
    public void Extension_TryParseStringIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
    {
        // Act
        bool success1 = ColorExtensions.TryParseIgnoreCase(name, out Color color1);
        bool success2 = Enum.TryParse(name, true, out Color color2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(EnumStringGenerator))]
    public void Extension_TryParseSpan_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
    {
        // Arrange
        ReadOnlySpan<char> spanName = name.AsSpan();
        
        // Act
        bool success1 = ColorExtensions.TryParse(spanName, out Color color1);
        bool success2 = Enum.TryParse(spanName, out Color color2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(EnumStringIgnoreCaseGenerator))]
    public void Extension_TryParseSpanIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
    {
        // Arrange
        ReadOnlySpan<char> spanName = name.AsSpan();

        // Act
        bool success1 = ColorExtensions.TryParseIgnoreCase(spanName, out Color color1);
        bool success2 = Enum.TryParse(spanName, true, out Color color2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(DescriptionDataGenerator))]
    public void Extension_GetDescription_GeneratesTheSameResultAsReflectionWould(Color input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }
}

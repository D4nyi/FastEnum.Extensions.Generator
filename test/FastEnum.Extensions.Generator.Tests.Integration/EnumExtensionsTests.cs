using FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

namespace FastEnum.Extensions.Generator.Tests.Integration;

public sealed class EnumExtensionsTests
{
    [Theory]
    [ClassData(typeof(DefaultGenerator))]
    public void FastToString_GeneratesTheSameResultAsToString(Color value)
    {
        // Arrange
        string expected = value.ToString();

        // Act
        string actual = value.FastToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ColorFormattingGenerator))]
    public void ToStringFormat_GeneratesTheSameResultAsToStringFormat(Color value, string? format)
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
    public void HasFlag_GeneratesTheSameResultAsHasFlag(Color value)
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
    public void IsDefined_GeneratesTheSameResultAsIsDefined(Color value)
    {
        // Arrange
        bool expected = Enum.IsDefined(value);

        // Act
        bool actual = value.IsDefined();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetValues_GeneratesTheSameResultAsGetValues()
    {
        // Arrange
        Color[] expected = Enum.GetValues<Color>();

        // Act
        Color[] actual = ColorExtensions.GetValues();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetNames_GeneratesTheSameResultAsGetNames()
    {
        // Arrange
        string[] expected = Enum.GetNames<Color>();

        // Act
        string[] actual = ColorExtensions.GetNames();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUnderlyingType_ExtractedCorrectly()
    {
        // Arrange
        Type expected = Enum.GetValuesAsUnderlyingType<Color>()
            .GetValue(0)!
            .GetType();

        // Act
        Type actual = ColorExtensions.GetUnderlyingValues()[0].GetType();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUnderlyingValues_GeneratesTheSameResultAsGetValuesAsUnderlyingType()
    {
        // Arrange
        short[] expected = (short[])Enum.GetValuesAsUnderlyingType<Color>();

        // Act
        short[] actual = ColorExtensions.GetUnderlyingValues();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MembersCount_GetsTheCorrectAmountOfMembersCount()
    {
        // Arrange
        int expected = Enum.GetValues<Color>().Length;

        // Act
        int actual = ColorExtensions.MembersCount;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ColorStringGenerator))]
    public void TryParseString_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
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

    [Fact]
    public void TryParse_NonIntegerNumber()
    {
        // Arrange
        const string name = "1.4";

        // Act
        bool success1 = ColorExtensions.TryParse(name, out _);
        bool success2 = Enum.TryParse<Color>(name, out _);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);
    }

    [Fact]
    public void TryParseString_NumberWithWhiteSpacesAtStart()
    {
        // Arrange
        const string name = " 4";
        const Color expected = Color.Red;

        // Act
        bool success1 = ColorExtensions.TryParse(name, out Color color1);
        bool success2 = Enum.TryParse(name, out Color color2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, color1);
        Assert.Equal(color1, color2);
    }

    [Fact]
    public void TryParseString_JustWhiteSpace()
    {
        // Arrange
        const string name = "   ";

        // Act
        bool success1 = ColorExtensions.TryParse(name, out Color color1);
        bool success2 = Enum.TryParse(name, out Color color2);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(default, color1);
        Assert.Equal(color1, color2);
    }

    [Fact]
    public void TryParse_InvalidEnumMember()
    {
        // Arrange
        const string name = "BlueRed";

        // Act
        bool success1 = ColorExtensions.TryParse(name, out _);
        bool success2 = Enum.TryParse<Color>(name, out _);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);
    }

    [Fact]
    public void TryParse_InvalidCompositionString()
    {
        // Arrange
        const string name = "Blue,Red,";

        // Act
        bool success1 = ColorExtensions.TryParse(name, out _);
        bool success2 = Enum.TryParse<Color>(name, out _);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void TryParseString_InvalidInput(string? name)
    {
        // Act
        bool success1 = ColorExtensions.TryParse(name, out Color color1);
        bool success2 = Enum.TryParse(name, out Color color2);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(default, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(ColorStringIgnoreCaseGenerator))]
    public void TryParseStringIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
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

    [Fact]
    public void TryParseStringIgnoreCase_JustWhiteSpace()
    {
        // Arrange
        const string name = "   ";

        // Act
        bool success1 = ColorExtensions.TryParseIgnoreCase(name, out Color color1);
        bool success2 = Enum.TryParse(name, true, out Color color2);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(default, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void TryParseStringIgnoreCase_InvalidInput(string? name)
    {
        // Act
        bool success1 = ColorExtensions.TryParseIgnoreCase(name, out Color color1);
        bool success2 = Enum.TryParse(name, true, out Color color2);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(default, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(ColorStringGenerator))]
    public void TryParseSpan_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
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
    [InlineData(null)]
    [InlineData("")]
    public void TryParseSpan_InvalidInput(string? name)
    {
        // Act
        ReadOnlySpan<char> spanName = name.AsSpan();

        bool success1 = ColorExtensions.TryParse(spanName, out Color color1);
        bool success2 = Enum.TryParse(spanName, out Color color2);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(default, color1);
        Assert.Equal(color1, color2);
    }

    [Theory]
    [ClassData(typeof(ColorStringIgnoreCaseGenerator))]
    public void TryParseSpanIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, Color expected)
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
    [InlineData(null)]
    [InlineData("")]
    public void TryParseSpanIgnoreCase_InvalidInput(string? name)
    {
        // Act
        ReadOnlySpan<char> spanName = name.AsSpan();

        bool success1 = ColorExtensions.TryParseIgnoreCase(spanName, out Color color1);
        bool success2 = Enum.TryParse(spanName, true, out Color color2);

        // Assert
        Assert.False(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(default, color1);
        Assert.Equal(color1, color2);
    }

    [Fact]
    public void GetDescription()
    {
        // Arrange
        Color[] members = ColorExtensions.GetValues();

        // Act && Assert
        foreach (Color member in members)
        {
            Assert.Equal(ColorNames.Lookup[member], member.GetDescription());
        }
    }

    [Theory]
    [ClassData(typeof(DescriptionDataGenerators<Color>))]
    public void GetDescription_GeneratesTheSameResultAsReflectionWould(Color input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Fact]
    public void GetEnumMemberValue()
    {
        // Arrange
        Color[] members = ColorExtensions.GetValues();

        // Act && Assert
        foreach (Color member in members)
        {
            Assert.Equal(ColorNames.Lookup[member], member.GetEnumMemberValue());
        }
    }

    [Theory]
    [ClassData(typeof(EnumMemberValueDataGenerator<Color>))]
    public void GetEnumMemberValue_GeneratesTheSameResultAsReflectionWould(Color input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetEnumMemberValue();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Fact]
    public void GetDisplayDescription()
    {
        // Arrange
        Color[] members = ColorExtensions.GetValues();

        // Act && Assert
        foreach (Color member in members)
        {
            Assert.Equal(ColorNames.Lookup[member], member.GetDisplayDescription());
        }
    }

    [Theory]
    [ClassData(typeof(DisplayDescriptionDataGenerator<Color>))]
    public void GetDisplayDescription_GeneratesTheSameResultAsReflectionWould(Color input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDisplayDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Fact]
    public void GetDisplayName()
    {
        // Arrange
        Color[] members = ColorExtensions.GetValues();

        // Act && Assert
        foreach (Color member in members)
        {
            Assert.Equal(ColorNames.Lookup[member], member.GetDisplayName());
        }
    }

    [Theory]
    [ClassData(typeof(DisplayNameDataGenerator<Color>))]
    public void GetDisplayName_GeneratesTheSameResultAsReflectionWould(Color input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDisplayName();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }
}

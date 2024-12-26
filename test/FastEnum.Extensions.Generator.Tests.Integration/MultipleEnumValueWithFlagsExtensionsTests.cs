using FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

namespace FastEnum.Extensions.Generator.Tests.Integration;

public sealed class MultipleEnumValueWithFlagsExtensionsTests
{
    [Theory]
    [ClassData(typeof(DefaultGenerator))]
    public void FastToString_GeneratesTheSameResultAsToString(GenerationOptions value)
    {
        // Arrange
        string expected = value.ToString();

        // Act
        string actual = value.FastToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionFormattingGenerator))]
    public void ToStringFormat_GeneratesTheSameResultAsToStringFormat(GenerationOptions value, string? format)
    {
        // Arrange
        string expected = value.ToString(format);

        // Act
        string actual = value.FastToString(format);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUnderlyingValues_GeneratesTheSameResultAsGetValuesAsUnderlyingType()
    {
        // Arrange
        int[] expected = (int[])Enum.GetValuesAsUnderlyingType<GenerationOptions>();

        // Act
        int[] actual = GenerationOptionExtensions.GetUnderlyingValues();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MembersCount_GetsTheCorrectAmountOfMembersCount()
    {
        // Arrange
        int expected = Enum.GetValues<GenerationOptions>().Length;

        // Act
        int actual = GenerationOptionExtensions.MembersCount;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringGenerator))]
    public void TryParseString_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOptions expected)
    {
        // Act
        bool success1 = GenerationOptionsExtensions.TryParse(name, out GenerationOptions generationOption1);
        bool success2 = Enum.TryParse(name, out GenerationOptions generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringIgnoreCaseGenerator))]
    public void TryParseStringIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOptions expected)
    {
        // Act
        bool success1 = GenerationOptionsExtensions.TryParseIgnoreCase(name, out GenerationOptions generationOption1);
        bool success2 = Enum.TryParse(name, true, out GenerationOptions generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringGenerator))]
    public void TryParseSpan_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOptions expected)
    {
        // Arrange
        ReadOnlySpan<char> spanName = name.AsSpan();

        // Act
        bool success1 = GenerationOptionsExtensions.TryParse(spanName, out GenerationOptions generationOption1);
        bool success2 = Enum.TryParse(spanName, out GenerationOptions generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringIgnoreCaseGenerator))]
    public void TryParseSpanIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOptions expected)
    {
        // Arrange
        ReadOnlySpan<char> spanName = name.AsSpan();

        // Act
        bool success1 = GenerationOptionsExtensions.TryParseIgnoreCase(spanName, out GenerationOptions generationOption1);
        bool success2 = Enum.TryParse(spanName, true, out GenerationOptions generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(DescriptionDataGenerators<GenerationOptions>))]
    public void GetDescription_GeneratesTheSameResultAsReflectionWould(GenerationOptions input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Theory]
    [ClassData(typeof(EnumMemberValueDataGenerator<GenerationOptions>))]
    public void GetEnumMemberValue_GeneratesTheSameResultAsReflectionWould(GenerationOptions input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetEnumMemberValue();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Theory]
    [ClassData(typeof(DisplayDescriptionDataGenerator<GenerationOptions>))]
    public void GetDisplayDescription_GeneratesTheSameResultAsReflectionWould(GenerationOptions input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDisplayDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Theory]
    [ClassData(typeof(DisplayNameDataGenerator<GenerationOptions>))]
    public void GetDisplayName_GeneratesTheSameResultAsReflectionWould(GenerationOptions input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDisplayName();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }
}

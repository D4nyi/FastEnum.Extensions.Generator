using FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

namespace FastEnum.Extensions.Generator.Tests.Integration;

public sealed class MultipleEnumValueExtensionsTests
{
    [Theory]
    [ClassData(typeof(DefaultGenerator))]
    public void FastToString_GeneratesTheSameResultAsToString(GenerationOption value)
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
    public void ToStringFormat_GeneratesTheSameResultAsToStringFormat(GenerationOption value, string? format)
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
        int[] expected = (int[])Enum.GetValuesAsUnderlyingType<GenerationOption>();

        // Act
        int[] actual = GenerationOptionExtensions.GetUnderlyingValues();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MembersCount_GetsTheCorrectAmountOfMembersCount()
    {
        // Arrange
        int expected = Enum.GetValues<GenerationOption>().Length;

        // Act
        int actual = GenerationOptionExtensions.MembersCount;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringGenerator))]
    public void TryParseString_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOption expected)
    {
        // Act
        bool success1 = GenerationOptionExtensions.TryParse(name, out GenerationOption generationOption1);
        bool success2 = Enum.TryParse(name, out GenerationOption generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringIgnoreCaseGenerator))]
    public void TryParseStringIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOption expected)
    {
        // Act
        bool success1 = GenerationOptionExtensions.TryParseIgnoreCase(name, out GenerationOption generationOption1);
        bool success2 = Enum.TryParse(name, true, out GenerationOption generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringGenerator))]
    public void TryParseSpan_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOption expected)
    {
        // Arrange
        ReadOnlySpan<char> spanName = name.AsSpan();

        // Act
        bool success1 = GenerationOptionExtensions.TryParse(spanName, out GenerationOption generationOption1);
        bool success2 = Enum.TryParse(spanName, out GenerationOption generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(GenerationOptionStringIgnoreCaseGenerator))]
    public void TryParseSpanIgnoreCase_GeneratesTheSameResultAsTheBuiltInMethod(string name, GenerationOption expected)
    {
        // Arrange
        ReadOnlySpan<char> spanName = name.AsSpan();

        // Act
        bool success1 = GenerationOptionExtensions.TryParseIgnoreCase(spanName, out GenerationOption generationOption1);
        bool success2 = Enum.TryParse(spanName, true, out GenerationOption generationOption2);

        // Assert
        Assert.True(success1);
        Assert.Equal(success1, success2);

        Assert.Equal(expected, generationOption1);
        Assert.Equal(generationOption1, generationOption2);
    }

    [Theory]
    [ClassData(typeof(DescriptionDataGenerators<GenerationOption>))]
    public void GetDescription_GeneratesTheSameResultAsReflectionWould(GenerationOption input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Theory]
    [ClassData(typeof(EnumMemberValueDataGenerator<GenerationOption>))]
    public void GetEnumMemberValue_GeneratesTheSameResultAsReflectionWould(GenerationOption input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetEnumMemberValue();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Theory]
    [ClassData(typeof(DisplayDescriptionDataGenerator<GenerationOption>))]
    public void GetDisplayDescription_GeneratesTheSameResultAsReflectionWould(GenerationOption input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDisplayDescription();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }

    [Theory]
    [ClassData(typeof(DisplayNameDataGenerator<GenerationOption>))]
    public void GetDisplayName_GeneratesTheSameResultAsReflectionWould(GenerationOption input, string? expectedDescription)
    {
        // Act
        string? actualDescription = input.GetDisplayName();

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class EnumMemberValueDataGenerator() : AttributeDataGenerator<EnumMemberAttribute>(x => x?.Value);

internal sealed class DisplayNameDataGenerator() : AttributeDataGenerator<DisplayAttribute>(x => x?.Name);

internal sealed class DisplayDescriptionDataGenerator() : AttributeDataGenerator<DisplayAttribute>(x => x?.Description);

internal sealed class DescriptionDataGenerators() : AttributeDataGenerator<DescriptionAttribute>(x => x?.Description);

internal abstract class AttributeDataGenerator<T> : TheoryData<Color, string?> where T : Attribute
{
    protected AttributeDataGenerator(Func<T?, string?> accessor)
    {
        foreach (Color color in Constants.TestValues)
        {
            T? attribute = GetAttribute(color);

            Add(color, accessor(attribute));
        }
    }

    private static T? GetAttribute(Color color) => Constants.ColorType
        .GetField(color.ToString())
        ?.GetCustomAttributes(typeof(T), false) is T[] { Length: > 0 } attributes
        ? attributes[0]
        : null;
}

static file class Constants
{
    public static readonly Type ColorType = typeof(Color);
    public static readonly Color[] TestValues = [Color.Red, Color.Green, Color.Black, Color.Blue, (Color)15];
}

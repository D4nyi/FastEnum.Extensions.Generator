using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace FastEnum.Extensions.Generator.Tests.Integration.DataGenerators;

internal sealed class EnumMemberValueDataGenerator<TEnum>() : AttributeDataGenerator<EnumMemberAttribute, TEnum>(x => x?.Value) where TEnum : Enum;

internal sealed class DisplayNameDataGenerator<TEnum>() : AttributeDataGenerator<DisplayAttribute, TEnum>(x => x?.Name) where TEnum : Enum;

internal sealed class DisplayDescriptionDataGenerator<TEnum>() : AttributeDataGenerator<DisplayAttribute, TEnum>(x => x?.Description) where TEnum : Enum;

internal sealed class DescriptionDataGenerators<TEnum>() : AttributeDataGenerator<DescriptionAttribute, TEnum>(x => x?.Description) where TEnum : Enum;

internal abstract class AttributeDataGenerator<T, TEnum> : TheoryData<TEnum, string?>
    where T : Attribute
    where TEnum : Enum
{
    protected AttributeDataGenerator(Func<T?, string?> accessor)
    {
        Type backingType = Enum.GetUnderlyingType(typeof(TEnum));

        for (int index = 0; index < Constants.TestValues.Length; index++)
        {
            short value = Constants.TestValues[index];

            TEnum @enum;
            if (backingType == typeof(int))
            {
                int tmp = value;
                @enum = Unsafe.As<int, TEnum>(ref tmp);
            }
            else
            {
                @enum = Unsafe.As<short, TEnum>(ref value);
            }

            T? attribute = GetAttribute(@enum);

            Add(@enum, accessor(attribute));
        }
    }

    private static T? GetAttribute(TEnum @enum) => typeof(TEnum)
        .GetField(@enum.ToString())
        ?.GetCustomAttributes(typeof(T), false) is T[] { Length: > 0 } attributes
        ? attributes[0]
        : null;
}

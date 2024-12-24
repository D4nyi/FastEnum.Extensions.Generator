using System.Reflection;
using System.Globalization;
using System.Text;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class Helpers
{
    private static readonly ParameterModifier[] Modifiers = [new(1)];
    private static readonly Type[] ArgTypes = [typeof(string)];
    private const BindingFlags Binding = BindingFlags.Public | BindingFlags.Instance;

    private static readonly Dictionary<Type, MethodInfo> ToStringFormats = [];

    internal static StringBuilder AddCorrectBitwiseOperation(this StringBuilder sb, string underlyingTypeName)
    {
        if (underlyingTypeName is "int" or "uint" or "long" or "ulong")
        {
            return sb.AppendLine("resultValue &= ~currentValue;");
        }

        // "byte" or "sbyte" or "short" or "ushort" or others
        return sb.Append("                resultValue = (").Append(underlyingTypeName).AppendLine(")(resultValue & ~currentValue);");
    }

    internal static StringBuilder AddCast(this StringBuilder sb, string enumName, string underlyingType)
    {
        return sb
            .Append("global::System.Runtime.CompilerServices.Unsafe.As<").Append(enumName)
            .Append(", ").Append(underlyingType).Append(">(ref value)");
    }

    internal static string GetFormat(this Type type)
    {
        if (type == typeof(byte) || type == typeof(sbyte))
        {
            return "X2";
        }

        if (type == typeof(short) || type == typeof(ushort))
        {
            return "X4";
        }

        if (type == typeof(int) || type == typeof(uint))
        {
            return "X8";
        }

        return "X16";
    }

    internal static MethodInfo GetToStringFormat(Type membersType)
    {
        if (!ToStringFormats.TryGetValue(membersType, out MethodInfo? toString) || toString is null)
        {
            toString = membersType.GetMethod(nameof(ToString), Binding, null, CallingConventions.Any, ArgTypes,
                           Modifiers)
                       ?? throw new InvalidOperationException("'ToString' method is not found!");

            ToStringFormats.Add(membersType, toString);
        }

        return toString;
    }

    internal static object GetCorrectZero(this Type numberType) =>
        numberType == typeof(int)
            ? 0
            : Convert.ChangeType(0, numberType, CultureInfo.InvariantCulture);
}

using System.Globalization;
using System.Reflection;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class Helpers
{
    private static readonly ParameterModifier[] _modifiers = [new(1)];
    private static readonly Type[] _argTypes = [typeof(string)];
    private const BindingFlags Binding = BindingFlags.Public | BindingFlags.Instance;

    private static readonly Dictionary<Type, MethodInfo> _toStringFormats = [];

    internal static StringBuilder AddCorrectBitwiseOperation(this StringBuilder sb, string underlyingTypeName)
    {
        sb.Append("                resultValue ");
        if (underlyingTypeName is "int" or "uint" or "long" or "ulong")
        {
            return sb.AppendLine("&= ~currentValue;");
        }

        // "byte" or "sbyte" or "short" or "ushort" or others
        return sb.Append("= (").Append(underlyingTypeName).AppendLine(")(resultValue & ~currentValue);");
    }

    internal static StringBuilder AddShift(this StringBuilder sb, int shiftValue)
    {
        if (shiftValue >= 8)
        {
            sb.Append("(value >> ").Append(shiftValue.ToString(CultureInfo.InvariantCulture)).AppendLine(");");
        }
        else
        {
            sb.AppendLine("value;");
        }

        return sb;
    }

    internal static StringBuilder AddTypeDefinition(this StringBuilder sb, string type, bool add)
    {
        if (add)
        {
            sb.Append("global::System.").Append(type).Append(' ');
        }

        return sb;
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
        if (!_toStringFormats.TryGetValue(membersType, out MethodInfo? toString) || toString is null)
        {
            toString = membersType.GetMethod(nameof(ToString), Binding, null, CallingConventions.Any, _argTypes,
                           _modifiers)
                       ?? throw new InvalidOperationException("'ToString' method is not found!");

            _toStringFormats.Add(membersType, toString);
        }

        return toString;
    }

    internal static object GetCorrectZero(this Type numberType) =>
        numberType == typeof(int)
            ? 0
            : Convert.ChangeType(0, numberType, CultureInfo.InvariantCulture);

    internal static string Get(this Indentation indentation)
    {
        // 4 spaces per indentation.
        return indentation switch
        {
            Indentation.Method     => "    ",
            Indentation.MethodBody => "        ",
            Indentation.Nesting1   => "            ",
            Indentation.Nesting2   => "                ",
            Indentation.Nesting3   => "                    ",
            Indentation.Nesting4   => "                        ",
            _ => ""
        };
    }
}

using System.Globalization;
using System.Reflection;
using System.Text;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class Helpers
{
    private const BindingFlags Binding = BindingFlags.Public | BindingFlags.Instance;

    private static readonly ParameterModifier[] _modifiers = [new(1)];
    private static readonly Type[] _argTypes = [typeof(string)];

    private static readonly Dictionary<Type, MethodInfo> _toStringFormats = [];

    extension(StringBuilder sb)
    {
        internal StringBuilder AddCorrectBitwiseOperation(string underlyingTypeName)
        {
            sb.Append("                resultValue ");

            if (underlyingTypeName is "int" or "uint" or "long" or "ulong")
            {
                return sb.AppendLine("&= ~currentValue;");
            }

            // "byte" or "sbyte" or "short" or "ushort" or others
            return sb.Append("= (").Append(underlyingTypeName).AppendLine(")(resultValue & ~currentValue);");
        }

        internal StringBuilder AddShift(int shiftValue)
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

        internal StringBuilder AddTypeDefinition(string type, bool add)
        {
            if (add)
            {
                sb.Append("global::System.").Append(type).Append(' ');
            }

            return sb;
        }

        internal StringBuilder AddCast(string enumName, string underlyingType)
        {
            return sb
                .Append("global::System.Runtime.CompilerServices.Unsafe.As<").Append(enumName)
                .Append(", ").Append(underlyingType).Append(">(ref value)");
        }
    }

    extension(Type type)
    {
        internal string GetFormat()
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

        internal MethodInfo GetToStringFormat()
        {
            if (!_toStringFormats.TryGetValue(type, out MethodInfo? toString) || toString is null)
            {
                toString = type.GetMethod(nameof(ToString), Binding, null, CallingConventions.Any, _argTypes, _modifiers)!;

                _toStringFormats.Add(type, toString);
            }

            return toString;
        }

        internal object GetCorrectZero() => Type.GetTypeCode(type) switch
        {
            TypeCode.Byte => (byte)0,
            TypeCode.SByte => (sbyte)0,
            TypeCode.Int16 => (short)0,
            TypeCode.UInt16 => (ushort)0,
            TypeCode.Int32 => 0,
            TypeCode.UInt32 => (uint)0,
            TypeCode.Int64 => (long)0,
            TypeCode.UInt64 => (ulong)0,
            _ => Convert.ChangeType(0, type, CultureInfo.InvariantCulture)
        };
    }
}

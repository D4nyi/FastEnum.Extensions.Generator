using System.Reflection;
using System;
using System.Diagnostics;
using System.Text;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class Helpers
{
    private static readonly ParameterModifier[] _modifiers = { new(1) };
    private static readonly Type[] _argTypes = { typeof(string) };
    private const BindingFlags binding = BindingFlags.Public | BindingFlags.Instance;
    
    internal static StringBuilder AddCorrectBitwiseOperation(this StringBuilder sb, string underlyingTypeName)
    {
        if (underlyingTypeName is "int" or "uint" or "long" or "ulong")
        {
            return sb.AppendLine("resultValue &= ~currentValue;");
        }

        // "byte" or "sbyte" or "short" or "ushort" or others
        return sb.Append("resultValue = (").Append(underlyingTypeName).AppendLine(")(resultValue & ~currentValue);");
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
        MethodInfo? toString = membersType.GetMethod("ToString", binding, null, CallingConventions.Any, _argTypes, _modifiers);

        if (toString is null)
        {
            throw new UnreachableException("'ToString' method is not found!");
        }
        
        return toString;
    }
}

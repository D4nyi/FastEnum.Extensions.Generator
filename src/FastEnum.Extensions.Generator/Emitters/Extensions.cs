using System.Text;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class Extensions
{
    internal static StringBuilder AddCorrectBitwiseOperation(this StringBuilder sb, string underlyingTypeName)
    {
        if (underlyingTypeName is "int" or "uint" or "long" or "ulong")
        {
            return sb.AppendLine("resultValue &= ~currentValue;");
        }

        // "byte" or "sbyte" or "short" or "ushort" or others
        return sb.Append("resultValue = (").Append(underlyingTypeName).AppendLine(")(resultValue & ~currentValue);");
    }
}
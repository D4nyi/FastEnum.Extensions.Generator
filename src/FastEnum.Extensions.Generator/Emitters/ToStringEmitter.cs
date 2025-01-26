using System.Globalization;
using System.Reflection;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class ToStringEmitter
{
    internal static void AddToString(StringBuilder sb, EnumGenerationSpec spec)
    {
        string methodIndent = Indentation.Method.Get();
        string methodBodyIndent = Indentation.MethodBody.Get();

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
                    /// <param name="value">The <see cref="{1}"/> value to convert to a string.</param>
                    /// <returns>The string representation of the value of this instance.</returns>
                    {0} static global::System.String FastToString(this {1} value) => value switch
                    {{

                """, spec.Modifier, spec.FullName);

        foreach (EnumMemberSpec member in spec.DistinctMembers)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => nameof(")
                .Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(methodBodyIndent).Append("_ => (")
            .AddCast(spec.FullName, spec.UnderlyingType).AppendLine(").ToString()")
            .Append(methodIndent).AppendLine("};")
            .AppendLine();
    }

    internal static void AddToStringFormat(StringBuilder sb, EnumGenerationSpec spec)
    {
        string methodIndent = Indentation.Method.Get();

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
                    /// <param name="value">The <see cref="{0}"/> value to convert to a string.</param>
                    /// <param name="format">A format string.</param>
                    /// <returns>The string representation of the value of this instance as specified by format.</returns>
                    /// <exception cref="global::System.FormatException"><paramref name="format"/> contains an invalid specification.</exception>

                """, spec.FullName)
            .Append(methodIndent).Append(spec.Modifier).Append(" static global::System.String FastToString(this ").Append(spec.FullName)
            .AppendLine(" value, [global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute(\"EnumFormat\")] global::System.String? format)")
            .Append(
                """
                    {
                        if (global::System.String.IsNullOrEmpty(format)) return value.FastToString();

                        if (format.Length != 1) throw CreateInvalidFormatSpecifierException();

                        global::System.Char formatChar = format[0];
                        switch (formatChar | 0x20)
                        {
                            case 'g': return value.FastToString();
                            case 'd': return
                """).Append(' ').AddCast(spec.FullName, spec.UnderlyingType).AppendLine(".ToString();")
            .Append(
                """
                            case 'x': return FormatNumberAsHex(value);
                            case 'f':
                                global::System.String? result = FormatFlagNames(value);
                                if (result is null) goto case 'd';
                                return result;
                            default: throw CreateInvalidFormatSpecifierException();
                        }
                    }


                """);
    }

    internal static void AddFormatFlagNames(StringBuilder sb, EnumGenerationSpec spec)
    {
        string methodBodyIndent = Indentation.MethodBody.Get();

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.String? FormatFlagNames({0} value) => value switch
                    {{

                """, spec.FullName);

        // Necessary to change the type of the number ZERO if the underlying type is not Int32,
        // otherwise the Equals will return false, no matter what the actual first value is in the enum
        // because of the type mismatch
        object zero = spec.Members[0].Value.GetType().GetCorrectZero();

        if (!spec.Members[0].Value.Equals(zero))
        {
            sb.AppendLine("        0 => \"0\",");
        }

        foreach (EnumMemberSpec member in spec.DistinctFlagMembers)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => nameof(")
                .Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(
                """
                        _ => ProcessMultipleFlagsNames(value)
                    };


                """);
    }

    internal static void AddFormatAsHexHelper(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.String FormatNumberAsHex({0} data) => data switch
                    {{

                """, spec.FullName);

        AddHexValuesForKnownFields(sb, spec);

        bool isByteSized = spec.OriginalUnderlyingType.EndsWith("byte", StringComparison.OrdinalIgnoreCase);

        string underlyingType = isByteSized ? "global::System.Byte" : spec.UnderlyingType;

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                        _ => global::System.String.Create(sizeof({0}) * 2, global::System.Runtime.CompilerServices.Unsafe.As<{1}, {0}>(ref data), (buffer, value) =>
                        {{

                """, underlyingType, spec.FullName);

        if (isByteSized)
        {
            sb
                .Append(
                    """
                                global::System.UInt32 difference = ((value & 0xF0U) << 4) + (value & 0x0FU) - 0x8989U;
                                global::System.UInt32 packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

                                buffer[1] = (global::System.Char)(packedResult & 0xFFU);
                                buffer[0] = (global::System.Char)(packedResult >> 8);
                            })
                        };


                    """);

            return;
        }

        UseToCharsBufferHelperInlined(sb, spec.OriginalUnderlyingType);
    }

    private static void AddHexValuesForKnownFields(StringBuilder sb, EnumGenerationSpec spec)
    {
        string nesting1Indent = Indentation.MethodBody.Get();

        Type membersType = spec.Members[0].Value.GetType(); // an enum has only one backing type
        MethodInfo toStringFormat = Helpers.GetToStringFormat(membersType);
        object[] toStringParam = [spec.ToStringFormat];

        foreach (EnumMemberSpec member in spec.DistinctMembers)
        {
            string hex = (string)toStringFormat.Invoke(member.Value, toStringParam);

            sb
                .Append(nesting1Indent).Append(member.FullName).Append(" => \"").Append(hex).AppendLine("\",");
        }
    }

    private static void UseToCharsBufferHelperInlined(StringBuilder sb, string originalUnderlyingType)
    {
        int shiftValue = originalUnderlyingType switch
        {
            "ulong" or "long" => 56,
            "uint" or "int" => 24,
            "ushort" or "short" => 8,
            _ => 0
        };

        if (shiftValue != 0)
        {
            bool firstItem = true;
            int startIndex = 0;

            for (; shiftValue >= 0; shiftValue -= 8)
            {
                sb
                    .Append("             ")
                    .AddTypeDefinition("Byte", firstItem)
                    .Append("byteValue = (global::System.Byte)").AddShift(shiftValue)
                    .Append("             ")
                    .AddTypeDefinition("UInt32", firstItem)
                    .AppendLine("difference = ((byteValue & 0xF0U) << 4) + (byteValue & 0x0FU) - 0x8989U;")
                    .Append("             ")
                    .AddTypeDefinition("UInt32", firstItem)
                    .AppendLine("packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;")
                    .AppendLine();

                firstItem = false;

                sb.Append("             buffer[").Append(startIndex + 1).AppendLine("] = (global::System.Char)(packedResult & 0xFFU);");
                sb.Append("             buffer[").Append(startIndex).AppendLine("] = (global::System.Char)(packedResult >> 8);");

                if (shiftValue >= 8)
                {
                    sb.AppendLine();
                }

                startIndex += 2;
            }
        }

        sb
            .Append(
                """
                        })
                    };


                """);
    }
}

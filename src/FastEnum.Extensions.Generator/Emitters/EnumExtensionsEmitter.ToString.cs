using System.Globalization;
using System.Text;

using FastEnum.Extensions.Generator.Emitters;
using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddToString(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
            """
                /// <summary>
                /// Converts the value of this instance to its equivalent string representation.")
                /// </summary>")
                /// <returns>The string representation of the value of this instance.</returns>
                {0} static global::System.String FastToString(this {1} value) => value switch
                {{
            
            """, _currentSpec.Modifier, _currentSpec.FullName);

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => nameof(")
                .Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(methodBodyIndent).Append("_ => (")
                .AddCast(_currentSpec.FullName, _currentSpec.UnderlyingType).AppendLine(").ToString()")
            .Append(methodIndent).AppendLine("};")
            .AppendLine();
    }

    private void AddToStringFormat(StringBuilder sb)
    {
        sb
            .Append(
                """
                    /// <summary>
                    /// Converts the value of this instance to its equivalent string representation using the specified format.
                    /// </summary>
                    /// <param name="format">A format string.</param>
                    /// <returns>The string representation of the value of this instance as specified by format.</returns>
                    /// <exception cref="global::System.FormatException"><paramref name="format"/> contains an invalid specification.</exception>
                    
                """)
            .Append(_currentSpec.Modifier).Append(" static global::System.String FastToString(this ").Append(_currentSpec.FullName).AppendLine(" value,")
            .Append(
                """
                #if NET7_0_OR_GREATER
                    [global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.EnumFormat)]
                #endif
                    global::System.String? format)
                    {
                        if (global::System.String.IsNullOrEmpty(format)) return value.FastToString();
                        
                        if (format.Length != 1) throw CreateInvalidFormatSpecifierException();
                        
                        global::System.Char formatChar = format[0];
                        switch (formatChar | 0x20)
                        {
                            case 'g': return value.FastToString();
                            case 'd': return 
                """).AddCast(_currentSpec.FullName, _currentSpec.UnderlyingType).AppendLine(".ToString();")
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

    private void AddFormatFlagNames(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    private static global::System.String FormatFlagNames({0} value) => value switch
                    {{
                
                """, _currentSpec.FullName);

        // Necessary to change the type of the number ZERO if the underlying type is not Int32,
        // otherwise the Equals will return false, no matter what the actual first value is in the enum
        // because of the type miss match  
        object zero = _currentSpec.Members[0].Value.GetType().GetCorrectZero();
        
        if (!_currentSpec.Members[0].Value.Equals(zero))
        {
            sb.AppendLine("        0 => \"0\",");
        }
        
        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => nameof(")
                .Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(
                """
                        _ => WriteMultipleFoundFlagsNames(value)
                    };
                
                
                """);
    }

    private void AddFormatAsHexHelper(StringBuilder sb)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.String FormatNumberAsHex({0} data)
                    {{
                
                """, _currentSpec.FullName);

        AddFormatNumberAsHexBufferGeneration(sb);

        int shiftValue = _currentSpec.OriginalUnderlyingType switch
        {
            "ulong" or "long" => 56,
            "uint" or "int" => 24,
            "ushort" or "short" => 8,
            _ => 0
        };

        if (shiftValue != 0)
        {
            int startIndex = 0;
            for (; shiftValue >= 0; shiftValue -= 8)
            {
                sb.Append("            ToCharsBuffer((global::System.Byte)");
                if (shiftValue >= 8)
                {
                    sb.Append("(value >> ").Append(shiftValue).Append(')');
                }
                else
                {
                    sb.Append("value");
                }

                sb.Append(", destination, ").Append(startIndex).AppendLine(");");

                startIndex += 2;
            }
        }
        else
        {
            sb
                .Append("            ToCharsBuffer((")
                .Append(_currentSpec.UnderlyingType).AppendLine(")data, destination, 0);");
        }

        sb
            .Append(
                """
                            return new global::System.String(destination);
                        }
                    }
                
                
                """);
    }

    private void AddFormatNumberAsHexBufferGeneration(StringBuilder sb)
    {
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .AppendLine(
                """
                        return data switch
                        {
                """);

        var membersType = _currentSpec.Members[0].Value.GetType(); // an enum has only one backing type
        var toStringFormat = Helpers.GetToStringFormat(membersType);
        object[] toStringParam = [_currentSpec.Type.GetFormat()];

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            string hex = (string)toStringFormat.Invoke(member.Value, toStringParam);

            sb
                .Append(nesting1Indent).Append(member.FullName).Append(" => \"").Append(hex).AppendLine("\",");
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                            _ => ToHexString(data)
                        }};
                        
                        [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                        static global::System.String ToHexString({0} data)
                        {{
                            global::System.Span<global::System.Char> destination = stackalloc global::System.Char[sizeof({1}) * 2];
                """, _currentSpec.FullName, _currentSpec.UnderlyingType);

        if (_currentSpec.OriginalUnderlyingType is not ("byte" or "sbyte"))
        {
            sb
                .Append(nesting1Indent)
                    .Append(_currentSpec.UnderlyingType).Append(" value = global::System.Runtime.CompilerServices.Unsafe.As<")
                    .Append(_currentSpec.FullName).Append(", ").Append(_currentSpec.UnderlyingType).AppendLine(">(ref data);");
        }
    }
}

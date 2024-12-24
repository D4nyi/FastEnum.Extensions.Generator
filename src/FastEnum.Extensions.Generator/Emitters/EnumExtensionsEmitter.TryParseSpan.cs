using System.Globalization;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddTryParseSpan(StringBuilder sb)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>
                    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="{0}" />.
                    /// This method using case-sensitive parsing.
                    /// </summary>
                    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
                    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
                    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
                    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<global::System.Char> value, out {0} result) =>
                        TryParseSpan(value, global::System.StringComparison.Ordinal, out result);
                    
                    /// <summary>
                    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="{0}" />.
                    /// This method using case-insensitive parsing.
                    /// A parameter specifies whether the operation is case-insensitive.
                    /// </summary>
                    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
                    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
                    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
                    public static global::System.Boolean TryParseIgnoreCase(global::System.ReadOnlySpan<global::System.Char> value, out {0} result) =>
                        TryParseSpan(value, global::System.StringComparison.OrdinalIgnoreCase, out result);


                """, _currentSpec.FullName);
    }

    private void AddTryParsePrivate(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.Boolean TryParseSpan(global::System.ReadOnlySpan<global::System.Char> value, global::System.StringComparison comparison, out {0} result)
                    {{
                        if (value.IsEmpty)
                        {{
                            result = default;
                            return false;
                        }}
                        
                        if (CheckIfNumber(value))
                        {{
                            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
                            return TryParseAsNumber(value, out result);
                        }}


                """, _currentSpec.FullName);

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(methodBodyIndent).Append("if (value.Equals(nameof(").Append(member.FullName).AppendLine(").AsSpan(), comparison))")
                .Append(methodBodyIndent).AppendLine("{")
                .Append(nesting1Indent).Append("result = ").Append(member.FullName).AppendLine(";")
                .Append(nesting1Indent).AppendLine("return true;")
                .Append(methodBodyIndent).AppendLine("}");
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                
                        global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
                        return TryParseByName(value, comparison == global::System.StringComparison.OrdinalIgnoreCase, out result);
                    }}
                    
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.Boolean CheckIfNumber(global::System.ReadOnlySpan<global::System.Char> value)
                    {{
                        global::System.Char c = value[0];
                
                        return global::System.Char.IsAsciiDigit(c) || c == '-' || c == '+';
                    }}
                    
                     private static global::System.Boolean TryParseAsNumber(global::System.ReadOnlySpan<global::System.Char> value, out {0} result)
                     {{
                        const global::System.Globalization.NumberStyles NumberStyle = global::System.Globalization.NumberStyles.AllowLeadingSign | global::System.Globalization.NumberStyles.AllowTrailingWhite;
                        global::System.Globalization.NumberFormatInfo numberFormat = global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
                        global::System.Boolean status = {1}.TryParse(value, NumberStyle, numberFormat, out var parseResult);
                
                        if (status)
                        {{
                            result = global::System.Runtime.CompilerServices.Unsafe.As<{1}, {0}>(ref parseResult);
                            return true;
                        }}
                        
                        result = default;
                        return false;
                    }}
                    
                    private static global::System.Boolean TryParseByName(global::System.ReadOnlySpan<global::System.Char> value, global::System.Boolean ignoreCase, out {0} result)
                    {{
                        global::System.String[] enumNames = _names;
                        {1}[] enumValues = _underlyingValues;
                        global::System.Boolean parsed = true;
                        {1} localResult = 0;
                
                        while (value.Length > 0)
                        {{
                            // Find the next separator
                            global::System.ReadOnlySpan<global::System.Char> subValue;
                            global::System.Int32 endIndex = value.IndexOf(',');
                            if (endIndex < 0)
                            {{
                                // No next separator; use the remainder as the next value
                                subValue = value.Trim();
                                value = default;
                            }}
                            else if (endIndex != value.Length - 1)
                            {{
                                // Found a separator before the last char
                                subValue = value[..endIndex].Trim();
                                value = value[(endIndex + 1)..];
                            }}
                            else
                            {{
                                // Last char was a separator, which is invalid
                                parsed = false;
                                break;
                            }}
                            
                            // Try to match this substring against each enum name
                            global::System.Boolean success = false;
                            if (ignoreCase)
                            {{
                                for (global::System.Int32 i = 0; i < enumNames.Length; i++)
                                {{
                                    if (subValue.Equals(enumNames[i], global::System.StringComparison.OrdinalIgnoreCase))
                                    {{
                                        localResult |= enumValues[i];
                                        success = true;
                                        break;
                                    }}
                                }}
                            }}
                            else
                            {{
                                for (global::System.Int32 i = 0; i < enumNames.Length; i++)
                                {{
                                    if (subValue.SequenceEqual(enumNames[i]))
                                    {{
                                        localResult |= enumValues[i];
                                        success = true;
                                        break;
                                    }}
                                }}
                            }}
                            
                            if (!success)
                            {{
                                parsed = false;
                                break;
                            }}
                        }}
                            
                        if (parsed)
                        {{
                            result = global::System.Runtime.CompilerServices.Unsafe.As<{1}, {0}>(ref localResult);
                            return true;
                        }}
                
                        result = default;
                        return false;
                    }}


                """, _currentSpec.FullName, _currentSpec.UnderlyingType);
    }
}

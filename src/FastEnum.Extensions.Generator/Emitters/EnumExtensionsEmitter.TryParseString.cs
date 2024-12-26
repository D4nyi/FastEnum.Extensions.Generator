using System.Globalization;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddTryParseString(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>
                    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="{0}" />.
                    /// This method using case-sensitive parsing.
                    /// </summary>
                    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
                    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
                    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
                    public static global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out {0} result)
                    {{
                        if (global::System.String.IsNullOrEmpty(value))
                        {{
                            result = default;
                            return false;
                        }}

                        global::System.ReadOnlySpan<global::System.Char> span = value.AsSpan().TrimStart();
                        if (span.IsEmpty)
                        {{
                            result = default;
                            return false;
                        }}

                        if (CheckIfNumber(span))
                        {{
                            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
                            return TryParseAsNumber(span, out result);
                        }}


                """, _currentSpec.FullName);

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(methodBodyIndent).Append("if (value.Equals(nameof(").Append(member.FullName).AppendLine(")))")
                .Append(methodBodyIndent).AppendLine("{")
                .Append(nesting1Indent).Append("result = ").Append(member.FullName).AppendLine(";")
                .Append(nesting1Indent).AppendLine("return true;")
                .Append(methodBodyIndent).AppendLine("}");
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """

                        return TryParseByName(value, false, out result);
                    }}

                    /// <summary>
                    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="{0}" />.
                    /// This method using case-insensitive parsing.
                    /// </summary>
                    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
                    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
                    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
                    public static global::System.Boolean TryParseIgnoreCase([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out {0} result)
                    {{
                        if (global::System.String.IsNullOrEmpty(value))
                        {{
                            result = default;
                            return false;
                        }}

                        global::System.ReadOnlySpan<global::System.Char> span = value.AsSpan().TrimStart();
                        if (span.IsEmpty)
                        {{
                            result = default;
                            return false;
                        }}

                        if (CheckIfNumber(span))
                        {{
                            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
                            return TryParseAsNumber(span, out result);
                        }}


                """, _currentSpec.FullName);

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(methodBodyIndent).Append("if (value.Equals(nameof(").Append(member.FullName)
                .Append("), global::System.StringComparison.OrdinalIgnoreCase").AppendLine("))")
                .Append(methodBodyIndent).AppendLine("{")
                .Append(nesting1Indent).Append("result = ").Append(member.FullName).AppendLine(";")
                .Append(nesting1Indent).AppendLine("return true;")
                .Append(methodBodyIndent).AppendLine("}");
        }

        sb.Append(
            """

                    return TryParseByName(value, true, out result);
                }


            """);
    }
}

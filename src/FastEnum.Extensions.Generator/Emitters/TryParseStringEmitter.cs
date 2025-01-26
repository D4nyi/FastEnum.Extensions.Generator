using System.Globalization;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class TryParseStringEmitter
{
    internal static void AddTryParseString(StringBuilder sb, EnumGenerationSpec spec)
    {
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
                        if (value is not null)
                        {{
                            return TryParseSpan(value.AsSpan(), false, out result);
                        }}

                        result = default;
                        return false;
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
                        if (value is not null)
                        {{
                            return TryParseSpan(value.AsSpan(), true, out result);
                        }}

                        result = default;
                        return false;
                    }}


                """, spec.FullName);
    }
}

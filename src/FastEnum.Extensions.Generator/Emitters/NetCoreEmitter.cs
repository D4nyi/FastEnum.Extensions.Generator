using System.Collections.Generic;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal sealed class NetCoreEmitter : SourceEmitter
{
    public NetCoreEmitter(in EnumExtensionsGenerator.EnumSourceGenerationContext sourceGenerationContext, List<EnumGenerationSpec> generationSpec, Framework framework)
        : base(sourceGenerationContext, generationSpec, framework) { }

    protected override void AddToStringFormat(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1 = Get(Indentation.Nesting1);

        sb
            .Append(methodIndent).Append(_currentSpec.Modifier).Append(" static global::System.String FastToString")
            .Append(_currentSpec.GenericTypeParameters)
            .Append("(this ").Append(_currentSpec.FullName).AppendLine(" value,")
            .AppendLine("#if NET7_0_OR_GREATER")
            .Append(methodBodyIndent).AppendLine("[global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.EnumFormat)]")
            .AppendLine("#endif")
            .Append(methodBodyIndent).Append("global::System.String? format)").Append(' ')
            .AppendLine(_currentSpec.GenericTypeConstraints)
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("if (global::System.String.IsNullOrEmpty(format))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1).AppendLine("return value.FastToString();")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("if (format.Length != 1) throw CreateInvalidFormatSpecifierException();")
            .Append(methodBodyIndent).AppendLine("global::System.Char formatChar = format[0];")
            .Append(methodBodyIndent).AppendLine("switch (formatChar | 0x20)")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1).Append("case 'g': return ").AppendLine((_currentSpec.IsFlags ? "FormatFlagNames(value);" : "value.FastToString();"))
            .Append(nesting1).Append("case 'd': return ");

        AddCast(sb, _currentSpec.FullName, _currentSpec.UnderlyingType).AppendLine(".ToString();")
            .Append(nesting1).AppendLine("case 'x': return FormatNumberAsHex(value);")
            .Append(nesting1).AppendLine("case 'f': return FormatFlagNames(value);")
            .Append(nesting1).AppendLine("default: throw CreateInvalidFormatSpecifierException();")
            .Append(methodBodyIndent).AppendLine("};")
            .Append(methodIndent).AppendLine("}")
            .AppendLine();
    }

    protected override void AddTryParseString(StringBuilder sb)
    {
        sb.Append(
$$"""

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="{{_currentSpec.FullName}}" />.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
        /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
        /// <param name="ignoreCase"><see langword="true"/> to read <see cref="{{_currentSpec.FullName}}" /> in case insensitive mode; <see langword="false"/> to read <see cref="{{_currentSpec.FullName}}" /> in case sensitive mode.</param>
        /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
        public static bool TryParse(
#if NETCOREAPP3_0_OR_GREATER
            [global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)]
#endif
            global::System.String? value, out {{_currentSpec.FullName}} result, global::System.Boolean ignoreCase = false)
        {
#if NETCOREAPP3_0_OR_GREATER
            return TryParse(value.AsSpan(), out result, false);
#else
            if (ignoreCase)
            {
                switch (value)
                {
""");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(
$$"""

                    case not null when value.Equals(nameof({{_currentSpec.FullName}}.{{member.Name}}), StringComparison.OrdinalIgnoreCase):
                        result = {{_currentSpec.FullName}}.{{member.Name}};
                        return true;
""");
        }

        sb.Append(
$$"""

                    case not null when global::System.Int32.TryParse(value, out global::System.Int32 numericResult):
                        result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.Int32, {{_currentSpec.FullName}}>(ref numericResult);
                        return true;
                    default:
                        result = default;
                        return false;
                }
            }

            switch (value)
            {
""");


        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(
$$"""

                case nameof({{_currentSpec.FullName}}.{{member.Name}}):
                    result = {{_currentSpec.FullName}}.{{member.Name}};
                    return true;
""");
        }

        sb.Append(
$$"""

                case not null when {{_currentSpec.UnderlyingType}}.TryParse(name, out {{_currentSpec.UnderlyingType}} numericResult):
                    result = global::System.Runtime.CompilerServices.Unsafe.As<{{_currentSpec.UnderlyingType}}, {{_currentSpec.FullName}}>(ref numericResult);
                    return true;
                default:
                    result = default;
                    return false;
            }
#endif
        }


""");

        sb.Append(
$$"""
#if NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Converts the span of chars representation of the name or numeric value of one or more enumerated constants to <see cref="{{_currentSpec.FullName}}" />.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
        /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
        /// <param name="ignoreCase"><see langword="true"/> to read <see cref="{{_currentSpec.FullName}}" /> in case insensitive mode; <see langword="false"/> to read <see cref="{{_currentSpec.FullName}}" /> in case sensitive mode.</param>
        /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
        public static global::System.Boolean TryParse(in global::System.ReadOnlySpan<global::System.Char> value, out {{_currentSpec.FullName}} result, global::System.Boolean ignoreCase = false)
        {
            if (ignoreCase)
            {
                switch (value)
                {
""");


        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(
$$"""

                    case global::System.ReadOnlySpan<global::System.Char> when value.Equals(nameof({{_currentSpec.FullName}}.{{member.Name}}).AsSpan(), global::System.StringComparison.OrdinalIgnoreCase):
                        result = {{_currentSpec.FullName}}.{{member.Name}};
                        return true;
""");
        }

        sb.Append(
$$"""

                    case global::System.ReadOnlySpan<global::System.Char> when {{_currentSpec.UnderlyingType}}.TryParse(value, out {{_currentSpec.UnderlyingType}} numericResult):
                        result = global::System.Runtime.CompilerServices.Unsafe.As<{{_currentSpec.UnderlyingType}}, {{_currentSpec.FullName}}>(ref numericResult);
                        return true;
                    default:
                        result = default;
                        return false;
                }
            }

            switch (value)
            {
""");


        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(
$$"""

                case global::System.ReadOnlySpan<global::System.Char> when value.Equals(nameof({{_currentSpec.FullName}}.{{member.Name}}).AsSpan(), global::System.StringComparison.Ordinal):
                    result = {{_currentSpec.FullName}}.{{member.Name}};
                    return true;
""");
        }

        sb.Append(
$$"""

                case global::System.ReadOnlySpan<global::System.Char> when {{_currentSpec.UnderlyingType}}.TryParse(value, out {{_currentSpec.UnderlyingType}} numericResult):
                    result = global::System.Runtime.CompilerServices.Unsafe.As<{{_currentSpec.UnderlyingType}}, {{_currentSpec.FullName}}>(ref numericResult);
                    return true;
                default:
                    result = default;
                    return false;
            }
        }
#endif


""");
    }

    protected override void AddFormatNumberAsHexBufferGeneration(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .Append(methodBodyIndent)
            .Append("global::System.Span<global::System.Char> destination = stackalloc global::System.Char[sizeof(")
            .Append(_currentSpec.UnderlyingType)
            .AppendLine(") * 2];");

        if (_currentSpec.OriginalUnderlyingType is not "byte" or "sbyte")
        {
            sb
                .Append(methodBodyIndent)
                    .Append(_currentSpec.UnderlyingType).Append(" value = global::System.Runtime.CompilerServices.Unsafe.As<")
                    .Append(_currentSpec.FullName).Append(", ").Append(_currentSpec.UnderlyingType).AppendLine(">(ref data);");
        }
    }

    protected override void AddToCharsBufferDefinition(StringBuilder sb)
    {
        sb.AppendLine("private static void ToCharsBuffer(global::System.Byte value, global::System.Span<global::System.Char> buffer, global::System.Int32 startingIndex)");
    }

    protected override StringBuilder AddCast(StringBuilder sb, string enumName, string underlyingType)
    {
        return sb
            .Append("global::System.Runtime.CompilerServices.Unsafe.As<").Append(enumName)
            .Append(", ").Append(underlyingType).Append(">(ref value)");
    }
}
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
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).AppendLine("/// Converts the value of this instance to its equivalent string representation.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).AppendLine("/// <returns>The string representation of the value of this instance.</returns>")
            .Append(methodIndent).Append(_currentSpec.Modifier).Append(" static global::System.String FastToString(this ")
                .Append(_currentSpec.FullName).AppendLine(" value) => value switch")
            .Append(methodIndent).AppendLine("{");

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
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1 = Get(Indentation.Nesting1);
        string nesting2 = Get(Indentation.Nesting2);
        string nesting3 = Get(Indentation.Nesting3);

        sb
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent)
            .AppendLine(
                "/// Converts the value of this instance to its equivalent string representation using the specified format.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).AppendLine("/// <param name=\"format\">A format string.</param>")
            .Append(methodIndent)
            .AppendLine(
                "/// <returns>The string representation of the value of this instance as specified by format.</returns>")
            .Append(methodIndent)
            .AppendLine(
                "/// <exception cref=\"global::System.FormatException\"><paramref name=\"format\"/> contains an invalid specification.</exception>")
            .Append(methodIndent).Append(_currentSpec.Modifier)
            .Append(" static global::System.String FastToString(this ")
            .Append(_currentSpec.FullName).AppendLine(" value,")
            .AppendLine("#if NET7_0_OR_GREATER")
            .Append(methodBodyIndent)
            .AppendLine(
                "[global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.EnumFormat)]")
            .AppendLine("#endif")
            .Append(methodBodyIndent).AppendLine("global::System.String? format)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("if (global::System.String.IsNullOrEmpty(format))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1).AppendLine("return value.FastToString();")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent)
            .AppendLine("if (format.Length != 1) throw CreateInvalidFormatSpecifierException();")
            .Append(methodBodyIndent).AppendLine("global::System.Char formatChar = format[0];")
            .Append(methodBodyIndent).AppendLine("switch (formatChar | 0x20)")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1).AppendLine("case 'g': return value.FastToString();")
            .Append(nesting1).Append("case 'd': return ").AddCast(_currentSpec.FullName, _currentSpec.UnderlyingType)
            .AppendLine(".ToString();")
            .Append(nesting1).AppendLine("case 'x': return FormatNumberAsHex(value);")
            .Append(nesting1).AppendLine("case 'f':")
            .Append(nesting2).AppendLine("global::System.String? result = FormatFlagNames(value);")
            .Append(nesting2).AppendLine("if (result is null)")
            .Append(nesting3).AppendLine("goto case 'd';")
            .AppendLine()
            .Append(nesting2).AppendLine("return result;")
            .Append(nesting1).AppendLine("default: throw CreateInvalidFormatSpecifierException();")
            .Append(methodBodyIndent).AppendLine("}")
            .Append(methodIndent).AppendLine("}")
            .AppendLine();

        // .Append(methodBodyIndent).AppendLine("return (formatChar | 0x20) switch")
        // .Append(methodBodyIndent).AppendLine("{")
        // .Append(nesting1).Append("'g' => ").AppendLine((_currentSpec.IsFlags ? "FormatFlagNames(value)," : "value.FastToString(),"))
        // .Append(nesting1).Append("'d' => ")
        //     .AddCast(_currentSpec.FullName, _currentSpec.UnderlyingType).AppendLine(".ToString(),")
        // .Append(nesting1).AppendLine("'x' => FormatNumberAsHex(value),")
        // .Append(nesting1).AppendLine("'f' => FormatFlagNames(value),")
        // .Append(nesting1).AppendLine("_ => throw CreateInvalidFormatSpecifierException()")
        // .Append(methodBodyIndent).AppendLine("};")
        // .Append(methodIndent).AppendLine("}")
        // .AppendLine();
    }

    private void AddFormatFlagNames(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .Append(methodIndent).Append("private static global::System.String FormatFlagNames(").Append(_currentSpec.FullName).AppendLine(" value) => value switch")
            .Append(methodIndent).AppendLine("{");

        if (!_currentSpec.Members[0].Value.Equals(0))
        {
            sb.Append(methodBodyIndent).AppendLine("0 => \"0\",");
        }
        
        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => nameof(")
                .Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(methodBodyIndent).AppendLine("_ => WriteMultipleFoundFlagsNames(value)")
            .Append(methodIndent).AppendLine("};")
            .AppendLine();
    }

    private void AddFormatAsHexHelper(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .Append(methodIndent).AppendLine(AggressiveInliningAttribute)
            .Append(methodIndent).Append("private static global::System.String FormatNumberAsHex(")
                .Append(_currentSpec.FullName).AppendLine(" data)")
            .Append(methodIndent).AppendLine("{");

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
                sb.Append(nesting1Indent).Append("ToCharsBuffer((global::System.Byte)");
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
                .Append(nesting1Indent).Append("ToCharsBuffer((")
                .Append(_currentSpec.UnderlyingType).AppendLine(")data, destination, 0);");
        }

        sb
            .AppendLine()
            .Append(nesting1Indent).AppendLine("return new global::System.String(destination);")
            .Append(methodBodyIndent).AppendLine("}")
            .Append(methodIndent).AppendLine("}")
            .AppendLine();
    }

    private void AddFormatNumberAsHexBufferGeneration(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .Append(methodBodyIndent).AppendLine("return data switch")
            .Append(methodBodyIndent).AppendLine("{");

        var membersType = _currentSpec.Members[0].Value.GetType(); // an enum has only one backing type
        var toStringFormat = Helpers.GetToStringFormat(membersType);
        object[] toStringParam = { _currentSpec.Type.GetFormat() };

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            string hex = (string)toStringFormat.Invoke(member.Value, toStringParam);

            sb
                .Append(nesting1Indent).Append(member.FullName).Append(" => \"").Append(hex).AppendLine("\",");
        }

        sb
            .Append(nesting1Indent).AppendLine("_ => ToHexString(data)")
            .Append(methodBodyIndent).AppendLine("};")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine(AggressiveInliningAttribute)
            .Append(methodBodyIndent)
                .Append("static global::System.String ToHexString(").Append(_currentSpec.FullName).AppendLine(" data)")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent)
            .Append("global::System.Span<global::System.Char> destination = stackalloc global::System.Char[sizeof(")
            .Append(_currentSpec.UnderlyingType)
            .AppendLine(") * 2];");

        if (_currentSpec.OriginalUnderlyingType is not ("byte" or "sbyte"))
        {
            sb
                .Append(nesting1Indent)
                    .Append(_currentSpec.UnderlyingType).Append(" value = global::System.Runtime.CompilerServices.Unsafe.As<")
                    .Append(_currentSpec.FullName).Append(", ").Append(_currentSpec.UnderlyingType).AppendLine(">(ref data);");
        }
    }
}

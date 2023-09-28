using System.Collections.Generic;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal sealed class Net48Emitter : SourceEmitter
{
    public Net48Emitter(in EnumExtensionsGenerator.EnumSourceGenerationContext sourceGenerationContext, List<EnumGenerationSpec> generationSpec, Framework framework)
        : base(sourceGenerationContext, generationSpec, framework) { }

    protected override void AddToStringFormat(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .Append(methodIndent).Append(_currentSpec.Modifier).Append(" static global::System.String FastToString")
            .Append(_currentSpec.GenericTypeParameters).Append("(this ").Append(_currentSpec.FullName)
                .AppendLine(" value, global::System.String format)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("if (global::System.String.IsNullOrEmpty(format))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).AppendLine("return value.FastToString();")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("if (format.Length != 1) throw CreateInvalidFormatSpecifierException();")
            .Append(methodBodyIndent).AppendLine("global::System.Char formatChar = format[0];")
            .Append(methodBodyIndent).AppendLine("switch (formatChar | 0x20)")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).Append("case 'g': return ").Append(_currentSpec.IsFlags ? "FormatFlagNames(value)" : "value.FastToString()").AppendLine(";")
            .Append(nesting1Indent).Append("case 'd': return (");

        AddCast(sb, _currentSpec.FullName, _currentSpec.UnderlyingType)
            .AppendLine(").ToString();")
            .Append(nesting1Indent).AppendLine("case 'x': return FormatNumberAsHex(value);")
            .Append(nesting1Indent).AppendLine("case 'f': return FormatFlagNames(value);")
            .Append(nesting1Indent).AppendLine("default: throw CreateInvalidFormatSpecifierException();")
            .Append(methodBodyIndent).AppendLine("};")
            .Append(methodIndent).AppendLine("}")
            .AppendLine();
    }

    protected override void AddTryParseString(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);
        string nesting2Indent = Get(Indentation.Nesting2);

        sb
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).AppendLine("/// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref=\"ToStringExample.Options\" />.<br/>")
            .Append(methodIndent).AppendLine("/// A parameter specifies whether the operation is case-insensitive.")
            .Append(methodIndent).AppendLine("/// <param name=\"value\">The string representation of the name or numeric value of one or more enumerated constants.</param>")
            .Append(methodIndent).AppendLine("/// <param name=\"result\">When this method returns <see langword=\"true\"/>, an object containing an enumeration constant representing the parsed value.</param>")
            .Append(methodIndent)
                .Append("/// <param name=\"ignoreCase\"><see langword=\"true\"/> to read <see cref=\"").Append(_currentSpec.CommentCompatibleFullName)
                .Append("\" /> in case insensitive mode; <see langword=\"false\"/> to read <see cref=\"").Append(_currentSpec.CommentCompatibleFullName)
                .AppendLine("\" /> in case sensitive mode.</param>")
            .Append(methodIndent)
                .Append("public static bool TryParse(global::System.String value, out ").Append(_currentSpec.FullName)
                .Append("result, global::System.Boolean ignoreCase = false)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("if (String.IsNullOrEmpty(value))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).AppendLine("result = default;")
            .Append(nesting1Indent).AppendLine("return false;")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .AppendLine(methodBodyIndent).AppendLine("if (ignoreCase)")
            .Append(methodBodyIndent).AppendLine("{");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(nesting1Indent).Append("if (value.Equals(nameof(")
                    .Append(_currentSpec.FullName).Append('.').Append(member.Name)
                    .AppendLine("), global::System.StringComparison.OrdinalIgnoreCase))")
                .Append(nesting2Indent).Append("result = ")
                    .Append(_currentSpec.FullName).Append('.').Append(member.Name).AppendLine(";")
                .Append(nesting2Indent).AppendLine("return true;")
                .Append(nesting1Indent).AppendLine("}");
        }

        sb
            .Append(nesting1Indent).AppendLine("global::System.Int32 result2;")
            .Append(nesting1Indent).AppendLine("if (global::System.Int32.TryParse(value, out result2))")
            .Append(nesting1Indent).AppendLine("{")
            .Append(nesting2Indent).AppendLine("result = (").Append(_currentSpec.FullName).AppendLine(")result2;")
            .Append(nesting2Indent).AppendLine("return true;")
            .Append(nesting1Indent).AppendLine("}")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("switch (value)")
            .Append(methodBodyIndent).AppendLine("{");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(nesting1Indent).Append("case nameof(")
                    .Append(_currentSpec.FullName).Append('.').Append(member.Name)
                    .AppendLine("):")
                .Append(nesting2Indent).Append("result = ")
                    .Append(_currentSpec.FullName).Append('.').Append(member.Name).AppendLine(";")
                .Append(nesting2Indent).AppendLine("return true;");
        }

        string nesting3Indent = Get(Indentation.Nesting3);
        string nesting4Indent = Get(Indentation.Nesting4);

        sb
            .Append(nesting1Indent).AppendLine("default:")
            .Append(nesting2Indent).AppendLine("{")
            .Append(nesting3Indent).AppendLine("int result3;")
            .Append(nesting3Indent).AppendLine("if (global::System.Int32.TryParse(value, out result3))")
            .Append(nesting3Indent).AppendLine("{")
            .Append(nesting4Indent).AppendLine("result = (").Append(_currentSpec.FullName).AppendLine(")result3;")
            .Append(nesting4Indent).AppendLine("return true;")
            .AppendLine()
            .Append(nesting3Indent).AppendLine("result = default;")
            .Append(nesting3Indent).AppendLine("return false;")
            .Append(nesting2Indent).AppendLine("}")
            .Append(methodBodyIndent).AppendLine("}")
            .Append(methodIndent).AppendLine("}");
    }

    protected override void AddFormatNumberAsHexBufferGeneration(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .Append(methodBodyIndent)
            .Append("global::System.Char[] destination = new global::System.Char[sizeof(")
            .Append(_currentSpec.UnderlyingType)
            .AppendLine(") * 2];");

        if (_currentSpec.OriginalUnderlyingType is not "byte" or "sbyte")
        {
            sb
                .Append(methodBodyIndent).Append(_currentSpec.UnderlyingType)
                .Append(" value = (").Append(_currentSpec.UnderlyingType)
                .AppendLine(")data;");
        }
    }

    protected override void AddToCharsBufferDefinition(StringBuilder sb)
    {
        sb.AppendLine("private static void ToCharsBuffer(global::System.Byte value, global::System.Char[] buffer, global::System.Int32 startingIndex)");
    }

    protected override StringBuilder AddCast(StringBuilder sb, string enumName, string underlyingType)
    {
        return sb.Append('(').Append(underlyingType).Append(')').Append("value");
    }
}
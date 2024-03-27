using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddTryParseString(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);

        sb
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).Append("/// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.")
            .Append(methodIndent).AppendLine("/// This method using case-sensitive parsing.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).AppendLine("/// <param name=\"value\">The string representation of the name or numeric value of one or more enumerated constants.</param>")
            .Append(methodIndent).AppendLine("/// <param name=\"result\">When this method returns <see langword=\"true\"/>, an object containing an enumeration constant representing the parsed value.</param>")
            .Append(methodIndent).AppendLine("/// <returns><see langword=\"true\"/> if the conversion succeeded; <see langword=\"false\"/> otherwise.</returns>")
            .Append(methodIndent).Append("public static global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out ")
                .Append(_currentSpec.FullName).AppendLine(" result)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("if (global::System.String.IsNullOrEmpty(value))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).AppendLine("result = default;")
            .Append(nesting1Indent).AppendLine("return false;")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("if (CheckIfNumber(value))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).AppendLine("global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);")
            .Append(nesting1Indent).AppendLine("return TryParseAsNumber(value, out result);")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine();

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
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("return TryParseByName(value, false, out result);")
            .Append(methodIndent).AppendLine("}")
            .AppendLine()
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).Append("/// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.")
            .Append(methodIndent).AppendLine("/// This method using case-insensitive parsing.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).AppendLine("/// <param name=\"value\">The string representation of the name or numeric value of one or more enumerated constants.</param>")
            .Append(methodIndent).AppendLine("/// <param name=\"result\">When this method returns <see langword=\"true\"/>, an object containing an enumeration constant representing the parsed value.</param>")
            .Append(methodIndent).AppendLine("/// <returns><see langword=\"true\"/> if the conversion succeeded; <see langword=\"false\"/> otherwise.</returns>")
            .Append(methodIndent).Append("public static global::System.Boolean TryParseIgnoreCase([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out ")
                .Append(_currentSpec.FullName).AppendLine(" result)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("if (global::System.String.IsNullOrEmpty(value))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).AppendLine("result = default;")
            .Append(nesting1Indent).AppendLine("return false;")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("if (CheckIfNumber(value))")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).AppendLine("global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);")
            .Append(nesting1Indent).AppendLine("return TryParseAsNumber(value, out result);")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine();

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

        sb
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("return TryParseByName(value, true, out result);")
            .Append(methodIndent).AppendLine("}").AppendLine();
    }

}

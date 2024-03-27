using System.Text;

using FastEnum.Extensions.Generator.Emitters;
using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private const string AggressiveInliningAttribute =
        "[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]";

    private const string NoInliningAttribute =
        "[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300";
    
    private void AddFileAndClassHeader(StringBuilder sb)
    {
        sb.AppendLine(Constants.FileHeader);

        if (!_currentSpec.IsGlobalNamespace)
        {
            sb
                .Append("namespace ")
                .AppendLine(_currentSpec.Namespace)
                .AppendLine("{");
        }

        string classHeaderIndent = Get(Indentation.Class);

        sb
            .Append(classHeaderIndent).AppendLine("/// <summary>")
            .Append(classHeaderIndent).Append("/// Extension methods for <see cref=\"")
            .Append(_currentSpec.FullName).AppendLine("\" />")
            .Append(classHeaderIndent).AppendLine("/// </summary>")
            .Append(classHeaderIndent)
                .Append("[global::System.CodeDom.Compiler.GeneratedCode(\"FastEnum.Helpers.Generator.EnumToStringGenerator\", \"")
                .Append(Assembly.Version).AppendLine("\")]")
            .Append(classHeaderIndent).Append(_currentSpec.Modifier).Append(" static class ").Append(_currentSpec.Name).AppendLine("Extensions")
            .Append(classHeaderIndent).AppendLine("{");
    }

    private void AddFieldsAndGetMethods(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .Append(methodIndent).Append("private static readonly ").Append(_currentSpec.UnderlyingType).AppendLine("[] _underlyingValues =")
            .Append(methodIndent).AppendLine("{");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(methodBodyIndent).Append(member.Value).AppendLine(",");
        }

        sb
            .Append(methodIndent).AppendLine("};")
            .AppendLine()
            .Append(methodIndent).Append("private static readonly ").Append(_currentSpec.FullName).AppendLine("[] _values =")
            .Append(methodIndent).AppendLine("{");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(methodBodyIndent).Append(member.FullName).AppendLine(",");
        }

        sb
            .Append(methodIndent).AppendLine("};")
            .AppendLine()
            .Append(methodIndent).AppendLine("private static readonly global::System.String[] _names =")
            .Append(methodIndent).AppendLine("{");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(methodBodyIndent).Append("nameof(").Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(methodIndent).AppendLine("};")
            .AppendLine()
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).AppendLine("/// The number of members in the enum.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).Append("public const global::System.Int32 MembersCount = ")
                .Append(_currentSpec.Members.Length).AppendLine(";")
            .AppendLine() // MembersCount
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).Append("/// Retrieves an array of the values of the members defined in <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).Append("/// <returns>An array of the values defined in <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.</returns>")
            .Append(methodIndent).Append("public static ").Append(_currentSpec.FullName)
                .AppendLine("[] GetValues() => _values;")
            .AppendLine() // GetValues
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).Append("/// Retrieves an array of the underlying vales of the members defined in <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).Append("/// <returns>An array of the underlying values defined in <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.</returns>")
            .Append(methodIndent).Append("public static ").Append(_currentSpec.UnderlyingType)
                .AppendLine("[] GetUnderlyingValues() => _underlyingValues;")
            .AppendLine() // GetUnderlyingValues
            .Append(methodIndent).AppendLine("/// <summary>")
            .Append(methodIndent).Append("/// Retrieves an array of the names of the members defined in <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.")
            .Append(methodIndent).AppendLine("/// </summary>")
            .Append(methodIndent).Append("/// <returns>An array of the names of the members defined in <see cref=\"")
                .Append(_currentSpec.FullName).AppendLine("\" />.</returns>")
            .Append(methodIndent).AppendLine("public static global::System.String[] GetNames() => _names;")
            .AppendLine(); // GetNames
    }

    private void AddHasFlag(StringBuilder sb)
    {
        string methodIndentation = Get(Indentation.Method);

        sb
            .Append(methodIndentation).AppendLine("/// <summary>Determines whether one or more bit fields are set in the current instance.</summary>")
            .Append(methodIndentation).AppendLine("/// <param name=\"instance\">The instance in which the the flags are searched.</param>")
            .Append(methodIndentation).AppendLine("/// <param name=\"flags\">The flags that will be looked up in the instance.</param>")
            .Append(methodIndentation).AppendLine("/// <returns><see langword=\"true\"/> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <see langword=\"false\"/>.</returns>")
            .Append(methodIndentation).Append("public static global::System.Boolean HasFlag(this ")
                .Append(_currentSpec.FullName).Append(" instance, ").Append(_currentSpec.FullName)
                .AppendLine(" flags) => (instance & flags) == flags;")
            .AppendLine();
    }

    private void AddIsDefined(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .Append(methodIndent).AppendLine("/// <summary>Returns a <see langword=\"global::System.Boolean\"/> telling whether the given enum value exists in the enumeration.</summary>")
            .Append(methodIndent).AppendLine("/// <param name=\"value\">The value to check if it's defined</param>")
            .Append(methodIndent).AppendLine("/// <returns><see langword=\"true\"/> if the value exists in the enumeration, <see langword=\"false\"/> otherwise</returns>")
            .Append(methodIndent).Append("public static global::System.Boolean IsDefined(this ").Append(_currentSpec.FullName).AppendLine(" value) => value switch")
            .Append(methodIndent).Append('{');

        int length = _currentSpec.Members.Length;
        int last = length - 1;
        for (int i = 0; i < _currentSpec.Members.Length; i++)
        {
            if (i % 3 == 0)
            {
                sb.AppendLine().Append(methodBodyIndent);
            }

            sb.Append(_currentSpec.Members[i].FullName);

            if (i != last)
            {
                sb.Append(" or ");
            }
        }

        sb
            .AppendLine(" => true,")
            .Append(methodBodyIndent).AppendLine("_ => false")
            .Append(methodIndent).AppendLine("};")
            .AppendLine();
    }

    private void AddPrivateHelperMethods(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        string nesting1Indent = Get(Indentation.Nesting1);
        string nesting2Indent = Get(Indentation.Nesting2);
        string nesting3Indent = Get(Indentation.Nesting3);

        sb
            .Append(methodIndent).Append("private static global::System.String? WriteMultipleFoundFlagsNames(")
                .Append(_currentSpec.FullName).AppendLine(" value)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).Append(_currentSpec.UnderlyingType).Append(" resultValue = ")
                .AddCast(_currentSpec.FullName, _currentSpec.UnderlyingType).AppendLine(";")
            .Append(methodBodyIndent).AppendLine("global::System.Int32 index = 0;")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("global::System.Text.StringBuilder sb = new global::System.Text.StringBuilder(MembersCount * 5); // TODO: Calc the longest element length or average element length")
            .Append(methodBodyIndent).AppendLine("while (index < _underlyingValues.Length)")
            .Append(methodBodyIndent).AppendLine("{")
            .Append(nesting1Indent).Append(_currentSpec.UnderlyingType).AppendLine(" currentValue = _underlyingValues[index];")
            .Append(nesting1Indent).AppendLine("if (currentValue != 0 && (resultValue & currentValue) == currentValue)")
            .Append(nesting1Indent).AppendLine("{")
            .Append(nesting2Indent).AddCorrectBitwiseOperation(_currentSpec.OriginalUnderlyingType)
            .Append(nesting2Indent).AppendLine("global::System.String currentValueName = currentValue switch")
            .Append(nesting2Indent).AppendLine("{");

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb
                .Append(nesting3Indent).Append(member.Value)
                .Append(" => nameof(").Append(member.FullName).AppendLine("),");
        }

        sb
            .Append(nesting3Indent).AppendLine("_ => throw CreateUnreachableException(resultValue)")
            .Append(nesting2Indent).AppendLine("};")
            .Append(nesting2Indent).AppendLine("sb.Append(currentValueName);")
            .AppendLine()
            .Append(nesting2Indent).AppendLine("if (resultValue == 0) break;")
            .Append(nesting2Indent).AppendLine("sb.Append(\", \");")
            .Append(nesting1Indent).AppendLine("}")
            .AppendLine()
            .Append(nesting1Indent).AppendLine("index++;")
            .Append(methodBodyIndent).AppendLine("}")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("// If we exhausted looking through all the values and we still have")
            .Append(methodBodyIndent).AppendLine("// a non-zero result, we couldn't match the result to only named values.")
            .Append(methodBodyIndent).AppendLine("// In that case, we return null and let the call site just generate")
            .Append(methodBodyIndent).AppendLine("// a string for the integral value.")
            .Append(methodBodyIndent).AppendLine("return resultValue == 0 ? sb.ToString() : null;")
            .Append(methodIndent).AppendLine("}")
            .AppendLine()
            .Append(methodIndent).AppendLine("[global::System.Security.SecuritySafeCriticalAttribute, global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]")
            .Append(methodIndent).AppendLine("private static void ToCharsBuffer(global::System.Byte value, global::System.Span<global::System.Char> buffer, global::System.Int32 startingIndex)")
            .Append(methodIndent).AppendLine("{")
            .Append(methodBodyIndent).AppendLine("global::System.UInt32 difference = ((value & 0xF0U) << 4) + (value & 0x0FU) - 0x8989U;")
            .Append(methodBodyIndent).AppendLine("global::System.UInt32 packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;")
            .AppendLine()
            .Append(methodBodyIndent).AppendLine("buffer[startingIndex + 1] = (global::System.Char)(packedResult & 0xFFU);")
            .Append(methodBodyIndent).AppendLine("buffer[startingIndex] = (global::System.Char)(packedResult >> 8);")
            .Append(methodIndent).AppendLine("}")
            .AppendLine()
            .Append(methodIndent).AppendLine(NoInliningAttribute)
            .Append(methodIndent).AppendLine("private static global::System.FormatException CreateInvalidFormatSpecifierException() =>")
            .Append(methodBodyIndent).AppendLine("new global::System.FormatException(\"Format string can be only \\\"G\\\", \\\"g\\\", \\\"X\\\", \\\"x\\\", \\\"F\\\", \\\"f\\\", \\\"D\\\" or \\\"d\\\".\");")
            .AppendLine()
            .Append(methodIndent).AppendLine(NoInliningAttribute)
            .AppendLine("#if NET7_0_OR_GREATER")
            .Append(methodIndent).AppendLine("private static global::System.Diagnostics.UnreachableException CreateUnreachableException(global::System.Int32 value) =>")
            .Append(methodBodyIndent).AppendLine("new global::System.Diagnostics.UnreachableException($\"Tried to access a non-existent value: {value}\");")
            .AppendLine("#else")
            .Append(methodIndent).AppendLine("private static global::System.IndexOutOfRangeException CreateUnreachableException(global::System.Int32 value) =>")
            .Append(methodBodyIndent).AppendLine("new global::System.IndexOutOfRangeException($\"Tried to access a non-existent value: {value}\");")
            .AppendLine("#endif")
            .AppendLine()
            .Append(methodIndent).AppendLine(NoInliningAttribute)
            .Append(methodIndent).AppendLine("private static global::System.ArgumentException CreateInvalidEmptyParseArgument() =>")
            .Append(methodBodyIndent).AppendLine("new global::System.ArgumentException(\"Must specify valid information for parsing in the string.\", \"value\");")
            .AppendLine()
            .Append(methodIndent).AppendLine(NoInliningAttribute)
            .Append(methodIndent).AppendLine("private static global::System.ArgumentException CreateValueNotFoundArgument(global::System.ReadOnlySpan<char> originalValue) =>")
            .Append(methodBodyIndent).AppendLine("new global::System.ArgumentException(global::System.String.Format(global::System.Globalization.CultureInfo.InvariantCulture, \"Requested value '{0}' was not found.\", originalValue.ToString()));");
    }

    private void CloseClassAndNamespace(StringBuilder sb)
    {
        string indent = Get(Indentation.Class);
        sb
            .Append(indent)
            .AppendLine("}");

        if (!_currentSpec.IsGlobalNamespace)
        {
            sb.Append('}');
        }
    }
}

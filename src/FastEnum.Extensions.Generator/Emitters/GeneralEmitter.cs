using System.Globalization;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class GeneralEmitter
{
    internal static void AddFileAndClassHeader(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb.AppendLine(Constants.FileHeader);

        if (!spec.IsGlobalNamespace)
        {
            sb
                .Append("namespace ").Append(spec.Namespace).AppendLine(";").AppendLine();
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                /// <summary>
                /// Extension methods for <see cref="{0}" />
                /// </summary>
                [global::System.CodeDom.Compiler.GeneratedCodeAttribute("{1}", "{2}")]
                {3} static class {4}Extensions
                {{
                """, spec.FullName, Constants.EnumExtensionsGenerator, Constants.Version, spec.Modifier, spec.Name);
    }

    internal static void AddFieldsAndGetMethods(StringBuilder sb, EnumGenerationSpec spec)
    {
        string methodBodyIndent = Indentation.MethodBody.Get();

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """

                    private static readonly {0}[] _underlyingValues =
                    {{

                """, spec.UnderlyingType);

        foreach (EnumMemberSpec member in spec.Members)
        {
            sb.Append(methodBodyIndent).Append(member.Value).AppendLine(",");
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    }};

                    private static readonly {0}[] _values =
                    {{

                """, spec.FullName);

        foreach (EnumMemberSpec member in spec.Members)
        {
            sb.Append(methodBodyIndent).Append(member.FullName).AppendLine(",");
        }

        sb
            .Append(
                """
                    };

                    private static readonly global::System.String[] _names =
                    {

                """);

        foreach (EnumMemberSpec member in spec.Members)
        {
            sb.Append(methodBodyIndent).Append("nameof(").Append(member.FullName).AppendLine("),");
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    }};

                    /// <summary>
                    /// The number of members in the enum.
                    /// </summary>
                    public const global::System.Int32 MembersCount = {0};

                    /// <summary>
                    /// Retrieves an array of the values of the members defined in <see cref="{1}" />.
                    /// </summary>
                    /// <returns>An array of the values defined in <see cref="{1}" />.</returns>
                    public static {1}[] GetValues() => _values;

                    /// <summary>
                    /// Retrieves an array of the underlying vales of the members defined in <see cref="{1}" />
                    /// </summary>
                    /// <returns>An array of the underlying values defined in <see cref="{1}" />.</returns>
                    public static {2}[] GetUnderlyingValues() => _underlyingValues;

                    /// <summary>
                    /// Retrieves an array of the names of the members defined in <see cref="{1}" />
                    /// </summary>
                    /// <returns>An array of the names of the members defined in <see cref="{1}" />.</returns>
                    public static global::System.String[] GetNames() => _names;


                """, spec.Members.Length, spec.FullName, spec.UnderlyingType);
    }

    internal static void AddHasFlag(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
                    /// <param name="instance">The instance in which the flags are searched.</param>
                    /// <param name="flags">The flags that will be looked up in the instance.</param>
                    /// <returns><see langword="true"/> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <see langword="false"/>.</returns>
                    public static global::System.Boolean HasFlag(this {0} instance, {1} flags) => (instance & flags) == flags;


                """, spec.FullName, spec.FullName);
    }

    internal static void AddIsDefined(StringBuilder sb, EnumGenerationSpec spec)
    {
        string methodBodyIndent = Indentation.MethodBody.Get();

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Returns a <see langword="global::System.Boolean"/> telling whether the given enum value exists in the enumeration.</summary>
                    /// <param name="value">The value to check if it's defined</param>
                    /// <returns><see langword="true"/> if the value exists in the enumeration, <see langword="false"/> otherwise</returns>
                    public static global::System.Boolean IsDefined(this {0} value) => value switch
                    {{
                """, spec.FullName);

        int length = spec.Members.Length;
        int last = length - 1;
        for (int i = 0; i < length; i++)
        {
            if (i % 3 == 0)
            {
                sb.AppendLine().Append(methodBodyIndent);
            }

            sb.Append(spec.Members[i].FullName);

            if (i != last)
            {
                sb.Append(" or ");
            }
        }

        sb.Append(
            """
             => true,
                    _ => false
                };


            """);
    }

    internal static void AddPrivateHelperMethods(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    private static global::System.String? ProcessMultipleFlagsNames({0} value)
                    {{
                        {1} resultValue = global::System.Runtime.CompilerServices.Unsafe.As<{0}, {1}>(ref value);

                        global::System.Span<global::System.Int32> foundItems = stackalloc global::System.Int32[{2}];
                        if (!TryFindFlagsNames(resultValue, foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount))
                        {{
                            return null;
                        }}

                        foundItems = foundItems[..foundItemsCount];

                        global::System.Int32 length = checked(resultLength + 2 * (foundItemsCount - 1)); // ", ".Length == 2

                        global::System.Span<global::System.Char> destination = stackalloc global::System.Char[length];

                        WriteMultipleFoundFlagsNames(foundItems, destination);

                        return new global::System.String(destination);
                    }}

                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.Boolean TryFindFlagsNames({1} resultValue, global::System.Span<global::System.Int32> foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount)
                    {{
                        // Now look for multiple matches, storing the indices of the values into our span.
                        resultLength = 0;
                        foundItemsCount = 0;

                        global::System.Int32 index = MembersCount - 1;
                        {1}[] values = _underlyingValues;
                        global::System.String[] names = _names;

                        while (true)
                        {{
                            {1} currentValue = values[index];
                            if (index == 0 && currentValue == 0)
                            {{
                                break;
                            }}

                            if ((resultValue & currentValue) == currentValue)
                            {{

                """, spec.FullName, spec.UnderlyingType, spec.Members.Length > 64 ? 64 : spec.Members.Length)
            .AddCorrectBitwiseOperation(spec.UnderlyingType)
            .Append(
                """
                                foundItems[foundItemsCount] = index;
                                foundItemsCount++;
                                resultLength = checked(resultLength + names[index].Length);
                                if (resultValue == 0)
                                {
                                    break;
                                }
                            }

                            index--;
                        }

                        // If we exhausted looking through all the values, and we still have
                        // a non-zero result, we couldn't match the result to only named values.
                        // In that case, we return null and let the call site just generate
                        // a string for the integral value if it desires.
                        return resultValue == 0;
                    }

                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static void WriteMultipleFoundFlagsNames(global::System.ReadOnlySpan<global::System.Int32> foundItems, global::System.Span<global::System.Char> destination)
                    {
                        for (global::System.Int32 i = foundItems.Length - 1; i != 0; i--)
                        {
                            global::System.String name = _names[foundItems[i]];
                            name.CopyTo(destination);
                            destination = destination[name.Length..];
                            global::System.Span<global::System.Char> afterSeparator = destination[2..]; // done before copying ", " to eliminate those two bounds checks
                            destination[0] = ',';
                            destination[1] = ' ';
                            destination = afterSeparator;
                        }

                        _names[foundItems[0]].CopyTo(destination);
                    }


                """);

        if (!spec.OriginalUnderlyingType.EndsWith("byte", StringComparison.OrdinalIgnoreCase))
        {
            sb.Append(
                """
                    [global::System.Security.SecuritySafeCriticalAttribute, global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static void ToCharsBuffer(global::System.Byte value, global::System.Span<global::System.Char> buffer, global::System.Int32 startingIndex)
                    {
                        global::System.UInt32 difference = ((value & 0xF0U) << 4) + (value & 0x0FU) - 0x8989U;
                        global::System.UInt32 packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

                        buffer[startingIndex + 1] = (global::System.Char)(packedResult & 0xFFU);
                        buffer[startingIndex] = (global::System.Char)(packedResult >> 8);
                    }


                """);
        }

        sb.Append(
            """
                [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
                private static global::System.FormatException CreateInvalidFormatSpecifierException() =>
                    new global::System.FormatException("Format string can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\".");

            """);
    }
}

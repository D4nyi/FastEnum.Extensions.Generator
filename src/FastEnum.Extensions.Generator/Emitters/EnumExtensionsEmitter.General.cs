using System.Globalization;
using System.Text;
using FastEnum.Extensions.Generator.Emitters;
using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddFileAndClassHeader(StringBuilder sb)
    {
        sb.AppendLine(Constants.FileHeader);

        if (!_currentSpec.IsGlobalNamespace)
        {
            sb
                .Append("namespace ").Append(_currentSpec.Namespace).AppendLine(";").AppendLine();
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                /// <summary>
                /// Extension methods for <see cref="{0}" />
                /// </summary>
                [global::System.CodeDom.Compiler.GeneratedCode("FastEnum.Helpers.Generator.EnumToStringGenerator", "{1}")]
                {2} static class {3}Extensions
                {{
                """, _currentSpec.FullName, Assembly.Version, _currentSpec.Modifier, _currentSpec.Name); ;
    }

    private void AddFieldsAndGetMethods(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                
                    private static readonly {0}[] _underlyingValues = 
                    {{
                
                """, _currentSpec.UnderlyingType);

        foreach (EnumMemberSpec member in _currentSpec.Members)
        {
            sb.Append(methodBodyIndent).Append(member.Value).AppendLine(",");
        }

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    }};
                
                    private static readonly {0}[] _values =
                    {{
                
                """, _currentSpec.FullName);

        foreach (EnumMemberSpec member in _currentSpec.Members)
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

        foreach (EnumMemberSpec member in _currentSpec.Members)
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
                    public static {1} [] GetValues() => _values;
                    
                    /// <summary>
                    /// Retrieves an array of the underlying vales of the members defined in <see cref="{1}" />
                    /// </summary>
                    /// <returns>An array of the underlying values defined in <see cref="{1}" />.</returns>
                    public static {2} [] GetUnderlyingValues() => _underlyingValues;
                    
                    /// <summary>
                    /// Retrieves an array of the names of the members defined in <see cref="{1}" />
                    /// </summary>
                    /// <returns>An array of the names of the members defined in <see cref="{1}" />.</returns>
                    public static global::System.String[] GetNames() => _names;
                
                
                """, _currentSpec.Members.Length, _currentSpec.FullName, _currentSpec.UnderlyingType);
    }

    private void AddHasFlag(StringBuilder sb)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
                    /// <param name="instance">The instance in which the the flags are searched.</param>
                    /// <param name="flags">The flags that will be looked up in the instance.</param>
                    /// <returns><see langword="true"/> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <see langword="false"/>.</returns>
                    public static global::System.Boolean HasFlag(this {0} instance, {1} flags) => (instance & flags) == flags;
                
                
                """, _currentSpec.FullName, _currentSpec.FullName);
    }

    private void AddIsDefined(StringBuilder sb)
    {
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Returns a <see langword="global::System.Boolean"/> telling whether the given enum value exists in the enumeration.</summary>
                    /// <param name="value">The value to check if it's defined</param>
                    /// <returns><see langword="true"/> if the value exists in the enumeration, <see langword="false"/> otherwise</returns>
                    public static global::System.Boolean IsDefined(this {0} value) => value switch
                    {{
                """, _currentSpec.FullName);

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

        sb.Append(
            """
            => true,
                    _ => false
                };
            
            
            """);
    }

    private void AddPrivateHelperMethods(StringBuilder sb)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    private static global::System.String? WriteMultipleFoundFlagsNames({0} value)
                    {{
                        {1} resultValue = global::System.Runtime.CompilerServices.Unsafe.As<{0}, {1}>(ref value);
                        global::System.Int32 index = 0;
                    
                        if (FindStart(resultValue) == -1)
                        {{
                            Span<global::System.Int32> foundItems = stackalloc global::System.Int32[{2}];
                            if (TryFindFlagsNames(resultValue, index, foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount))
                            {{
                                foundItems = foundItems[..foundItemsCount];
                    
                                global::System.Int32 length = checked(resultLength + 2 * (foundItemsCount - 1)); // ", ".Length == 2
                    
                                global::System.Span<global::System.Char> c = stackalloc global::System.Char[length];
                                global::System.String[] names = _names;
                    
                                for (int i = foundItems.Length - 1; i != 0; i--)
                                {{
                                    global::System.String name = names[foundItems[i]];
                                    name.CopyTo(c);
                                    c = c[name.Length..];
                                    global::System.Span<global::System.Char> afterSeparator = c[2..]; // done before copying ", " to eliminate those two bounds checks
                                    c[0] = ',';
                                    c[1] = ' ';
                                    c = afterSeparator;
                                }}
                    
                                names[foundItems[0]].CopyTo(c);
                    
                                return new global::System.String(c);
                            }}
                        }}
                    
                        return null;
                    }}
                    
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.Int32 FindStart({1} resultValue)
                    {{
                        {1}[] values = _underlyingValues;
                
                        int i;
                        for (i = values.Length - 1; (global::System.UInt32)i < (global::System.UInt32)values.Length; i--)
                        {{
                            if (values[i] <= resultValue)
                            {{
                                if (values[i] == resultValue)
                                {{
                                    return i;
                                }}
                
                                break;
                            }}
                        }}
                
                        return i;
                    }}
                    
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static global::System.Boolean TryFindFlagsNames({1} resultValue, global::System.Int32 index, Span<global::System.Int32> foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount)
                    {{
                        // Now look for multiple matches, storing the indices of the values into our span.
                        resultLength = 0;
                        foundItemsCount = 0;
                
                        {1}[] values = _underlyingValues;
                        global::System.String[] names = _names;
                
                        while (true)
                        {{
                            if ((global::System.UInt32)index >= (global::System.UInt32)values.Length)
                            {{
                                break;
                            }}
                
                            {1} currentValue = values[index];
                            if (index == 0 && currentValue == 0)
                            {{
                                break;
                            }}
                
                            if ((resultValue & currentValue) == currentValue)
                            {{
                                
                """, _currentSpec.FullName, _currentSpec.UnderlyingType, _currentSpec.Members.Length > 64 ? 64 : _currentSpec.Members.Length)
                .AddCorrectBitwiseOperation(_currentSpec.UnderlyingType)
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
                
                
                """)
            .Append(
                """
                    [global::System.Security.SecuritySafeCriticalAttribute, global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                    private static void ToCharsBuffer(global::System.Byte value, global::System.Span<global::System.Char> buffer, global::System.Int32 startingIndex)
                    {
                        global::System.UInt32 difference = ((value & 0xF0U) << 4) + (value & 0x0FU) - 0x8989U;
                        global::System.UInt32 packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;
                
                        buffer[startingIndex + 1] = (global::System.Char)(packedResult & 0xFFU);
                        buffer[startingIndex] = (global::System.Char)(packedResult >> 8);
                    }
                
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
                    private static global::System.FormatException CreateInvalidFormatSpecifierException() =>
                        new global::System.FormatException("Format string can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\".");
                
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
                #if NET7_0_OR_GREATER
                    private static global::System.Diagnostics.UnreachableException CreateUnreachableException(global::System.Int32 value) =>
                        new global::System.Diagnostics.UnreachableException($"Tried to access a non-existent value: {value}");
                #else
                    private static global::System.IndexOutOfRangeException CreateUnreachableException(global::System.Int32 value) =>
                        new global::System.IndexOutOfRangeException($"Tried to access a non-existent value: {value}");
                #endif
                
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
                    private static global::System.ArgumentException CreateInvalidEmptyParseArgument() =>
                        new global::System.ArgumentException("Must specify valid information for parsing in the string.", "value");
                
                    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
                    private static global::System.ArgumentException CreateValueNotFoundArgument(global::System.ReadOnlySpan<global::System.Char> originalValue) =>
                        new global::System.ArgumentException(global::System.String.Format(global::System.Globalization.CultureInfo.InvariantCulture, "Requested value '{0}' was not found.", originalValue.ToString()));

                """);
    }

    private static void CloseClassAndNamespace(StringBuilder sb)
    {
        sb.AppendLine("}");
    }
}

﻿//HintName: NoneUniqueOptionsExtensions.g.cs
// <auto-generated/>

#nullable enable annotations
#nullable disable warnings

namespace SnapshotTesting;

/// <summary>
/// Extension methods for <see cref="SnapshotTesting.NoneUniqueOptions" />
/// </summary>
[global::System.CodeDom.Compiler.GeneratedCode("FastEnum.Extensions.Generator.EnumExtensionsGenerator", "1.4.0")]
public static class NoneUniqueOptionsExtensions
{
    private static readonly global::System.UInt64[] _underlyingValues =
    {
        0,
        1,
        1,
        2,
    };

    private static readonly SnapshotTesting.NoneUniqueOptions[] _values =
    {
        SnapshotTesting.NoneUniqueOptions.None,
        SnapshotTesting.NoneUniqueOptions.ToString,
        SnapshotTesting.NoneUniqueOptions.ToStringFormat,
        SnapshotTesting.NoneUniqueOptions.Parse,
    };

    private static readonly global::System.String[] _names =
    {
        nameof(SnapshotTesting.NoneUniqueOptions.None),
        nameof(SnapshotTesting.NoneUniqueOptions.ToString),
        nameof(SnapshotTesting.NoneUniqueOptions.ToStringFormat),
        nameof(SnapshotTesting.NoneUniqueOptions.Parse),
    };

    /// <summary>
    /// The number of members in the enum.
    /// </summary>
    public const global::System.Int32 MembersCount = 4;

    /// <summary>
    /// Retrieves an array of the values of the members defined in <see cref="SnapshotTesting.NoneUniqueOptions" />.
    /// </summary>
    /// <returns>An array of the values defined in <see cref="SnapshotTesting.NoneUniqueOptions" />.</returns>
    public static SnapshotTesting.NoneUniqueOptions[] GetValues() => _values;

    /// <summary>
    /// Retrieves an array of the underlying vales of the members defined in <see cref="SnapshotTesting.NoneUniqueOptions" />
    /// </summary>
    /// <returns>An array of the underlying values defined in <see cref="SnapshotTesting.NoneUniqueOptions" />.</returns>
    public static global::System.UInt64[] GetUnderlyingValues() => _underlyingValues;

    /// <summary>
    /// Retrieves an array of the names of the members defined in <see cref="SnapshotTesting.NoneUniqueOptions" />
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="SnapshotTesting.NoneUniqueOptions" />.</returns>
    public static global::System.String[] GetNames() => _names;

    /// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
    /// <param name="instance">The instance in which the flags are searched.</param>
    /// <param name="flags">The flags that will be looked up in the instance.</param>
    /// <returns><see langword="true"/> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <see langword="false"/>.</returns>
    public static global::System.Boolean HasFlag(this SnapshotTesting.NoneUniqueOptions instance, SnapshotTesting.NoneUniqueOptions flags) => (instance & flags) == flags;

    /// <summary>Returns a <see langword="global::System.Boolean"/> telling whether the given enum value exists in the enumeration.</summary>
    /// <param name="value">The value to check if it's defined</param>
    /// <returns><see langword="true"/> if the value exists in the enumeration, <see langword="false"/> otherwise</returns>
    public static global::System.Boolean IsDefined(this SnapshotTesting.NoneUniqueOptions value) => value switch
    {
        SnapshotTesting.NoneUniqueOptions.None or SnapshotTesting.NoneUniqueOptions.ToString or SnapshotTesting.NoneUniqueOptions.ToStringFormat or 
        SnapshotTesting.NoneUniqueOptions.Parse => true,
        _ => false
    };

    /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
    /// <param name="value">The <see cref="SnapshotTesting.NoneUniqueOptions"/> value to convert to a string.</param>
    /// <returns>The string representation of the value of this instance.</returns>
    public static global::System.String FastToString(this SnapshotTesting.NoneUniqueOptions value) => value switch
    {
        SnapshotTesting.NoneUniqueOptions.None => nameof(SnapshotTesting.NoneUniqueOptions.None),
        SnapshotTesting.NoneUniqueOptions.ToStringFormat => nameof(SnapshotTesting.NoneUniqueOptions.ToStringFormat),
        SnapshotTesting.NoneUniqueOptions.Parse => nameof(SnapshotTesting.NoneUniqueOptions.Parse),
        _ => (global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOptions, global::System.UInt64>(ref value)).ToString()
    };

    /// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
    /// <param name="value">The <see cref="SnapshotTesting.NoneUniqueOptions"/> value to convert to a string.</param>
    /// <param name="format">A format string.</param>
    /// <returns>The string representation of the value of this instance as specified by format.</returns>
    /// <exception cref="global::System.FormatException"><paramref name="format"/> contains an invalid specification.</exception>
    public static global::System.String FastToString(this SnapshotTesting.NoneUniqueOptions value,
        [global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.EnumFormat)]
        global::System.String? format)
    {
        if (global::System.String.IsNullOrEmpty(format)) return value.FastToString();

        if (format.Length != 1) throw CreateInvalidFormatSpecifierException();

        global::System.Char formatChar = format[0];
        switch (formatChar | 0x20)
        {
            case 'g': return value.FastToString();
            case 'd': return global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOptions, global::System.UInt64>(ref value).ToString();
            case 'x': return FormatNumberAsHex(value);
            case 'f':
                global::System.String? result = FormatFlagNames(value);
                if (result is null) goto case 'd';
                return result;
            default: throw CreateInvalidFormatSpecifierException();
        }
    }

    /// <summary>Gets the Value property from applied <see cref="global::System.Runtime.Serialization.EnumMemberAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.Runtime.Serialization.EnumMemberAttribute.Value"/> if exists; otherwise null.</returns>
    public static string? GetEnumMemberValue(this SnapshotTesting.NoneUniqueOptions value) => null;

    /// <summary>Gets the Name property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Name"/> if exists; otherwise null.</returns>
    public static string? GetDisplayName(this SnapshotTesting.NoneUniqueOptions value) => null;

    /// <summary>Gets the Description property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Description"/> if exists; otherwise null.</returns>
    public static string? GetDisplayDescription(this SnapshotTesting.NoneUniqueOptions value) => null;

    /// <summary>Gets the value of the description from applied <see cref="global::System.ComponentModel.DescriptionAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The description read from the applied <see cref="global::System.ComponentModel.DescriptionAttribute"/> if exists; otherwise null.</returns>
    public static string? GetDescription(this SnapshotTesting.NoneUniqueOptions value) => null;

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOptions" />.
    /// This method using case-sensitive parsing.
    /// </summary>
    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out SnapshotTesting.NoneUniqueOptions result)
    {
        if (global::System.String.IsNullOrEmpty(value))
        {
            result = default;
            return false;
        }

        global::System.ReadOnlySpan<global::System.Char> span = value.AsSpan().TrimStart();
        if (span.IsEmpty)
        {
            result = default;
            return false;
        }

        if (CheckIfNumber(span))
        {
            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
            return TryParseAsNumber(span, out result);
        }

        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.None)))
        {
            result = SnapshotTesting.NoneUniqueOptions.None;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.ToString)))
        {
            result = SnapshotTesting.NoneUniqueOptions.ToString;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.ToStringFormat)))
        {
            result = SnapshotTesting.NoneUniqueOptions.ToStringFormat;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.Parse)))
        {
            result = SnapshotTesting.NoneUniqueOptions.Parse;
            return true;
        }

        return TryParseByName(value, false, out result);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOptions" />.
    /// This method using case-insensitive parsing.
    /// </summary>
    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParseIgnoreCase([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out SnapshotTesting.NoneUniqueOptions result)
    {
        if (global::System.String.IsNullOrEmpty(value))
        {
            result = default;
            return false;
        }

        global::System.ReadOnlySpan<global::System.Char> span = value.AsSpan().TrimStart();
        if (span.IsEmpty)
        {
            result = default;
            return false;
        }

        if (CheckIfNumber(span))
        {
            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
            return TryParseAsNumber(span, out result);
        }

        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.None), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.NoneUniqueOptions.None;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.ToString), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.NoneUniqueOptions.ToString;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.ToStringFormat), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.NoneUniqueOptions.ToStringFormat;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.Parse), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.NoneUniqueOptions.Parse;
            return true;
        }

        return TryParseByName(value, true, out result);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOptions" />.
    /// This method using case-sensitive parsing.
    /// </summary>
    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.NoneUniqueOptions result) =>
        TryParseSpan(value, global::System.StringComparison.Ordinal, out result);

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOptions" />.
    /// This method using case-insensitive parsing.
    /// A parameter specifies whether the operation is case-insensitive.
    /// </summary>
    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParseIgnoreCase(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.NoneUniqueOptions result) =>
        TryParseSpan(value, global::System.StringComparison.OrdinalIgnoreCase, out result);

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.String FormatNumberAsHex(SnapshotTesting.NoneUniqueOptions data) => data switch
    {
        SnapshotTesting.NoneUniqueOptions.None => "0000000000000000",
        SnapshotTesting.NoneUniqueOptions.ToStringFormat => "0000000000000001",
        SnapshotTesting.NoneUniqueOptions.Parse => "0000000000000002",
        _ => global::System.String.Create(sizeof(global::System.UInt64) * 2, global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOptions, global::System.UInt64>(ref data), (buffer, value) =>
        {
             ToCharsBuffer((global::System.Byte)(value >> 56), buffer, 0);
             ToCharsBuffer((global::System.Byte)(value >> 48), buffer, 2);
             ToCharsBuffer((global::System.Byte)(value >> 40), buffer, 4);
             ToCharsBuffer((global::System.Byte)(value >> 32), buffer, 6);
             ToCharsBuffer((global::System.Byte)(value >> 24), buffer, 8);
             ToCharsBuffer((global::System.Byte)(value >> 16), buffer, 10);
             ToCharsBuffer((global::System.Byte)(value >> 8), buffer, 12);
             ToCharsBuffer((global::System.Byte)value, buffer, 14);
        })
    };

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.Boolean TryParseSpan(global::System.ReadOnlySpan<global::System.Char> value, global::System.StringComparison comparison, out SnapshotTesting.NoneUniqueOptions result)
    {
        if (value.IsEmpty)
        {
            result = default;
            return false;
        }

        if (CheckIfNumber(value))
        {
            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
            return TryParseAsNumber(value, out result);
        }

        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.None).AsSpan(), comparison))
        {
            result = SnapshotTesting.NoneUniqueOptions.None;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.ToString).AsSpan(), comparison))
        {
            result = SnapshotTesting.NoneUniqueOptions.ToString;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.ToStringFormat).AsSpan(), comparison))
        {
            result = SnapshotTesting.NoneUniqueOptions.ToStringFormat;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.NoneUniqueOptions.Parse).AsSpan(), comparison))
        {
            result = SnapshotTesting.NoneUniqueOptions.Parse;
            return true;
        }

        global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
        return TryParseByName(value, comparison == global::System.StringComparison.OrdinalIgnoreCase, out result);
    }

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.Boolean CheckIfNumber(global::System.ReadOnlySpan<global::System.Char> value)
    {
        global::System.Char c = value[0];

        return global::System.Char.IsAsciiDigit(c) || c == '-' || c == '+';
    }

    private static global::System.Boolean TryParseAsNumber(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.NoneUniqueOptions result)
    {
        const global::System.Globalization.NumberStyles NumberStyle = global::System.Globalization.NumberStyles.AllowLeadingSign | global::System.Globalization.NumberStyles.AllowTrailingWhite;
        global::System.Globalization.NumberFormatInfo numberFormat = global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
        global::System.Boolean status = global::System.UInt64.TryParse(value, NumberStyle, numberFormat, out var parseResult);

        if (status)
        {
            result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.UInt64, SnapshotTesting.NoneUniqueOptions>(ref parseResult);
            return true;
        }

        result = default;
        return false;
    }

    private static global::System.Boolean TryParseByName(global::System.ReadOnlySpan<global::System.Char> value, global::System.Boolean ignoreCase, out SnapshotTesting.NoneUniqueOptions result)
    {
        global::System.String[] enumNames = _names;
        global::System.UInt64[] enumValues = _underlyingValues;
        global::System.Boolean parsed = true;
        global::System.UInt64 localResult = 0;

        while (value.Length > 0)
        {
            // Find the next separator
            global::System.ReadOnlySpan<global::System.Char> subValue;
            global::System.Int32 endIndex = value.IndexOf(',');
            if (endIndex < 0)
            {
                // No next separator; use the remainder as the next value
                subValue = value.Trim();
                value = default;
            }
            else if (endIndex != value.Length - 1)
            {
                // Found a separator before the last char
                subValue = value[..endIndex].Trim();
                value = value[(endIndex + 1)..];
            }
            else
            {
                // Last char was a separator, which is invalid
                parsed = false;
                break;
            }

            // Try to match this substring against each enum name
            global::System.Boolean success = false;
            if (ignoreCase)
            {
                for (global::System.Int32 i = 0; i < enumNames.Length; i++)
                {
                    if (subValue.Equals(enumNames[i], global::System.StringComparison.OrdinalIgnoreCase))
                    {
                        localResult |= enumValues[i];
                        success = true;
                        break;
                    }
                }
            }
            else
            {
                for (global::System.Int32 i = 0; i < enumNames.Length; i++)
                {
                    if (subValue.SequenceEqual(enumNames[i]))
                    {
                        localResult |= enumValues[i];
                        success = true;
                        break;
                    }
                }
            }

            if (!success)
            {
                parsed = false;
                break;
            }
        }

        if (parsed)
        {
            result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.UInt64, SnapshotTesting.NoneUniqueOptions>(ref localResult);
            return true;
        }

        result = default;
        return false;
    }

    private static global::System.String? FormatFlagNames(SnapshotTesting.NoneUniqueOptions value) => value switch
    {
        SnapshotTesting.NoneUniqueOptions.None => nameof(SnapshotTesting.NoneUniqueOptions.None),
        SnapshotTesting.NoneUniqueOptions.ToStringFormat => nameof(SnapshotTesting.NoneUniqueOptions.ToStringFormat),
        SnapshotTesting.NoneUniqueOptions.Parse => nameof(SnapshotTesting.NoneUniqueOptions.Parse),
        _ => ProcessMultipleFlagsNames(value)
    };

    private static global::System.String? ProcessMultipleFlagsNames(SnapshotTesting.NoneUniqueOptions value)
    {
        global::System.UInt64 resultValue = global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOptions, global::System.UInt64>(ref value);

        Span<global::System.Int32> foundItems = stackalloc global::System.Int32[4];
        if (!TryFindFlagsNames(resultValue, foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount))
        {
            return null;
        }

        foundItems = foundItems[..foundItemsCount];

        global::System.Int32 length = checked(resultLength + 2 * (foundItemsCount - 1)); // ", ".Length == 2

        global::System.Span<global::System.Char> destination = stackalloc global::System.Char[length];

        WriteMultipleFoundFlagsNames(_names, foundItems, destination);

        return new global::System.String(destination);
    }

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.Boolean TryFindFlagsNames(global::System.UInt64 resultValue, global::System.Span<global::System.Int32> foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount)
    {
        // Now look for multiple matches, storing the indices of the values into our span.
        resultLength = 0;
        foundItemsCount = 0;

        global::System.Int32 index = MembersCount - 1;
        global::System.UInt64[] values = _underlyingValues;
        global::System.String[] names = _names;

        while (true)
        {
            global::System.UInt64 currentValue = values[index];
            if (index == 0 && currentValue == 0)
            {
                break;
            }

            if ((resultValue & currentValue) == currentValue)
            {
                resultValue = (global::System.UInt64)(resultValue & ~currentValue);
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
    private static void WriteMultipleFoundFlagsNames(global::System.String[] names, global::System.ReadOnlySpan<global::System.Int32> foundItems, global::System.Span<global::System.Char> destination)
    {
        for (global::System.Int32 i = foundItems.Length - 1; i != 0; i--)
        {
            global::System.String name = names[foundItems[i]];
            name.CopyTo(destination);
            destination = destination[name.Length..];
            global::System.Span<global::System.Char> afterSeparator = destination[2..]; // done before copying ", " to eliminate those two bounds checks
            destination[0] = ',';
            destination[1] = ' ';
            destination = afterSeparator;
        }

        names[foundItems[0]].CopyTo(destination);
    }

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
}

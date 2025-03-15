﻿//HintName: NoneUniqueOptionExtensions.g.cs
// <auto-generated/>

#nullable enable

namespace SnapshotTesting;

/// <summary>Extension methods for <see cref="SnapshotTesting.NoneUniqueOption" /></summary>
public static class NoneUniqueOptionExtensions
{
    private static readonly global::System.Int32[] _underlyingValues =
    {
        0,
        1,
        1,
        2,
        8,
    };

    private static readonly SnapshotTesting.NoneUniqueOption[] _values =
    {
        SnapshotTesting.NoneUniqueOption.None,
        SnapshotTesting.NoneUniqueOption.ToString,
        SnapshotTesting.NoneUniqueOption.ToStringFormat,
        SnapshotTesting.NoneUniqueOption.Parse,
        SnapshotTesting.NoneUniqueOption.IsDefined,
    };

    private static readonly global::System.String[] _names =
    {
        nameof(SnapshotTesting.NoneUniqueOption.None),
        nameof(SnapshotTesting.NoneUniqueOption.ToString),
        nameof(SnapshotTesting.NoneUniqueOption.ToStringFormat),
        nameof(SnapshotTesting.NoneUniqueOption.Parse),
        nameof(SnapshotTesting.NoneUniqueOption.IsDefined),
    };

    /// <summary>The number of members in the enum.</summary>
    public const global::System.Int32 MembersCount = 5;

    /// <summary>Retrieves an array of the values of the members defined in <see cref="SnapshotTesting.NoneUniqueOption" />.</summary>
    /// <returns>An array of the values defined in <see cref="SnapshotTesting.NoneUniqueOption" />.</returns>
    public static SnapshotTesting.NoneUniqueOption[] GetValues() => _values;

    /// <summary>Retrieves an array of the underlying vales of the members defined in <see cref="SnapshotTesting.NoneUniqueOption" /></summary>
    /// <returns>An array of the underlying values defined in <see cref="SnapshotTesting.NoneUniqueOption" />.</returns>
    public static global::System.Int32[] GetUnderlyingValues() => _underlyingValues;

    /// <summary>Retrieves an array of the names of the members defined in <see cref="SnapshotTesting.NoneUniqueOption" /></summary>
    /// <returns>An array of the names of the members defined in <see cref="SnapshotTesting.NoneUniqueOption" />.</returns>
    public static global::System.String[] GetNames() => _names;

    /// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
    /// <param name="instance">The instance in which the flags are searched.</param>
    /// <param name="flags">The flags that will be looked up in the instance.</param>
    /// <returns><see langword="true"/> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <see langword="false"/>.</returns>
    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static global::System.Boolean HasFlag(this SnapshotTesting.NoneUniqueOption instance, SnapshotTesting.NoneUniqueOption flags) => (instance & flags) == flags;

    /// <summary>Returns a <see langword="global::System.Boolean"/> telling whether the given enum value exists in the enumeration.</summary>
    /// <param name="value">The value to check if it's defined</param>
    /// <returns><see langword="true"/> if the value exists in the enumeration, <see langword="false"/> otherwise</returns>
    public static global::System.Boolean IsDefined(this SnapshotTesting.NoneUniqueOption value) => value switch
    {
        SnapshotTesting.NoneUniqueOption.None or SnapshotTesting.NoneUniqueOption.ToString or SnapshotTesting.NoneUniqueOption.ToStringFormat or 
        SnapshotTesting.NoneUniqueOption.Parse or SnapshotTesting.NoneUniqueOption.IsDefined => true,
        _ => false
    };

    /// <summary>Gets the Value property from applied <see cref="global::System.Runtime.Serialization.EnumMemberAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOption"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.Runtime.Serialization.EnumMemberAttribute.Value"/> if exists; otherwise null.</returns>
    public static global::System.String? GetEnumMemberValue(this SnapshotTesting.NoneUniqueOption value) => null;

    /// <summary>Gets the Name property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOption"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Name"/> if exists; otherwise null.</returns>
    public static global::System.String? GetDisplayName(this SnapshotTesting.NoneUniqueOption value) => value switch
    {
        SnapshotTesting.NoneUniqueOption.Parse => "Parse",
        _ => null
    };

    /// <summary>Gets the Description property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOption"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Description"/> if exists; otherwise null.</returns>
    public static global::System.String? GetDisplayDescription(this SnapshotTesting.NoneUniqueOption value) => null;

    /// <summary>Gets the value of the description from applied <see cref="global::System.ComponentModel.DescriptionAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.NoneUniqueOption"/> enum value from which the attribute value is read.</param>
    /// <returns>The description read from the applied <see cref="global::System.ComponentModel.DescriptionAttribute"/> if exists; otherwise null.</returns>
    public static global::System.String? GetDescription(this SnapshotTesting.NoneUniqueOption value) => null;

    /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
    /// <param name="value">The <see cref="SnapshotTesting.NoneUniqueOption"/> value to convert to a string.</param>
    /// <returns>The string representation of the value of this instance.</returns>
    public static global::System.String FastToString(this SnapshotTesting.NoneUniqueOption value) => value switch
    {
        SnapshotTesting.NoneUniqueOption.None => nameof(SnapshotTesting.NoneUniqueOption.None),
        SnapshotTesting.NoneUniqueOption.ToString => nameof(SnapshotTesting.NoneUniqueOption.ToString),
        SnapshotTesting.NoneUniqueOption.Parse => nameof(SnapshotTesting.NoneUniqueOption.Parse),
        SnapshotTesting.NoneUniqueOption.IsDefined => nameof(SnapshotTesting.NoneUniqueOption.IsDefined),
        _ => (global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOption, global::System.Int32>(ref value)).ToString()
    };

    /// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
    /// <param name="value">The <see cref="SnapshotTesting.NoneUniqueOption"/> value to convert to a string.</param>
    /// <param name="format">A format string.</param>
    /// <returns>The string representation of the value of this instance as specified by format.</returns>
    /// <exception cref="global::System.FormatException"><paramref name="format"/> contains an invalid specification.</exception>
    public static global::System.String FastToString(this SnapshotTesting.NoneUniqueOption value, [global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute("EnumFormat")] global::System.String? format)
    {
        if (global::System.String.IsNullOrEmpty(format)) return value.FastToString();

        if (format.Length != 1) throw CreateInvalidFormatSpecifierException();

        global::System.Char formatChar = format[0];
        switch (formatChar | 0x20)
        {
            case 'g': return value.FastToString();
            case 'd': return global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOption, global::System.Int32>(ref value).ToString();
            case 'x': return FormatNumberAsHex(value);
            case 'f':
                global::System.String? result = FormatFlagNames(value);
                if (result is null) goto case 'd';
                return result;
            default: throw CreateInvalidFormatSpecifierException();
        }
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOption" />.
    /// This method using case-sensitive parsing.
    /// </summary>
    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out SnapshotTesting.NoneUniqueOption result)
    {
        if (global::System.String.IsNullOrEmpty(value))
        {
            result = default;
            return false;
        }

        global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
        return TryParseSpan(value.AsSpan(), false, out result);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOption" />.
    /// This method using case-insensitive parsing.
    /// </summary>
    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParseIgnoreCase([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out SnapshotTesting.NoneUniqueOption result)
    {
        if (global::System.String.IsNullOrEmpty(value))
        {
            result = default;
            return false;
        }

        global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
        return TryParseSpan(value.AsSpan(), true, out result);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOption" />.
    /// This method using case-sensitive parsing.
    /// </summary>
    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.NoneUniqueOption result)
    {
        if (value.IsEmpty)
        {
           result = default;
           return false;
       }

       global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
       return TryParseSpan(value, false, out result);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.NoneUniqueOption" />.
    /// This method using case-insensitive parsing.
    /// A parameter specifies whether the operation is case-insensitive.
    /// </summary>
    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParseIgnoreCase(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.NoneUniqueOption result)
    {
        if (value.IsEmpty)
        {
            result = default;
            return false;
        }

        global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
        return TryParseSpan(value, true, out result);
    }

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.String FormatNumberAsHex(SnapshotTesting.NoneUniqueOption data) => data switch
    {
        SnapshotTesting.NoneUniqueOption.None => "00000000",
        SnapshotTesting.NoneUniqueOption.ToString => "00000001",
        SnapshotTesting.NoneUniqueOption.Parse => "00000002",
        SnapshotTesting.NoneUniqueOption.IsDefined => "00000008",
        _ => global::System.String.Create(sizeof(global::System.Int32) * 2, global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOption, global::System.Int32>(ref data), static (buffer, value) =>
        {
             global::System.Byte byteValue = (global::System.Byte)(value >> 24);
             global::System.UInt32 difference = ((byteValue & 0xF0U) << 4) + (byteValue & 0x0FU) - 0x8989U;
             global::System.UInt32 packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

             buffer[1] = (global::System.Char)(packedResult & 0xFFU);
             buffer[0] = (global::System.Char)(packedResult >> 8);

             byteValue = (global::System.Byte)(value >> 16);
             difference = ((byteValue & 0xF0U) << 4) + (byteValue & 0x0FU) - 0x8989U;
             packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

             buffer[3] = (global::System.Char)(packedResult & 0xFFU);
             buffer[2] = (global::System.Char)(packedResult >> 8);

             byteValue = (global::System.Byte)(value >> 8);
             difference = ((byteValue & 0xF0U) << 4) + (byteValue & 0x0FU) - 0x8989U;
             packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

             buffer[5] = (global::System.Char)(packedResult & 0xFFU);
             buffer[4] = (global::System.Char)(packedResult >> 8);

             byteValue = (global::System.Byte)value;
             difference = ((byteValue & 0xF0U) << 4) + (byteValue & 0x0FU) - 0x8989U;
             packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

             buffer[7] = (global::System.Char)(packedResult & 0xFFU);
             buffer[6] = (global::System.Char)(packedResult >> 8);
        })
    };

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.String? FormatFlagNames(SnapshotTesting.NoneUniqueOption value) => value switch
    {
        SnapshotTesting.NoneUniqueOption.None => nameof(SnapshotTesting.NoneUniqueOption.None),
        SnapshotTesting.NoneUniqueOption.ToStringFormat => nameof(SnapshotTesting.NoneUniqueOption.ToStringFormat),
        SnapshotTesting.NoneUniqueOption.Parse => nameof(SnapshotTesting.NoneUniqueOption.Parse),
        SnapshotTesting.NoneUniqueOption.IsDefined => nameof(SnapshotTesting.NoneUniqueOption.IsDefined),
        _ => ProcessMultipleFlagsNames(value)
    };

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.Boolean TryParseSpan(global::System.ReadOnlySpan<global::System.Char> value, global::System.Boolean ignoreCase, out SnapshotTesting.NoneUniqueOption result)
    {
        global::System.Char c = value[0];
        if (global::System.Char.IsWhiteSpace(c))
        {
            value = value.TrimStart();
            if (value.IsEmpty)
            {
                goto Done;
            }

            c = value[0];
        }

        if (!global::System.Char.IsAsciiDigit(c) && c != '-' && c != '+')
        {
            global::System.Runtime.CompilerServices.Unsafe.SkipInit(out result);
            return TryParseByName(value, ignoreCase, out result);
        }

        const global::System.Globalization.NumberStyles NumberStyle = global::System.Globalization.NumberStyles.AllowLeadingSign | global::System.Globalization.NumberStyles.AllowTrailingWhite;
        global::System.Globalization.NumberFormatInfo numberFormat = global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
        global::System.Boolean status = global::System.Int32.TryParse(value, NumberStyle, numberFormat, out global::System.Int32 parseResult);

        if (status)
        {
            result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.Int32, SnapshotTesting.NoneUniqueOption>(ref parseResult);
            return true;
        }

    Done:
        result = default;
        return false;
    }

    private static global::System.Boolean TryParseByName(global::System.ReadOnlySpan<global::System.Char> value, global::System.Boolean ignoreCase, out SnapshotTesting.NoneUniqueOption result)
    {
        global::System.ReadOnlySpan<global::System.String> enumNames = _names;
        global::System.ReadOnlySpan<global::System.Int32> enumValues = _underlyingValues;
        global::System.Boolean parsed = true;
        global::System.Int32 localResult = 0;

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
            result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.Int32, SnapshotTesting.NoneUniqueOption>(ref localResult);
            return true;
        }

        result = default;
        return false;
    }

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
    private static global::System.String? ProcessMultipleFlagsNames(SnapshotTesting.NoneUniqueOption value)
    {
        global::System.Int32 resultValue = global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.NoneUniqueOption, global::System.Int32>(ref value);

        global::System.Span<global::System.Int32> foundItems = stackalloc global::System.Int32[5];

        // Now look for multiple matches, storing the indices of the values into our span.
        global::System.Int32 resultLength = 0;
        global::System.Int32 foundItemsCount = 0;

        global::System.Int32 index = MembersCount - 1;
        global::System.Int32[] values = _underlyingValues;
        global::System.String[] names = _names;

        while (true)
        {
            if ((global::System.UInt32)index >= (global::System.UInt32)values.Length)
            {
                break;
            }

            global::System.Int32 currentValue = values[index];
            if (index == 0 && currentValue == 0)
            {
                break;
            }

            if ((resultValue & currentValue) == currentValue)
            {
                resultValue = (global::System.Int32)(resultValue & ~currentValue);
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
        if (resultValue != 0)
        {
            return null;
        }

        foundItems = foundItems[..foundItemsCount];

        foundItemsCount--;

        global::System.Int32 length = checked(resultLength + 2 * foundItemsCount); // ", ".Length == 2

        global::System.Span<global::System.Char> destination = stackalloc global::System.Char[length];

        global::System.Span<global::System.Char> workingSpan = destination;

        for (global::System.Int32 i = foundItemsCount; i != 0; i--)
        {
            global::System.String name = names[foundItems[i]];
            name.CopyTo(workingSpan);
            workingSpan = workingSpan[name.Length..];
            global::System.Span<global::System.Char> afterSeparator = workingSpan[2..]; // done before copying ", " to eliminate those two bounds checks
            workingSpan[0] = ',';
            workingSpan[1] = ' ';
            workingSpan = afterSeparator;
        }

        names[foundItems[0]].CopyTo(workingSpan);

        return new global::System.String(destination);
    }

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
    private static global::System.FormatException CreateInvalidFormatSpecifierException() =>
        new global::System.FormatException("Format string can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\".");
}
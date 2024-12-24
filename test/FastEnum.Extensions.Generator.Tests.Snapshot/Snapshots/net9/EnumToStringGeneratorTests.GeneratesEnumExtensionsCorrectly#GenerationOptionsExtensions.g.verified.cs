﻿//HintName: GenerationOptionsExtensions.g.cs
// <auto-generated/>

#nullable enable annotations
#nullable disable warnings

namespace SnapshotTesting;

/// <summary>
/// Extension methods for <see cref="SnapshotTesting.GenerationOptions" />
/// </summary>
[global::System.CodeDom.Compiler.GeneratedCode("FastEnum.Helpers.Generator.EnumToStringGenerator", "1.3.0")]
public static class GenerationOptionsExtensions
{
    private static readonly global::System.Byte[] _underlyingValues =
    {
        0,
        1,
        2,
        4,
    };

    private static readonly SnapshotTesting.GenerationOptions[] _values =
    {
        SnapshotTesting.GenerationOptions.None,
        SnapshotTesting.GenerationOptions.ToString,
        SnapshotTesting.GenerationOptions.Parse,
        SnapshotTesting.GenerationOptions.HasFlag,
    };

    private static readonly global::System.String[] _names =
    {
        nameof(SnapshotTesting.GenerationOptions.None),
        nameof(SnapshotTesting.GenerationOptions.ToString),
        nameof(SnapshotTesting.GenerationOptions.Parse),
        nameof(SnapshotTesting.GenerationOptions.HasFlag),
    };

    /// <summary>
    /// The number of members in the enum.
    /// </summary>
    public const global::System.Int32 MembersCount = 4;

    /// <summary>
    /// Retrieves an array of the values of the members defined in <see cref="SnapshotTesting.GenerationOptions" />.
    /// </summary>
    /// <returns>An array of the values defined in <see cref="SnapshotTesting.GenerationOptions" />.</returns>
    public static SnapshotTesting.GenerationOptions [] GetValues() => _values;

    /// <summary>
    /// Retrieves an array of the underlying vales of the members defined in <see cref="SnapshotTesting.GenerationOptions" />
    /// </summary>
    /// <returns>An array of the underlying values defined in <see cref="SnapshotTesting.GenerationOptions" />.</returns>
    public static global::System.Byte [] GetUnderlyingValues() => _underlyingValues;

    /// <summary>
    /// Retrieves an array of the names of the members defined in <see cref="SnapshotTesting.GenerationOptions" />
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="SnapshotTesting.GenerationOptions" />.</returns>
    public static global::System.String[] GetNames() => _names;

    /// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
    /// <param name="instance">The instance in which the flags are searched.</param>
    /// <param name="flags">The flags that will be looked up in the instance.</param>
    /// <returns><see langword="true"/> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <see langword="false"/>.</returns>
    public static global::System.Boolean HasFlag(this SnapshotTesting.GenerationOptions instance, SnapshotTesting.GenerationOptions flags) => (instance & flags) == flags;

    /// <summary>Returns a <see langword="global::System.Boolean"/> telling whether the given enum value exists in the enumeration.</summary>
    /// <param name="value">The value to check if it's defined</param>
    /// <returns><see langword="true"/> if the value exists in the enumeration, <see langword="false"/> otherwise</returns>
    public static global::System.Boolean IsDefined(this SnapshotTesting.GenerationOptions value) => value switch
    {
        SnapshotTesting.GenerationOptions.None or SnapshotTesting.GenerationOptions.ToString or SnapshotTesting.GenerationOptions.Parse or 
        SnapshotTesting.GenerationOptions.HasFlag => true,
        _ => false
    };

    /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
    /// <param name="value">The <see cref="SnapshotTesting.GenerationOptions"/> value to convert to a string.</param>
    /// <returns>The string representation of the value of this instance.</returns>
    public static global::System.String FastToString(this SnapshotTesting.GenerationOptions value) => value switch
    {
        SnapshotTesting.GenerationOptions.None => nameof(SnapshotTesting.GenerationOptions.None),
        SnapshotTesting.GenerationOptions.ToString => nameof(SnapshotTesting.GenerationOptions.ToString),
        SnapshotTesting.GenerationOptions.Parse => nameof(SnapshotTesting.GenerationOptions.Parse),
        SnapshotTesting.GenerationOptions.HasFlag => nameof(SnapshotTesting.GenerationOptions.HasFlag),
        _ => (global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.GenerationOptions, global::System.Byte>(ref value)).ToString()
    };

    /// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
    /// <param name="value">The <see cref="SnapshotTesting.GenerationOptions"/> value to convert to a string.</param>
    /// <param name="format">A format string.</param>
    /// <returns>The string representation of the value of this instance as specified by format.</returns>
    /// <exception cref="global::System.FormatException"><paramref name="format"/> contains an invalid specification.</exception>
    public static global::System.String FastToString(this SnapshotTesting.GenerationOptions value,
        [global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute(global::System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.EnumFormat)]
        global::System.String? format)
    {
        if (global::System.String.IsNullOrEmpty(format)) return value.FastToString();

        if (format.Length != 1) throw CreateInvalidFormatSpecifierException();

        global::System.Char formatChar = format[0];
        switch (formatChar | 0x20)
        {
            case 'g': return value.FastToString();
            case 'd': return global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.GenerationOptions, global::System.Byte>(ref value).ToString();
            case 'x': return FormatNumberAsHex(value);
            case 'f':
                global::System.String? result = FormatFlagNames(value);
                if (result is null) goto case 'd';
                return result;
            default: throw CreateInvalidFormatSpecifierException();
        }
    }

    /// <summary>Gets the Value property from applied <see cref="global::System.Runtime.Serialization.EnumMemberAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.GenerationOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.Runtime.Serialization.EnumMemberAttribute.Value"/> if exists; otherwise null.</returns>
    public static string? GetEnumMemberValue(this SnapshotTesting.GenerationOptions value) => null;

    /// <summary>Gets the Name property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.GenerationOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Name"/> if exists; otherwise null.</returns>
    public static string? GetDisplayName(this SnapshotTesting.GenerationOptions value) => null;

    /// <summary>Gets the Description property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.GenerationOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Description"/> if exists; otherwise null.</returns>
    public static string? GetDisplayDescription(this SnapshotTesting.GenerationOptions value) => null;

    /// <summary>Gets the value of the description from applied <see cref="global::System.ComponentModel.DescriptionAttribute"/>.</summary>
    /// <param name="value">A(n) <see cref="SnapshotTesting.GenerationOptions"/> enum value from which the attribute value is read.</param>
    /// <returns>The description read from the applied <see cref="global::System.ComponentModel.DescriptionAttribute"/> if exists; otherwise null.</returns>
    public static string? GetDescription(this SnapshotTesting.GenerationOptions value) => null;

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.GenerationOptions" />.
    /// This method using case-sensitive parsing.
    /// </summary>
    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParse([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out SnapshotTesting.GenerationOptions result)
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

        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.None)))
        {
            result = SnapshotTesting.GenerationOptions.None;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.ToString)))
        {
            result = SnapshotTesting.GenerationOptions.ToString;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.Parse)))
        {
            result = SnapshotTesting.GenerationOptions.Parse;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.HasFlag)))
        {
            result = SnapshotTesting.GenerationOptions.HasFlag;
            return true;
        }

        return TryParseByName(value, false, out result);
    }
    
    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.GenerationOptions" />.
    /// This method using case-insensitive parsing.
    /// </summary>
    /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParseIgnoreCase([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] global::System.String? value, out SnapshotTesting.GenerationOptions result)
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

        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.None), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.GenerationOptions.None;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.ToString), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.GenerationOptions.ToString;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.Parse), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.GenerationOptions.Parse;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.HasFlag), global::System.StringComparison.OrdinalIgnoreCase))
        {
            result = SnapshotTesting.GenerationOptions.HasFlag;
            return true;
        }

        return TryParseByName(value, true, out result);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.GenerationOptions" />.
    /// This method using case-sensitive parsing.
    /// </summary>
    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParse(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.GenerationOptions result) =>
        TryParseSpan(value, global::System.StringComparison.Ordinal, out result);
    
    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to <see cref="SnapshotTesting.GenerationOptions" />.
    /// This method using case-insensitive parsing.
    /// A parameter specifies whether the operation is case-insensitive.
    /// </summary>
    /// <param name="value">The span representation of the name or numeric value of one or more enumerated constants.</param>
    /// <param name="result">When this method returns <see langword="true"/>, an object containing an enumeration constant representing the parsed value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; <see langword="false"/> otherwise.</returns>
    public static global::System.Boolean TryParseIgnoreCase(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.GenerationOptions result) =>
        TryParseSpan(value, global::System.StringComparison.OrdinalIgnoreCase, out result);

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.String FormatNumberAsHex(SnapshotTesting.GenerationOptions data) => data switch
    {
        SnapshotTesting.GenerationOptions.None => "00",
        SnapshotTesting.GenerationOptions.ToString => "01",
        SnapshotTesting.GenerationOptions.Parse => "02",
        SnapshotTesting.GenerationOptions.HasFlag => "04",
        _ => global::System.String.Create(sizeof(global::System.Byte) * 2, global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.GenerationOptions, global::System.Byte>(ref data), (buffer, value) =>
        {
            global::System.UInt32 difference = ((value & 0xF0U) << 4) + (value & 0x0FU) - 0x8989U;
            global::System.UInt32 packedResult = ((((global::System.UInt32)(-(global::System.Int32)difference & 0x7070U)) >> 4) + difference + 0xB9B9U) | 0U;

            buffer[1] = (global::System.Char)(packedResult & 0xFFU);
            buffer[0] = (global::System.Char)(packedResult >> 8);
        })
    };

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static global::System.Boolean TryParseSpan(global::System.ReadOnlySpan<global::System.Char> value, global::System.StringComparison comparison, out SnapshotTesting.GenerationOptions result)
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

        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.None).AsSpan(), comparison))
        {
            result = SnapshotTesting.GenerationOptions.None;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.ToString).AsSpan(), comparison))
        {
            result = SnapshotTesting.GenerationOptions.ToString;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.Parse).AsSpan(), comparison))
        {
            result = SnapshotTesting.GenerationOptions.Parse;
            return true;
        }
        if (value.Equals(nameof(SnapshotTesting.GenerationOptions.HasFlag).AsSpan(), comparison))
        {
            result = SnapshotTesting.GenerationOptions.HasFlag;
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
    
     private static global::System.Boolean TryParseAsNumber(global::System.ReadOnlySpan<global::System.Char> value, out SnapshotTesting.GenerationOptions result)
     {
        const global::System.Globalization.NumberStyles NumberStyle = global::System.Globalization.NumberStyles.AllowLeadingSign | global::System.Globalization.NumberStyles.AllowTrailingWhite;
        global::System.Globalization.NumberFormatInfo numberFormat = global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
        global::System.Boolean status = global::System.Byte.TryParse(value, NumberStyle, numberFormat, out var parseResult);

        if (status)
        {
            result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.Byte, SnapshotTesting.GenerationOptions>(ref parseResult);
            return true;
        }
        
        result = default;
        return false;
    }
    
    private static global::System.Boolean TryParseByName(global::System.ReadOnlySpan<global::System.Char> value, global::System.Boolean ignoreCase, out SnapshotTesting.GenerationOptions result)
    {
        global::System.String[] enumNames = _names;
        global::System.Byte[] enumValues = _underlyingValues;
        global::System.Boolean parsed = true;
        global::System.Byte localResult = 0;

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
            result = global::System.Runtime.CompilerServices.Unsafe.As<global::System.Byte, SnapshotTesting.GenerationOptions>(ref localResult);
            return true;
        }

        result = default;
        return false;
    }

    private static global::System.String? FormatFlagNames(SnapshotTesting.GenerationOptions value) => value switch
    {
        SnapshotTesting.GenerationOptions.None => nameof(SnapshotTesting.GenerationOptions.None),
        SnapshotTesting.GenerationOptions.ToString => nameof(SnapshotTesting.GenerationOptions.ToString),
        SnapshotTesting.GenerationOptions.Parse => nameof(SnapshotTesting.GenerationOptions.Parse),
        SnapshotTesting.GenerationOptions.HasFlag => nameof(SnapshotTesting.GenerationOptions.HasFlag),
        _ => ProcessMultipleFlagsNames(value)
    };

    private static global::System.String? ProcessMultipleFlagsNames(SnapshotTesting.GenerationOptions value)
    {
        global::System.Byte resultValue = global::System.Runtime.CompilerServices.Unsafe.As<SnapshotTesting.GenerationOptions, global::System.Byte>(ref value);

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
    private static global::System.Boolean TryFindFlagsNames(global::System.Byte resultValue, global::System.Span<global::System.Int32> foundItems, out global::System.Int32 resultLength, out global::System.Int32 foundItemsCount)
    {
        // Now look for multiple matches, storing the indices of the values into our span.
        resultLength = 0;
        foundItemsCount = 0;

        global::System.Int32 index = MembersCount - 1;
        global::System.Byte[] values = _underlyingValues;
        global::System.String[] names = _names;

        while (true)
        {
            global::System.Byte currentValue = values[index];
            if (index == 0 && currentValue == 0)
            {
                break;
            }

            if ((resultValue & currentValue) == currentValue)
            {
                resultValue = (global::System.Byte)(resultValue & ~currentValue);
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

    [global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining)] // https://github.com/dotnet/runtime/issues/78300
    private static global::System.FormatException CreateInvalidFormatSpecifierException() =>
        new global::System.FormatException("Format string can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\".");
}

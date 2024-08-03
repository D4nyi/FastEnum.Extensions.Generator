[![Build & Test](https://github.com/D4nyi/FastEnum.Extensions.Generator/actions/workflows/build.yml/badge.svg)](https://github.com/D4nyi/FastEnum.Extensions.Generator/actions/workflows/build.yml)

# FastEnum.Extensions.Generator
Source Generator for enums to create extension methods with basic functionality.

## Usage

Use the `[Extensions]` on your enums, so the source generator will generate the extensions for those enums:

## Feature

- MembersCount (field)
- GetValues
- GetUnderlyingValues
- GetNames
- HasFlag
- IsDefined
- FastToString
- FastToString with format option
- GetDescription
- TryParse (string/System.ReadOnlySpan<char>)
- TryParseIgnoreCase (string/System.ReadOnlySpan<char>)

_**Note:**_
I'm trying to make the generated code behave the same as the .NET implementation.
If you find any differences please let me know.

## Limitations

- Only actively supported .NET versions are supported. See: [supported version list](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core#lifecycle)
- .NET Framework is not supported
- Generation extensions for enums nested in classes with generic type parameters are not supported.

## Example

```csharp
using FastEnum;

namespace ToStringExample;

public class NestingClass
{
    [Extensions]
    public enum NestedInClassEnum
    {
        None
    }
}

[Extensions, Flags]
public enum Color
{
    [Description("Crimson Red")]
    Red = 0x990000,
    [Display(Name = "Pine", Description = "Pine")]
    Green = 0x166138,
    [EnumMember(Value = "Sky")]
    Blue = 0x87CEEB
}

[Extensions, Flags]
public enum GenerationOptions : byte
{
    None = 0,
    ToString = 1,
    Parse = 2,
    HasFlag = 4
}
```

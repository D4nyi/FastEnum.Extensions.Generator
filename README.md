# FastEnumToString
Source Generator for enums to create extension methods with basic functionality.


## Usage

Use the `[Extensions]` on your enums, so the source generator will genrate the extensions for those enums:

## Feature

- MembersCount (field)
- GetValues *
- GetUnderlyingValues *
- GetNames *
- HasFlag *
- IsDefined *
- FastToString *
- FastToString with format option *
- GetDescription
- TryParse (string/System.ReadOnlySpan<char>)
- TryParseIgnoreCase (string/System.ReadOnlySpan<char>)

_**Note:**_
I'm trying to make the generated code behave the same as the .NET implementation.
Yet I tested most of the scenarios for the features (methods) marked with an asterisc (*) in the Features list.
If you find any differences please let me know.

## Limitations

- Only actively supported .NET versions are supperted. See: [supported version list](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core#lifecycle)
- .NET Framwork is not supported
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

[Extensions]
public enum Color : System.Byte
{
    Red,
    Green,
    Blue,
}

[Extensions, Flags]
public enum Options
{
    None = 0,
    ToString = 1,
    Parse = 2,
    HasFlag = 4,
}
```

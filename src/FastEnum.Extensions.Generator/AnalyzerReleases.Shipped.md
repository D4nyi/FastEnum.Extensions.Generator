; Shipped analyzer releases
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 1.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
ETS1001 | Design   | Warning  | Enum cannot be private, we are currently unable to create extensions.
ETS1002 | Design   | Warning  | Enum's underlying type cannot be determined  therefore we are unable to create extensions.
ETS1003 | Design   | Warning  | Extension generation restriction, extension generation for enum's nested in generic types are available.

## Release 2.0

### Changed Rules

Rule ID | New Category                           | New Severity | Old Category | Old Severity | Notes
--------|----------------------------------------|--------------|--------------|--------------|------
ETS1001 | FastEnumToString.EnumToStringGenerator | Warning      | Design       | Warning      | Enum cannot be private, we are currently unable to create extensions.
ETS1002 | FastEnumToString.EnumToStringGenerator | Warning      | Design       | Warning      | Enum's underlying type cannot be determined  therefore we are unable to create extensions.
ETS1003 | FastEnumToString.EnumToStringGenerator | Warning      | Design       | Warning      | Extension generation restriction, extension generation for enum's nested in generic types are available.

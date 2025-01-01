; Shipped analyzer releases
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 1.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
ETS1001 | Design   | Warning  | Enum cannot be private, we are currently unable to create extensions
ETS1002 | Design   | Warning  | Enum's underlying type cannot be determined  therefore we are unable to create extensions
ETS1003 | Design   | Warning  | Extension generation restriction, extension generation for enum's nested in generic types are available

## Release 2.0

### Changed Rules

Rule ID | New Category                           | New Severity | Old Category | Old Severity | Notes
--------|----------------------------------------|--------------|--------------|--------------|-------
ETS1001 | FastEnumToString.EnumToStringGenerator | Warning      | Design       | Warning      | Enum cannot be private, we are currently unable to create extensions
ETS1002 | FastEnumToString.EnumToStringGenerator | Warning      | Design       | Warning      | Enum's underlying type cannot be determined  therefore we are unable to create extensions
ETS1003 | FastEnumToString.EnumToStringGenerator | Warning      | Design       | Warning      | Extension generation restriction, extension generation for enum's nested in generic types are available

## Release 3.0

### New Rules

Rule ID | Category | Severity  | Notes
--------|----------|-----------|-------
EEG1001 | Usage    | Error     | [EEG1001](https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1001-invalid-visibility-modifier)
EEG1002 | Usage    | Warning   | [EEG1002](https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1002-invalid-backing-type)
EEG1003 | Usage    | Warning   | [EEG1003](https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1003-invalid-nesting-type)
EEG1004 | Usage    | Warning   | [EEG1004](https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1004-multiple-nesting-type)

### Removed Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
ETS1001 | Design   | Warning  | Enum cannot be private, we are currently unable to create extensions
ETS1002 | Design   | Warning  | Enum's underlying type cannot be determined  therefore we are unable to create extensions
ETS1003 | Design   | Warning  | Extension generation restriction, extension generation for enum's nested in generic types are available
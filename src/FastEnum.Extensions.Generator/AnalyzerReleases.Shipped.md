; Shipped analyzer releases
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Relese 1.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
ETS1001 | Design   | Warning  | Enum cannot be private, we are currently unable to create extensions.
ETS1002 | Design   | Warning  | Enum's underlying type cannot be determined  therefore we are unable to create extensions.
ETS1003 | Design   | Warning  | Extension generation restriction, extension generation for enum's nested in generic types are anavilable.
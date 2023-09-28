using System.Runtime.CompilerServices;

[assembly: System.CLSCompliant(false)]
[assembly: System.Reflection.AssemblyVersion(Assembly.CorrectVersion)]
[assembly: System.Reflection.AssemblyInformationalVersion(Assembly.Version)]
[assembly: System.Reflection.AssemblyFileVersion(Assembly.CorrectVersion)]

[assembly: InternalsVisibleTo("FastEnumToString.Test", AllInternalsVisible = true)]

internal static class Assembly
{
    internal const string Version = "1.0.0";
    internal const string CorrectVersion = "1.0.0";
}


/*
Extract:
options.GlobalOptions.TryGetValue("build_property.FastEnumDefaultBehaviour", out string? defaultBehaviour);
if (String.IsNullOrEmpty(defaultBehaviour))
{
    _defaultToStringBehaviour = 0;
    return;
}

defaultBehaviour = defaultBehaviour!.Trim();

if (defaultBehaviour.Equals("first", StringComparison.OrdinalIgnoreCase))
{
    _defaultToStringBehaviour = 1;
}
else if (defaultBehaviour.Equals("throw", StringComparison.OrdinalIgnoreCase))
{
    _defaultToStringBehaviour = 2;
}
else
{
    _defaultToStringBehaviour = 0;
}

----------------------------------------------------------------------------------------------------------------------
Use:
int defaultBranch = _currentSpec.DefaultValue < 0
    ? defaultBehaviour
    : _currentSpec.DefaultValue;

sb.Append(nesting1Indent).Append("default: ");
switch (defaultBranch)
{
    case 0:
        sb.Append("return ((").Append(_currentSpec.UnderlyingType).AppendLine(")value).ToString();");
        break;
    case 1:
        sb.Append("return nameof(").Append(_currentSpec.FullName).Append('.').Append(_currentSpec.Members[0].Name).AppendLine(";");
        break;
    default: // 2
        sb.Append("throw new global::System.ArgumentOutOfRangeException(nameof(value), value, $\"Value: '{(").Append(_currentSpec.UnderlyingType).AppendLine(")value}' cannot be found in the provided enum type!\");");
        break;
}
*/
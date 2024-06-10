using System.Runtime.CompilerServices;
using FastEnum.Extensions.Generator;

[assembly: System.CLSCompliant(false)]
[assembly: System.Reflection.AssemblyVersion(Assembly.CorrectVersion)]
[assembly: System.Reflection.AssemblyInformationalVersion(Assembly.Version)]
[assembly: System.Reflection.AssemblyFileVersion(Assembly.CorrectVersion)]

[assembly: InternalsVisibleTo("FastEnum.Helpers.Generator.Testst", AllInternalsVisible = true)]

namespace FastEnum.Extensions.Generator;

internal static class Assembly
{
    internal const string Version = "1.0.0-preview.4";
    internal const string CorrectVersion = "1.0.0";
}

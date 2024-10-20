using System.Runtime.CompilerServices;
using FastEnum.Extensions.Generator;

[assembly: CLSCompliant(false)]
[assembly: System.Reflection.AssemblyVersion(Assembly.CorrectVersion)]
[assembly: System.Reflection.AssemblyInformationalVersion(Assembly.Version)]
[assembly: System.Reflection.AssemblyFileVersion(Assembly.CorrectVersion)]

[assembly: InternalsVisibleTo("FastEnum.Extensions.Generator", AllInternalsVisible = true)]

namespace FastEnum.Extensions.Generator;

internal static class Assembly
{
    internal const string Version = "1.3.0";
    internal const string CorrectVersion = "1.3.0";
}

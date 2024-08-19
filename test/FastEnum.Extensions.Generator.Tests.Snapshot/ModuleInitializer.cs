using System.Runtime.CompilerServices;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() =>
        VerifySourceGenerators.Initialize();
}

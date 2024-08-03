using System.Runtime.CompilerServices;

namespace FastEnum.Extensions.Generator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() =>
        VerifySourceGenerators.Initialize();
}

namespace FastEnum.Extensions.Generator;

internal static class Constants
{
    internal const string Version = "1.4.0";

    internal const string EnumMemberAttributeFullName = "System.Runtime.Serialization.EnumMemberAttribute";
    internal const string DisplayAttributeFullName = "System.ComponentModel.DataAnnotations.DisplayAttribute";
    internal const string DescriptionAttributeFullName = "System.ComponentModel.DescriptionAttribute";
    internal const string FlagsAttributeFullName = "System.FlagsAttribute";

    internal const string EnumExtensionsGenerator = "FastEnum.Extensions.Generator.EnumExtensionsGenerator";

    internal const string ExtensionsAttributeFullName = "FastEnum.ExtensionsAttribute";
    internal const string AttributesFile = "FastEnumExtensionsAttribute.g.cs";

    internal static readonly string[] UnsupportedVisibilityModifiers = ["private", "protected", "protected internal", "file"];

    internal const string InitialExtraction = nameof(InitialExtraction);
    internal const string RemovingNulls = nameof(RemovingNulls);
    internal const string CollectedGenerationData = nameof(CollectedGenerationData);


    internal const string Attributes =
        $$"""
          // <auto-generated/>

          namespace FastEnum
          {
              /// <summary>Marks an enum to generate optimized extensions for it.</summary>
              [global::System.CodeDom.Compiler.GeneratedCode("{{EnumExtensionsGenerator}}", "{{Version}}")]
              [global::System.AttributeUsage(global::System.AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
              internal sealed class ExtensionsAttribute : global::System.Attribute { }
          }
          """;

    internal const string FileHeader =
        """
        // <auto-generated/>

        #nullable enable annotations
        #nullable disable warnings

        """;

    internal const string EmptyEnum =
        """

            // There is no member defined for this enum.

        """;
}

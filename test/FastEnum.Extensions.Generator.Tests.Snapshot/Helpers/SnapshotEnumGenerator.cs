namespace FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

internal static class SnapshotEnumGenerator
{
    internal static Dictionary<string, string> Enums()
    {
        Dictionary<string, string> enums = new()
        {
            {
                "Color", """
                         namespace SnapshotTesting
                         {
                             [FastEnum.Extensions]
                             public enum Color
                             {
                                 [System.ComponentModel.Description("Crimson red")]
                                 Red = 0,
                                 [System.Runtime.Serialization.EnumMember(Value = "Pine")]
                                 Green = 1,
                                 [System.ComponentModel.DataAnnotations.Display(Name = "Sky", Description = "Sky")]
                                 Blue = 2,
                             }
                         }
                         """
            },
            {
                "GenerationOptions", """
                                     namespace SnapshotTesting;

                                     [FastEnum.Extensions, System.Flags]
                                     public enum GenerationOptions : byte
                                     {
                                         None = 0,
                                         [System.ComponentModel.Description("generate ToString")]
                                         ToString = 1,
                                         [System.Runtime.Serialization.EnumMember(Value = "generate Parse")]
                                         Parse = 2,
                                         [System.ComponentModel.DataAnnotations.Display(Name = "generate HasFlag", Description = "generate HasFlag")]
                                         HasFlag = 4
                                     }
                                     """
            },
            {
                "NestedInGenericClass", """
                                        namespace SnapshotTesting
                                        {
                                            public class NestingGenericClass<T>
                                            {
                                                [FastEnum.Extensions]
                                                public enum NestedInGenericClass
                                                {
                                                    None
                                                }
                                            }
                                        }
                                        """
            },
            {
                "EmptyEnum", """
                             namespace SnapshotTesting
                             {
                                 [FastEnum.Extensions]
                                 public enum EmptyEnum
                                 {
                                 }
                             }
                             """
            }
        };

        AddNestedTypesWithVisibilityOnClass(enums);
        AddNestedTypesWithVisibilityOnEnum(enums);
        AddVisibility(enums);
        AddBackingTypes(enums);

        return enums;
    }

    private static void AddNestedTypesWithVisibilityOnClass(Dictionary<string, string> enums)
    {
        TypeUsage[] types =
        [
            new("Class", "class"), new("StaticClass", "static class"),
            new("SealedClass", "sealed class"), new("Record", "record"),
            new("RecordClass", "record class"), new("RecordSealedClass", "record sealed class"),
            new("Struct", "struct"), new("ReadonlyStruct", "readonly struct"),
            new("RecordStruct", "record struct"), new("ReadonlyRecordStruct", "readonly record struct"),
            new("InternalClass", "class", "internal")
        ];

        foreach (TypeUsage type in types)
        {
            enums.Add(type.Key, $$"""
                                  namespace SnapshotTesting
                                  {
                                      {{type.Visibility}} {{type.TypeName}} NestingType
                                      {
                                          [FastEnum.Extensions]
                                          public enum NestedIn{{type.Key}}
                                          {
                                              None
                                          }
                                      }
                                  }
                                  """);
        }
    }

    private static void AddNestedTypesWithVisibilityOnEnum(Dictionary<string, string> enums)
    {
        TypeUsage[] types =
        [
            new("ProtectedClass", "class", "protected"),
            new("ProtectedInternalClass", "class", "protected internal"),
            new("PrivateClass", "class", "private")
        ];

        foreach (TypeUsage type in types)
        {
            enums.Add(type.Key, $$"""
                                  namespace SnapshotTesting
                                  {
                                      public {{type.TypeName}} NestingType
                                      {
                                          [FastEnum.Extensions]
                                          {{type.Visibility}} enum NestedIn{{type.Key}}
                                          {
                                              None
                                          }
                                      }
                                  }
                                  """);
        }
    }

    private static void AddVisibility(Dictionary<string, string> enums)
    {
        TypeUsage[] types =
        [
            new("Internal", "", "internal"),
            new("Public", "", "internal"),
            new("File", "", "file")
        ];


        foreach (TypeUsage type in types)
        {
            enums.Add(type.Key, $$"""
                                  namespace SnapshotTesting
                                  {
                                      [FastEnum.Extensions]
                                      {{type.Visibility}} enum Is{{type.Key}}
                                      {
                                          None
                                      }
                                  }
                                  """);
        }
    }

    private static void AddBackingTypes(Dictionary<string, string> enums)
    {
        TypeUsage[] types =
        [
            new("Byte", "byte"),
            new("SByte", "sbyte"),
            new("Short", "short"),
            new("UShort", "ushort"),
            new("Int", "int"),
            new("UInt", "uint"),
            new("Long", "long"),
            new("ULong", "ulong")
        ];

        foreach (TypeUsage type in types)
        {
            enums.Add(type.Key, $$"""
                                  namespace SnapshotTesting
                                  {
                                      [FastEnum.Extensions]
                                      public enum Is{{type.Key}} : {{type.Key}}
                                      {
                                          None
                                      }
                                  }
                                  """);
        }
    }

    private sealed record TypeUsage(string Key, string TypeName, string Visibility = "public");
}
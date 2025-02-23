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
                             [FastEnum.Attributes.ExtensionsAttribute]
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

                                     [FastEnum.Attributes.ExtensionsAttribute, System.Flags]
                                     public enum GenerationOptions : byte
                                     {
                                         None = 0,
                                         [System.ComponentModel.Description("generate ToString")]
                                         ToString = 1,
                                         [System.Runtime.Serialization.EnumMember(Value = "generate Parse")]
                                         Parse = 2,
                                         [System.ComponentModel.DataAnnotations.Display(Name = "generate HasFlag", Description = "generate HasFlag")]
                                         HasFlag = 4,
                                         [System.ComponentModel.DataAnnotations.Display(Description = "generate IsDefined")]
                                         IsDefined = 8
                                     }
                                     """
            },
            {
                "NoneUniqueOption", """
                                    namespace SnapshotTesting;

                                    [FastEnum.Attributes.ExtensionsAttribute]
                                    public enum NoneUniqueOption : UInt16
                                    {
                                        [System.Runtime.Serialization.EnumMember()]
                                        None = 0,

                                        [System.ComponentModel.Description()]
                                        ToString = 1,

                                        [System.ComponentModel.Description()]
                                        ToStringFormat = ToString,

                                        [System.ComponentModel.DataAnnotations.Display(Name = "Parse")]
                                        Parse = 2,

                                        [System.ComponentModel.DataAnnotations.Display()]
                                        IsDefined = 8
                                    }
                                    """
            },
            {
                "NoneUniqueOptions", """
                                    namespace SnapshotTesting;

                                    [FastEnum.Attributes.ExtensionsAttribute, System.Flags]
                                    public enum NoneUniqueOptions : System.UInt64
                                    {
                                        [System.Runtime.Serialization.EnumMember(Value = "None")]
                                        None = 0,

                                        [System.ComponentModel.Description("ToString")]
                                        ToString = 1,

                                        [System.ComponentModel.Description("ToStringFormat")]
                                        ToStringFormat = ToString,

                                        [System.ComponentModel.DataAnnotations.Display(Name = "Parse", Description = "Parse")]
                                        Parse = 2
                                    }
                                    """
            },
            {
                "NestedInGenericClass", """
                                        namespace SnapshotTesting
                                        {
                                            public class NestingGenericClass<T>
                                            {
                                                [FastEnum.Attributes.ExtensionsAttribute]
                                                public enum NestedInGenericClass
                                                {
                                                    None
                                                }
                                            }
                                        }
                                        """
            },
            {
                "NestedWithInconsistentAccessibility", """
                                                       namespace SnapshotTesting
                                                       {
                                                           internal class NestingClass
                                                           {
                                                               [FastEnum.Attributes.ExtensionsAttribute]
                                                               public enum NestedWithInconsistentAccessibility
                                                               {
                                                                   None
                                                               }
                                                           }
                                                       }
                                                       """
            },
            {
                "NestedInGenericClassWithInconsistentAccessibility", """
                                                                     namespace SnapshotTesting
                                                                     {
                                                                         internal class NestingGenericInconsistentClass<T>
                                                                         {
                                                                             [FastEnum.Attributes.ExtensionsAttribute]
                                                                             public enum NestedInGenericClassWithInconsistentAccessibility
                                                                             {
                                                                                 None
                                                                             }
                                                                         }
                                                                     }
                                                                     """
            },
            {
                "NestedInMultipleClass", """
                                         namespace SnapshotTesting
                                         {
                                             public class NestingGrandParentClass
                                             {
                                                 public class NestingParentClass
                                                 {
                                                     [FastEnum.Attributes.ExtensionsAttribute]
                                                     public enum NestedInMultipleClass
                                                     {
                                                         None
                                                     }
                                                 }
                                             }
                                         }
                                         """
            },
            {
                "NestedInMultipleClassWithAGenericClass", """
                                                          namespace SnapshotTesting
                                                          {
                                                              public class NestingGrandParentClass_Generic
                                                              {
                                                                  public class NestingParentGenericClass<T>
                                                                  {
                                                                      [FastEnum.Attributes.ExtensionsAttribute]
                                                                      public enum NestedInMultipleClassWithAGenericClass
                                                                      {
                                                                          None
                                                                      }
                                                                  }
                                                              }
                                                          }
                                                          """
            },
            {
                "EmptyEnum", """
                             namespace SnapshotTesting
                             {
                                 [FastEnum.Attributes.ExtensionsAttribute]
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
            new("RecordClass", "record class"), new("RecordSealedClass", "sealed record class"),
            new("Struct", "struct"), new("ReadonlyStruct", "readonly struct"),
            new("RecordStruct", "record struct"), new("ReadonlyRecordStruct", "readonly record struct")
        ];

        foreach (TypeUsage type in types)
        {
            enums.Add(type.Key, $$"""
                                  namespace SnapshotTesting
                                  {
                                      {{type.Visibility}} {{type.TypeName}} NestingType{{type.Key}}
                                      {
                                          [FastEnum.Attributes.ExtensionsAttribute]
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
                                      public {{type.TypeName}} NestingType{{type.Key}}
                                      {
                                          [FastEnum.Attributes.ExtensionsAttribute]
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
                                      [FastEnum.Attributes.ExtensionsAttribute]
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
                                      [FastEnum.Attributes.ExtensionsAttribute]
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

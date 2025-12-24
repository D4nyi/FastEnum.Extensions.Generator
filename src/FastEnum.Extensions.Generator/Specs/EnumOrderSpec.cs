namespace FastEnum.Extensions.Generator.Specs;

[Flags]
internal enum EnumOrderSpec
{
    None = 0,
    SingleValue = 1,
    SequentialWithZero = 2,
    SequentialWithoutZero = 4,
    NotSequential = 8
}

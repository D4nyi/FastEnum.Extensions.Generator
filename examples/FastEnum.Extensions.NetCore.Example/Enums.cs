using FastEnum;

namespace ToStringExample;

public class NestingClass
{
    [Extensions]
    public enum NestedInClassEnum
    {
        None,
        First,
        Second,
        Third,
        Fourth,
    }
}

[Extensions]
public enum Color : Byte
{
    Red = 1,
    Green = 2,
    Blue = 4,
}

[Extensions, Flags]
public enum Options
{
    None = 0,
    ToString = 1,
    Parse = 2,
    HasFlag = 4,
}

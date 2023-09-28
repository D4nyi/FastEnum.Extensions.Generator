using FastEnum;

namespace ToStringExample;

public class NestingClass<T, K>
    where T : struct
    where K : class, new()
{
    [Extensions]
    public enum NestedInClassEnum
    {
        None
    }
}

[Extensions]
public enum Color : System.Byte
{
    Red = 1,
    Green = 2,
    Blue = 4,
}

[Extensions]
public enum Options
{
    None = 0,
    ToString = 1,
    Parse = 2,
    HasFlag = 4,
}
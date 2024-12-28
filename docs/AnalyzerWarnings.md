# Rules

## ETS1001: Invalid visibility modifier

### Category
Usage

### Description
Current generation strategy does not support extension generation for enums with `protected, protected internal` or `private` visibility modifiers.

Therefore, only `public` and `internal` visibility modifiers are allowed.

### Non-compliant code
```csharp
public sealed class ExampleClass
{
    // ...
    
    private enum NonCompliant1
    {
        None = 0
    }
    
    protected enum NonCompliant2
    {
        None = 0
    }
    
    protected internal enum NonCompliant3
    {
        None = 0
    }
}
```

### Compliant code
```csharp
public sealed class ExampleClass
{
    // ...
    
    public enum Compliant1
    {
        None = 0
    }
    
    internal enum Compliant2
    {
        None = 0
    }
}
```

## ETS1002: Invalid backing type

### Category
Usage

### Description
Please use one of the following types as the backing type: `byte, sbyte, short, ushort, int, uint, long, ulong`.

### Non-compliant code
```csharp
public enum NonCompliant1 : float
{
    None = 0
}

internal enum NonCompliant2 : decimal
{
    None = 0
}
```

### Compliant code
```csharp
public enum Compliant1 : byte
{
    None = 0
}

internal enum Compliant2 : uint
{
    None = 0
}
```

## ETS1002: Invalid nesting type

### Category
Usage

### Description
Please define your enum outside a generic type.

### Non-compliant code
```csharp
public sealed class ExampleClass<T>
{
    // ...
    
    public enum NonCompliant
    {
        None = 0
    }
}
```

#### Compliant code
```csharp
public sealed class ExampleClass<T>
{
    // ...
}

internal enum Compliant
{
    None = 0
}
```
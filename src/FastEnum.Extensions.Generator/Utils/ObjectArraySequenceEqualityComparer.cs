using System.Collections.Immutable;

namespace FastEnum.Extensions.Generator.Utils;

internal sealed class ObjectArraySequenceEqualityComparer<T> : IEqualityComparer<T[]>
{
    public bool Equals(T[] left, T[] right)
    {
        // Check for identical references or both by default
        if (left.Equals(right))
        {
            return true;
        }

        // Length check
        if (left.Length != right.Length)
        {
            return false;
        }

        // Element-by-element comparison
        for (int i = 0; i < left.Length; i++)
        {
            bool areEqual = left[i] is { } leftElem
                ? leftElem.Equals(right[i])
                : right[i] is null;

            if (!areEqual)
            {
                return false;
            }
        }

        return true;
    }

    public int GetHashCode(T[] obj)
    {
        if (obj.IsEmpty)
        {
            return 0;
        }

        return obj.Aggregate(17, (acc, current) => acc * 31 + current?.GetHashCode() ?? 0);
    }
}

internal sealed class ObjectImmutableArraySequenceEqualityComparer<T> : IEqualityComparer<ImmutableArray<T>>
{
    public bool Equals(ImmutableArray<T> left, ImmutableArray<T> right)
    {
        // Check for identical references or both by default
        if (left.Equals(right))
        {
            return true;
        }

        // Length check
        if (left.Length != right.Length)
        {
            return false;
        }

        // Element-by-element comparison
        for (int i = 0; i < left.Length; i++)
        {
            bool areEqual = left[i] is { } leftElem
                ? leftElem.Equals(right[i])
                : right[i] is null;

            if (!areEqual)
            {
                return false;
            }
        }

        return true;
    }

    public int GetHashCode(ImmutableArray<T> obj)
    {
        if (obj.IsDefaultOrEmpty)
        {
            return 0;
        }

        return obj.Aggregate(17, (acc, current) => acc * 31 + current?.GetHashCode() ?? 0);
    }
}

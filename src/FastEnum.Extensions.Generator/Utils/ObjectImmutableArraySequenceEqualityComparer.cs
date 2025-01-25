using System.Collections.Immutable;

namespace FastEnum.Extensions.Generator.Utils;

internal sealed class ObjectImmutableArraySequenceEqualityComparer<T> : IEqualityComparer<ImmutableArray<T>>
{
    public bool Equals(ImmutableArray<T> left, ImmutableArray<T> right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

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

    public int GetHashCode(ImmutableArray<T> obj) => Enumerable.Aggregate(obj, 0, (current, t) => (current, t).GetHashCode());
}

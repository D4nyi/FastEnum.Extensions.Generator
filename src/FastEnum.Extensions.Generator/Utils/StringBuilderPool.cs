using System.Text;

namespace FastEnum.Extensions.Generator.Utils;

/// <summary>
/// StringBuilderCache to cache one StringBuilder.
/// </summary>
/// <remarks>
/// From experience there is no need for multiple item cache or pool.
/// </remarks>
internal static class StringBuilderCache
{
    private const int DefaultBuilderCapacity = 25_000;
    private const int MaxBuilderCapacity = 50_000; // Add a maximum capacity

    private static StringBuilder? _fastItem;

    /// <summary>
    /// Retrieves a <see cref="StringBuilder"/> from the object pool.
    /// If the pool is empty a new one will be created.
    /// </summary>
    /// <returns>The retrieved or the new created <see cref="StringBuilder"/>.</returns>
    internal static StringBuilder Get()
    {
        StringBuilder? item = _fastItem;
        if (item is not null && Interlocked.CompareExchange(ref _fastItem, null, item) == item)
        {
            return item;
        }

        return new StringBuilder(DefaultBuilderCapacity);
    }

    /// <summary>Adds the provided <see cref="StringBuilder"/> to the object pool.</summary>
    /// <param name="sb"><see cref="StringBuilder"/> that should be (re)added to the object pool.</param>
    /// <returns>The <see cref="string"/> held by the provided <see cref="StringBuilder"/>.</returns>
    /// <remarks><code>string value = StringBuilderCache.Return(sb);</code></remarks>
    internal static string Return(StringBuilder sb)
    {
        string tmp = sb.ToString();

        // Only cache if the StringBuilder hasn't grown too large
        if (_fastItem is null && sb.Length <= MaxBuilderCapacity)
        {
            _ = Interlocked.CompareExchange(ref _fastItem, sb, null);
        }

        sb.Clear();

        return tmp;
    }
}

using System.Text;

namespace FastEnum.Extensions.Generator.Utils;

/// <summary>
/// .NET Core-like object pool for StringBuilder
/// </summary>
/// <remarks>
/// Our own cheesy object pool since we can't use the .NET core version
/// Logic borrowed from:
/// https://github.com/davidfowl/LoggingGenerator/blob/939125e726d4b67cfb827c36dbbbaefea767fb69/Microsoft.Helpers.Logging.Generators/LoggingGenerator.cs#L346
/// </remarks>
internal static class StringBuilderPool
{
    private const int DefaultStackCapacity   = 4;
    private const int DefaultBuilderCapacity = 4 * 1024;
    private static StringBuilder? _fastItem;

    private static readonly Stack<StringBuilder> _builders = new(DefaultStackCapacity);

    /// <summary>
    /// Initializes the object pool with a few items.
    /// </summary>
    static StringBuilderPool()
    {
        for (int i = 0; i < DefaultStackCapacity; i++)
        {
            _builders.Push(new StringBuilder(DefaultBuilderCapacity));
        }
    }

    /// <summary>
    /// Retrieves a <see cref="StringBuilder"/> from the object pool.
    /// If the pool is empty a new one will be created.
    /// </summary>
    /// <returns>The retrieved or the new created <see cref="StringBuilder"/>.</returns>
    internal static StringBuilder Get()
    {
        if (_builders.Count == 0)
        {
            return new StringBuilder(DefaultBuilderCapacity);
        }

        StringBuilder? item = _fastItem;
        if (item is null || Interlocked.CompareExchange(ref _fastItem, null, item) != item)
        {
            item = _builders.Pop();
        }

        return item;
    }

    /// <summary>
    /// Adds the provided <see cref="StringBuilder"/> to the object pool.
    /// <br/>
    /// Usage:
    /// <code>
    /// string value = StringBuilderPool.Return(sb);
    /// </code>
    /// </summary>
    /// <param name="sb"><see cref="StringBuilder"/> that should be (re)added to the object pool.</param>
    /// <returns>The <see cref="string"/> held by the provided <see cref="StringBuilder"/>.</returns>
    internal static string Return(StringBuilder sb)
    {
        // cache one "sb"; then put the others back to the stack
        if (_fastItem is not null)
        {
            _ = Interlocked.CompareExchange(ref _fastItem, sb, null);
        }
        else
        {
            _builders.Push(sb);
        }

        string tmp = sb.ToString();

        sb.Clear();

        return tmp;
    }
}

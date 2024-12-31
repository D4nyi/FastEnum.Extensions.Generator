using System.Collections.Immutable;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Utils;

internal static class DistinctMemberExtension
{
    internal static ImmutableArray<EnumMemberSpec> ToDistinct(this ImmutableArray<EnumMemberSpec> members, bool hasFlags)
    {
        HashSet<EnumMemberSpec> set = [];

        foreach (EnumMemberSpec member in members)
        {
            if (!set.Add(member) && hasFlags)
            {
                set.Remove(member);
                set.Add(member);
            }
        }

        return [..set];
    }
}

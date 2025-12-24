using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Utils;

internal static class DistinctMemberExtension
{
    extension<T>(T[] array)
    {
        internal bool IsEmpty => array.Length == 0;
    }

    internal static EnumMemberSpec[] ToDistinct(this EnumMemberSpec[] members, bool hasFlags)
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

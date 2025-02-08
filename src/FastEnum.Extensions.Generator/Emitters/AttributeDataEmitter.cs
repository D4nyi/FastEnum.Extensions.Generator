using System.Globalization;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator.Emitters;

internal static class AttributeDataEmitter
{
    internal static void AddAttributeMethods(StringBuilder sb, EnumGenerationSpec spec)
    {
        AddGetEnumMemberValue(sb, spec);
        AddGetDisplayName(sb, spec);
        AddGetDisplayDescription(sb, spec);
        AddGetDescription(sb, spec);
    }

    private static void AddGetEnumMemberValue(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Gets the Value property from applied <see cref="global::System.Runtime.Serialization.EnumMemberAttribute"/>.</summary>
                    /// <param name="value">A(n) <see cref="{0}"/> enum value from which the attribute value is read.</param>
                    /// <returns>The value of <see cref="global::System.Runtime.Serialization.EnumMemberAttribute.Value"/> if exists; otherwise null.</returns>
                    public static global::System.String? GetEnumMemberValue(this {0} value)
                """, spec.FullName);

        AddAttributeMethodBody(sb, spec, static x => x.EnumMemberValue);
    }

    private static void AddGetDisplayName(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Gets the Name property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
                    /// <param name="value">A(n) <see cref="{0}"/> enum value from which the attribute value is read.</param>
                    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Name"/> if exists; otherwise null.</returns>
                    public static global::System.String? GetDisplayName(this {0} value)
                """, spec.FullName);

        AddAttributeMethodBody(sb, spec, static x => x.DisplayName);
    }

    private static void AddGetDisplayDescription(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Gets the Description property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.</summary>
                    /// <param name="value">A(n) <see cref="{0}"/> enum value from which the attribute value is read.</param>
                    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Description"/> if exists; otherwise null.</returns>
                    public static global::System.String? GetDisplayDescription(this {0} value)
                """, spec.FullName);

        AddAttributeMethodBody(sb, spec, static x => x.DisplayDescription);
    }

    private static void AddGetDescription(StringBuilder sb, EnumGenerationSpec spec)
    {
        sb
            .AppendFormat(CultureInfo.InvariantCulture,
                """
                    /// <summary>Gets the value of the description from applied <see cref="global::System.ComponentModel.DescriptionAttribute"/>.</summary>
                    /// <param name="value">A(n) <see cref="{0}"/> enum value from which the attribute value is read.</param>
                    /// <returns>The description read from the applied <see cref="global::System.ComponentModel.DescriptionAttribute"/> if exists; otherwise null.</returns>
                    public static global::System.String? GetDescription(this {0} value)
                """, spec.FullName);

        AddAttributeMethodBody(sb, spec, static x => x.Description);
    }

    private static void AddAttributeMethodBody(StringBuilder sb, EnumGenerationSpec spec, Func<AttributeValues, string?> accessor)
    {
        List<EnumMemberSpec> notNulls = spec.DistinctMembers.Where(x => accessor(x.Data) is not null).ToList();
        if (notNulls.Count == 0)
        {
            sb.AppendLine(" => null;").AppendLine();
            return;
        }

        sb
            .AppendLine(" => value switch")
            .AppendLine("    {");

        foreach (EnumMemberSpec member in notNulls)
        {
            sb
                .Append("        ").Append(member.FullName).Append(" => \"")
                .Append(accessor(member.Data)).AppendLine("\",");
        }

        sb
            .AppendLine("        _ => null")
            .AppendLine("    };")
            .AppendLine();
    }
}

using System.Text;

using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddAttributeMethods(StringBuilder sb)
    {
        AddGetEnumMemberValue(sb);
        AddGetDisplayName(sb);
        AddGetDisplayDescription(sb);
        AddGetDescription(sb);
    }

    private void AddGetEnumMemberValue(StringBuilder sb)
    {
        sb
            .Append(
                """
                    /// <summary>
                    /// Gets the Value property from applied <see cref="global::System.Runtime.Serialization.EnumMemberAttribute"/>.
                    /// </summary>
                    /// <returns>The value of <see cref="global::System.Runtime.Serialization.EnumMemberAttribute.Value"/> if exists; otherwise null.</returns>
                    public static string? GetEnumMemberValue(this 
                """);

        AddAttributeMethodBody(sb, static x => x.EnumMemberValue);
    }

    private void AddGetDisplayName(StringBuilder sb)
    {
        sb
            .Append(
                """
                    /// <summary>
                    /// Gets the Name property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.
                    /// </summary>
                    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Name"/> if exists; otherwise null.</returns>
                    public static string? GetDisplayName(this 
                """);

        AddAttributeMethodBody(sb, static x => x.DisplayName);
    }

    private void AddGetDisplayDescription(StringBuilder sb)
    {
        sb
            .Append(
                """
                    /// <summary>
                    /// Gets the Description property from applied <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute"/>.
                    /// </summary>
                    /// <returns>The value of <see cref="global::System.ComponentModel.DataAnnotations.DisplayAttribute.Description"/> if exists; otherwise null.</returns>
                    public static string? GetDisplayDescription(this 
                """);

        AddAttributeMethodBody(sb, static x => x.DisplayDescription);
    }

    private void AddGetDescription(StringBuilder sb)
    {
        sb
            .Append(
                """
                    /// <summary>
                    /// Gets the value of the description from applied <see cref="global::System.ComponentModel.DescriptionAttribute"/>.
                    /// </summary>
                    /// <returns>The description read from the applied <see cref="global::System.ComponentModel.DescriptionAttribute"/> if exists; otherwise null.</returns>
                    public static string? GetDescription(this 
                """);

        AddAttributeMethodBody(sb, static x => x.Description);
    }

    private void AddAttributeMethodBody(StringBuilder sb, Func<AttributeValues, string?> accessor)
    {
        sb.Append(_currentSpec.FullName).Append(" value)");

        List<EnumMemberSpec> notNulls = _currentSpec.Members.Where(x => accessor(x.Data) is not null).ToList();
        if (notNulls.Count == 0)
        {
            sb.AppendLine(" => null;").AppendLine();
            return;
        }

        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);

        sb
            .AppendLine(" => value switch")
            .Append(methodIndent).AppendLine("{");

        foreach (EnumMemberSpec member in notNulls)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => \"")
                .Append(accessor(member.Data)).AppendLine("\",");
        }

        sb
            .Append(methodBodyIndent).AppendLine("_ => null")
            .Append(methodIndent).AppendLine("};")
            .AppendLine();
    }
}

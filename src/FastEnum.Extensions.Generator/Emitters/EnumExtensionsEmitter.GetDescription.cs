using System.Text;
using FastEnum.Extensions.Generator.Specs;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private void AddGetDescription(StringBuilder sb)
    {
        string methodIndent = Get(Indentation.Method);
        string methodBodyIndent = Get(Indentation.MethodBody);
        
        sb
            .Append(
            """
                /// <summary>
                /// Gets the value of the description from applied <see cref="global::System.ComponentModel.DescriptionAttribute"/>.
                /// If no description is applied or the attribute or the enum value is not found then <see langword="null"/> is returned.
                /// </summary>
                /// <returns>The description read from the applied <see cref="global::System.ComponentModel.DescriptionAttribute"/> if exists; otherwise false.</returns>
                public static string? GetDescription(this 
            """)
            .Append(_currentSpec.FullName).Append(" value)");

        List<EnumMemberSpec> notNulls = _currentSpec.Members.Where(x => x.Description is not null).ToList();
        if (notNulls.Count == 0)
        {
            sb.AppendLine(" => null;").AppendLine();
            return;
        }
        
        if (notNulls.Count == 1)
        {
            EnumMemberSpec member = notNulls[0];
            sb.Append(" => value == ").Append(member.FullName)
                .Append(" ? \"").Append(member.Description).AppendLine("\" : null;").AppendLine();
            
            return;
        }

        sb
            .AppendLine(" => value switch")
            .Append(methodIndent).AppendLine("{");
        
        foreach (EnumMemberSpec member in notNulls)
        {
            sb
                .Append(methodBodyIndent).Append(member.FullName).Append(" => \"")
                .Append(member.Description).AppendLine("\",");
        }

        sb
            .Append(methodBodyIndent).AppendLine("_ => null")
            .Append(methodIndent).AppendLine("};")
            .AppendLine();
    }
}

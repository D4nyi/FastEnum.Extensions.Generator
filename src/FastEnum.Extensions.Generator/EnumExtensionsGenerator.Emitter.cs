using System.Text;

using FastEnum.Extensions.Generator.Emitters;
using FastEnum.Extensions.Generator.Specs;
using FastEnum.Extensions.Generator.Utils;

namespace FastEnum.Extensions.Generator;

public sealed partial class EnumExtensionsGenerator
{
    private static string Emit(EnumGenerationSpec enumGenerationSpec)
    {
        StringBuilder sb = StringBuilderPool.Get();

        GeneralEmitter.AddFileAndClassHeader(sb, enumGenerationSpec);

        if (enumGenerationSpec.Members.IsEmpty)
        {
            sb.Append(Constants.EmptyEnum);
        }
        else
        {
            GeneralEmitter.AddFieldsAndGetMethods(sb, enumGenerationSpec);
            GeneralEmitter.AddHasFlag(sb, enumGenerationSpec);
            GeneralEmitter.AddIsDefined(sb, enumGenerationSpec);
            ToStringEmitter.AddToString(sb, enumGenerationSpec);
            ToStringEmitter.AddToStringFormat(sb, enumGenerationSpec);
            AttributeDataEmitter.AddAttributeMethods(sb, enumGenerationSpec);
            TryParseStringEmitter.AddTryParseString(sb, enumGenerationSpec);
            TryParseSpanEmitter.AddTryParseSpan(sb, enumGenerationSpec);
            ToStringEmitter.AddFormatAsHexHelper(sb, enumGenerationSpec);
            TryParseSpanEmitter.AddTryParsePrivate(sb, enumGenerationSpec);
            ToStringEmitter.AddFormatFlagNames(sb, enumGenerationSpec);
            GeneralEmitter.AddPrivateHelperMethods(sb, enumGenerationSpec);
        }

        sb.Append('}').AppendLine();

        return StringBuilderPool.Return(sb);
    }
}

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

        // General
        GeneralEmitter.AddFileAndClassHeader(sb, enumGenerationSpec);
        GeneralEmitter.AddFieldsAndGetMethods(sb, enumGenerationSpec);
        GeneralEmitter.AddHasFlag(sb, enumGenerationSpec);
        GeneralEmitter.AddIsDefined(sb, enumGenerationSpec);

        AttributeDataEmitter.AddAttributeMethods(sb, enumGenerationSpec);

        // ToString
        ToStringEmitter.AddToString(sb, enumGenerationSpec);
        ToStringEmitter.AddToStringFormat(sb, enumGenerationSpec);

        // TryParse
        TryParseStringEmitter.AddTryParseString(sb, enumGenerationSpec);
        TryParseSpanEmitter.AddTryParseSpan(sb, enumGenerationSpec);

        // Private Helpers
        ToStringEmitter.AddFormatAsHexHelper(sb, enumGenerationSpec);
        ToStringEmitter.AddFormatFlagNames(sb, enumGenerationSpec);

        TryParseSpanEmitter.AddTryParsePrivate(sb, enumGenerationSpec);

        GeneralEmitter.AddPrivateHelperMethods(sb, enumGenerationSpec);

        sb.Append('}').AppendLine();

        return StringBuilderPool.Return(sb);
    }
}

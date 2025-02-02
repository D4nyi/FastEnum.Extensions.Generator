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
        GeneralEmitter
            .AddFileAndClassHeader(sb, enumGenerationSpec)
            .AddFieldsAndGetMethods(enumGenerationSpec)
            .AddIsDefined(enumGenerationSpec);

        AttributeDataEmitter.AddAttributeMethods(sb, enumGenerationSpec);

        // ToString
        ToStringEmitter
            .AddToString(sb, enumGenerationSpec)
            .AddToStringFormat(enumGenerationSpec);

        // TryParse
        TryParseStringEmitter.AddTryParseString(sb, enumGenerationSpec);
        TryParseSpanEmitter.AddTryParseSpan(sb, enumGenerationSpec);

        // Private Helpers
        ToStringEmitter
            .AddFormatAsHexHelper(sb, enumGenerationSpec)
            .AddFormatFlagNames(enumGenerationSpec);

        TryParseSpanEmitter.AddTryParsePrivate(sb, enumGenerationSpec);

        if (enumGenerationSpec.HasFlags)
        {
            GeneralEmitter.AddPrivateHelperMethods(sb, enumGenerationSpec);
        }

        sb
            .AddExceptionHelper()
            .AppendLine().Append('}');

        return StringBuilderPool.Return(sb);
    }
}

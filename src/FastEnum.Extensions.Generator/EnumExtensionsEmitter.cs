using System.Text;
using FastEnum.Extensions.Generator.Specs;
using FastEnum.Extensions.Generator.Utils;

using Microsoft.CodeAnalysis.Text;

using static FastEnum.Extensions.Generator.EnumExtensionsGenerator;

namespace FastEnum.Extensions.Generator;

internal sealed partial class EnumExtensionsEmitter
{
    private readonly EnumSourceGenerationContext _sourceGenerationContext;
    private readonly List<EnumGenerationSpec> _generationSpec;

    private EnumGenerationSpec _currentSpec;

    public EnumExtensionsEmitter(in EnumSourceGenerationContext sourceGenerationContext, List<EnumGenerationSpec> generationSpec)
    {
        _sourceGenerationContext = sourceGenerationContext;
        _generationSpec = generationSpec;
    }

    public void Emit()
    {
        foreach (EnumGenerationSpec enumGenerationSpec in _generationSpec)
        {
            _currentSpec = enumGenerationSpec;

            StringBuilder sb = StringBuilderPool.Get();

            AddFileAndClassHeader(sb);
            AddFieldsAndGetMethods(sb);
            AddHasFlag(sb);
            AddIsDefined(sb);
            AddToString(sb);
            AddToStringFormat(sb);
            AddAttributeMethods(sb);
            AddTryParseString(sb);
            AddTryParseSpan(sb);
            AddFormatAsHexHelper(sb);
            AddTryParsePrivate(sb);
            AddFormatFlagNames(sb);
            AddPrivateHelperMethods(sb);
            CloseClassAndNamespace(sb);

            string generatedClass = StringBuilderPool.Return(sb);

            // Add extension implementation.
            AddSource($"{_currentSpec.Name}Extensions.g.cs", generatedClass);
        }
    }

    private void AddSource(string fileName, string generatedClass)
    {
        _sourceGenerationContext.AddSource(fileName, SourceText.From(generatedClass, Encoding.UTF8));
    }

    private string Get(Indentation indentation)
    {
        if (_currentSpec.IsGlobalNamespace && indentation < Indentation.Method)
        {
            indentation--;
        }

        // 4 spaces per indentation.
        return indentation switch
        {
            Indentation.Method     => "    ",
            Indentation.MethodBody => "        ",
            Indentation.Nesting1   => "            ",
            Indentation.Nesting2   => "                ",
            Indentation.Nesting3   => "                    ",
            Indentation.Nesting4   => "                        ",
            _ => "", // Indentation.Namespace or Indentation.Class
        };
    }
}

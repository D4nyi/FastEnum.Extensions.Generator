using System.Collections;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;

using FastEnum.Extensions.Generator.Specs;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

internal static class GeneratorInternalsTestHelper
{
    private static readonly string[] _stages = typeof(Stages)
        .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
        .Select(x => x.Name)
        .ToArray();

    internal static void AssertRunsEqual(GeneratorDriverRunResult runResult1, GeneratorDriverRunResult runResult2)
    {
        // We're given all the tracking names, but not all that
        // stages will necessarily execute, so extract all the
        // output steps, and filter to ones we know about
        Dictionary<string, ImmutableArray<IncrementalGeneratorRunStep>> trackedSteps1 = GetTrackedSteps(runResult1);
        Dictionary<string, ImmutableArray<IncrementalGeneratorRunStep>> trackedSteps2 = GetTrackedSteps(runResult2);

        // Both runs should have the same tracked steps
        Assert.NotEmpty(trackedSteps1);
        Assert.Equal(trackedSteps1.Count, trackedSteps2.Count);
        Assert.Equal(trackedSteps1.Keys, trackedSteps2.Keys);

        // Get the IncrementalGeneratorRunStep collection for each run
        foreach (var (trackingName, runSteps1) in trackedSteps1)
        {
            // Assert that both runs produced the same outputs
            ImmutableArray<IncrementalGeneratorRunStep> runSteps2 = trackedSteps2[trackingName];
            AssertEqual(runSteps1, runSteps2);
        }

        return;

        // Local function that extracts the tracked steps
        static Dictionary<string, ImmutableArray<IncrementalGeneratorRunStep>> GetTrackedSteps(GeneratorDriverRunResult runResult)
            => runResult
                .Results[0] // We're only running a single generator, so this is safe
                .TrackedSteps // Get the pipeline outputs
                .Where(step => _stages.Contains(step.Key)) // filter to known steps
                .ToDictionary(x => x.Key, x => x.Value); // Convert to a dictionary
    }

    private static void AssertEqual(
        ImmutableArray<IncrementalGeneratorRunStep> runSteps1,
        ImmutableArray<IncrementalGeneratorRunStep> runSteps2)
    {
        Assert.Equal(runSteps1.Length, runSteps2.Length);

        for (int i = 0; i < runSteps1.Length; i++)
        {
            IncrementalGeneratorRunStep runStep1 = runSteps1[i];
            IncrementalGeneratorRunStep runStep2 = runSteps2[i];

            Assert.Single(runStep1.Outputs);
            Assert.Equal(runStep1.Outputs.Length, runStep2.Outputs.Length);

            // The outputs should be equal between different runs
            object output1 = runStep1.Outputs.Select(x => x.Value).FirstOrDefault()!;
            object output2 = runStep2.Outputs.Select(x => x.Value).FirstOrDefault()!;


            switch (output1)
            {
                case Diagnostic diagnostic1:
                    AssertDiagnostic(diagnostic1, output2 as Diagnostic);
                    break;
                case ImmutableArray<object> immutableArray1:
                    AssertImmutableArray(immutableArray1, (ImmutableArray<object>)output2);
                    break;
                default:
                    Assert.Equal(output1, output2);
                    break;
            }

            // Therefore, on the second run the results should always be cached or unchanged!
            // - Unchanged is when the _input_ has changed, but the output hasn't
            // - Cached is when the input has not changed, so the cached output is used
            // static x => Assert.Equal(IncrementalStepRunReason.Modified, x.Reason);
            Assert.All(runStep2.Outputs, static x =>
            {
                bool asd = x.Value is List<EnumBaseDataSpec>
                    ? x.Reason == IncrementalStepRunReason.Modified
                    : x.Reason is IncrementalStepRunReason.Cached or IncrementalStepRunReason.Unchanged;

                Assert.True(asd);
            });

            // Make sure we're not using anything we shouldn't
            AssertObjectGraph(runStep1);
        }
    }

    private static void AssertImmutableArray(ImmutableArray<object> immutableArray1, ImmutableArray<object> immutableArray2)
    {
        Assert.Equal(immutableArray1.Length, immutableArray2.Length);

        for (int index = 0; index < immutableArray1.Length; index++)
        {
            if (immutableArray1[index] is Diagnostic d1)
            {
                AssertDiagnostic(d1, immutableArray2[index] as Diagnostic);
            }
            else
            {
                Assert.Equal(immutableArray1[index], immutableArray2[index]);
            }
        }
    }

    private static void AssertDiagnostic(Diagnostic? diagnostic1, Diagnostic? diagnostic2)
    {
        Assert.NotNull(diagnostic1);
        Assert.NotNull(diagnostic2);

        Assert.Equal(diagnostic1.Descriptor, diagnostic2.Descriptor);
        Assert.Equal(diagnostic1.GetMessage(CultureInfo.InvariantCulture), diagnostic2.GetMessage(CultureInfo.InvariantCulture));
        Assert.Equal(diagnostic1.Location, diagnostic2.Location);
        Assert.Equal(diagnostic1.Severity, diagnostic2.Severity);
        Assert.Equal(diagnostic1.WarningLevel, diagnostic2.WarningLevel);
    }

    private static void AssertObjectGraph(IncrementalGeneratorRunStep runStep)
    {
        HashSet<object> visited = [];

        // Check all the outputs - probably overkill, but why not
        foreach (var (obj, _) in runStep.Outputs)
        {
            Visit(obj, visited);
        }
    }

    private static void Visit(object? node, HashSet<object> visited)
    {
        // If we've already seen this object, or it's null, stop.
        // We don't examine diagnostics
        if (node is null || node is Diagnostic || !visited.Add(node))
        {
            return;
        }

        // Make sure it's not a banned type
        Assert.IsNotAssignableFrom<Compilation>(node);
        Assert.IsNotAssignableFrom<ISymbol>(node);
        Assert.IsNotAssignableFrom<SyntaxNode>(node);

        // Examine the object
        Type type = node.GetType();
        if (type.IsPrimitive || type.IsEnum || type == typeof(string))
        {
            return;
        }

        // If the object is a collection, check each of the values
        if (node is IEnumerable collection and not string)
        {
            if (collection is ImmutableArray<string> { IsDefaultOrEmpty: true } or ImmutableArray<ImmutableArray<string>> { IsDefaultOrEmpty: true })
            {
                collection = Enumerable.Empty<string>();
            }

            foreach (object element in collection)
            {
                // recursively check each element in the collection
                Visit(element, visited);
            }

            return;
        }

        // Recursively check each field in the object
        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            object? fieldValue = field.GetValue(node);
            Visit(fieldValue, visited);
        }
    }
}

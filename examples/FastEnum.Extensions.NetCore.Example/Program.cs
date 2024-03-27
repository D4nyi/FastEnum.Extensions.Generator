using BenchmarkDotNet.Running;
using FastEnum.Extensions.NetCore.Example;

BenchmarkRunner.Run<Benchmarks>();

// Options options = Options.HasFlag;
//
// Console.WriteLine(options.ToString());
// Console.WriteLine("G: {0:G}", options);
// Console.WriteLine("X: {0:X}", options);
// Console.WriteLine("F: {0:F}", options);
// Console.WriteLine("D: {0:D}", options);
//
// Console.WriteLine(options.FastToString());
// Console.WriteLine("G: {0}", options.FastToString("G"));
// Console.WriteLine("X: {0}", options.FastToString("X"));
// Console.WriteLine("F: {0}", options.FastToString("F"));
// Console.WriteLine("D: {0}", options.FastToString("D"));

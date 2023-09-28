using BenchmarkDotNet.Running;

using ToStringExample;


BenchmarkRunner.Run<Benchmarks>();

const Options options = Options.ToString;

Console.WriteLine(options.ToString());
//Console.WriteLine("G: {0:G}", options);
//Console.WriteLine("X: {0:X}", options);
//Console.WriteLine("F: {0:F}", options);
//Console.WriteLine("D: {0:D}", options);

//Console.WriteLine(options.FastToString());
//Console.WriteLine("G: {0}", options.FastToString("G"));
//Console.WriteLine("X: {0}", options.FastToString("X"));
//Console.WriteLine("F: {0}", options.FastToString("F"));
//Console.WriteLine("D: {0}", options.FastToString("D"));

//Console.WriteLine(color);
//Console.WriteLine(color.FastToString());
//Console.WriteLine(NestingClass<int, List<int>>.NestedInClassEnum.None.FastToString());
//Console.WriteLine(EnumStringConverter.FastToString(NestingClass<int, List<int>>.NestedInClassEnum.None));
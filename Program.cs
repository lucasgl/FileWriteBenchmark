// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

//new FileWriterBenchmark().WriteToMemoryMappedFile();

var summary = BenchmarkRunner.Run<FileWriterBenchmark>();
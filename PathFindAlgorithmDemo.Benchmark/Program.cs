using BenchmarkDotNet.Running;

namespace PathFindAlgorithmDemo.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _=BenchmarkRunner.Run<PathFindAlgorithmBenchmark>();
        }
    }
}
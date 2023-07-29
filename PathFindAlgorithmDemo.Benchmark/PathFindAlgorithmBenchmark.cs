using BenchmarkDotNet.Attributes;
using PathFindAlgorithmDemo.HelpFullTools;
using System.Security.Cryptography;

namespace PathFindAlgorithmDemo.Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class PathFindAlgorithmBenchmark
    {
        public const string testMazeJSONPath = @"C:\Users\Ilia\Downloads\testMaze.json";

        public Maze maze;

        [GlobalSetup]
        public void Setup()
        {
            maze = Maze.LoadMazeJSON(testMazeJSONPath);
        }

        [Benchmark]
        public int[,] BreadthFirstSearchBenchmark()
        {
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solveMapBreadthFirstSearch = matrix.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            return solveMapBreadthFirstSearch;
        }

        [Benchmark]
        public int[,] DepthFirstSearchBenchmark()
        {
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solveMapDepthFirstSearch = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            return solveMapDepthFirstSearch;
        }

        [Benchmark]
        public int[,] GreedySearchBenchmark()
        {
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solveMapGreedySearch = matrix.GreedySearch(maze.StartPoint, maze.FinishPoint);
            return solveMapGreedySearch;
        }
    }

}
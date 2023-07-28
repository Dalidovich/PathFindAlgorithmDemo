using PathFindAlgorithmDemo.HelpFullStructures;
using PathFindAlgorithmDemo.NodePathFinderComponents;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace PathFindAlgorithmDemo
{
    public class Program
    {
        public static void testMatrixPathFinder(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("MatrixPathFinder");
            var maze = Maze.LoadMazeJSON(mazePath);

            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            sw.Start();
            var solveMapBreadthFirstSearch = matrix.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"BreadthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solveMapBreadthFirstSearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapBreadthFirstSearch.png");

            matrix.ClearMapEpoch();
            matrix.SetWalls(maze.Walls);
            sw.Start();
            var solveMapDepthFirstSearch = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            sw.Stop();
            Display.CreateSolveMazeImage(solveMapDepthFirstSearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapDepthFirstSearch.png");

            matrix.ClearMapEpoch();
            matrix.SetWalls(maze.Walls);
            sw.Start();
            var solveMapGreedySearch = matrix.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solveMapGreedySearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapGreedySearch.png");
        }

        public static void testGraphPathFinder(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("GraphPathFinder");
            var maze = Maze.LoadMazeJSON(mazePath);

            var graph = new Graph(maze.Width, maze.Height);
            graph.SetWalls(maze.Walls).Wait();
            sw.Start();
            var solveMapBreadthFirstSearch = graph.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"BreadthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphBreadthFirstSearch.png");

            graph.ClearNodeEpoch();
            sw.Start();
            var solveMapDepthFirstSearch = graph.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            sw.Stop();
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphDepthFirstSearch.png");

            graph.ClearNodeEpoch();
            sw.Start();
            var solveMapGreedySearch = graph.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphGreedySearch.png");
        }

        static async Task Main(string[] args)
        {
            testMatrixPathFinder(@"testMaze.json");
            testGraphPathFinder(@"testMaze.json");
        }
    }
}
using System.Diagnostics;

namespace PathFindAlgorithmDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(@"testMaze.json");

            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            sw.Start();
            var solveMapBreadthFirstSearch = matrix.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"BreadthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solveMapBreadthFirstSearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, "solveMapBreadthFirstSearch.png");

            matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            sw.Start();
            var solveMapDepthFirstSearch = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            sw.Stop();
            Display.CreateSolveMazeImage(solveMapDepthFirstSearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, "solveMapDepthFirstSearch.png");

            matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            sw.Start();
            var solveMapGreedySearch = matrix.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solveMapGreedySearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, "solveMapGreedySearch.png");
        }
    }
}
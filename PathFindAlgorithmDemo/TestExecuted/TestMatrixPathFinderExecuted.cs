using PathFindAlgorithmDemo.HelpFullTools;
using PathFindAlgorithmDemo.HelpFullTools.SideCheckers;
using PathFindAlgorithmDemo.NodePathFinderComponents;
using System.Diagnostics;

namespace PathFindAlgorithmDemo.TestExecuted
{
    public class TestMatrixPathFinderExecuted
    {
        public static void testMatrixPathFinderBreadthFirstSearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(mazePath);
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"BreadthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solve, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapBreadthFirstSearch.png");
        }

        public static void testMatrixPathFinderDepthFirstSearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(mazePath);
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solve, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapDepthFirstSearch.png");
        }

        public static void testMatrixPathFinderGreedySearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(mazePath);
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solve, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapGreedySearch.png");
        }

        public static void testMatrixPathFinderAllSearch(string mazePath, string? savePath = null)
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
            sw.Restart();
            var solveMapDepthFirstSearch = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            sw.Stop();
            Display.CreateSolveMazeImage(solveMapDepthFirstSearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapDepthFirstSearch.png");

            matrix.ClearMapEpoch();
            sw.Restart();
            var solveMapGreedySearch = matrix.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveMazeImage(solveMapGreedySearch, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveMapGreedySearch.png");
        }

        public static void testMatrixDepthFirstPathFinderQueueNeighbor(string mazePath, string? savePath = null)
        {
            string saveFolder = "testMatrixDepthFirstPathFinderQueueNeighbor";
            Sides[] sides = new Sides[]
            {
                Sides.bottom,
                Sides.up,
                Sides.right,
                Sides.left,
            };

            var sideCombinations = PermutationCombinations<Sides>.GenerateCombinations(sides.ToList(), 0, sides.Length - 1);
            Stopwatch sw = Stopwatch.StartNew();
            sw.Stop();
            var maze = Maze.LoadMazeJSON(mazePath);
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);

            Directory.CreateDirectory(saveFolder);

            SideCheckerForPathFinder.SideCheckerDelegateForPathFinder[] delegateCombinations = new SideCheckerForPathFinder.SideCheckerDelegateForPathFinder[sideCombinations.Count];

            for (int i = 0; i < sideCombinations.Count; i++)
            {
                delegateCombinations[i] = SideCheckerForPathFinder.sideCheckerDelegateDictionaryForPathFinder[sideCombinations[i][0]];
                delegateCombinations[i] += SideCheckerForPathFinder.sideCheckerDelegateDictionaryForPathFinder[sideCombinations[i][1]];
                delegateCombinations[i] += SideCheckerForPathFinder.sideCheckerDelegateDictionaryForPathFinder[sideCombinations[i][2]];
                delegateCombinations[i] += SideCheckerForPathFinder.sideCheckerDelegateDictionaryForPathFinder[sideCombinations[i][3]];
            }

            for (int i = 0; i < delegateCombinations.Length; i++)
            {
                matrix.ClearMapEpoch();
                sw.Restart();
                var solve = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint, delegateCombinations[i]);
                sw.Stop();
                var sideQ = string.Join(" ", sideCombinations[i].Select(x => x.ToString()[0]));
                Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds} , {sideQ}");
                Display.CreateSolveMazeImage(solve, matrix.WeightMap, maze.StartPoint, maze.FinishPoint, $"{savePath ?? saveFolder}\\DepthFirstSearch_{sideQ}.png");
            }
        }
    }

    public class TestGraphPathFinderExecuted
    {
        public static void testGraphPathFinderBreadthFirstSearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(mazePath);
            var graph = new Graph(maze.Width, maze.Height);
            graph.SetWalls(maze.Walls).Wait();
            sw.Start();
            var solve = graph.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"BreadthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphBreadthFirstSearch.png");
        }

        public static void testGraphPathFinderDepthFirstSearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(mazePath);
            var graph = new Graph(maze.Width, maze.Height);
            graph.SetWalls(maze.Walls).Wait();
            sw.Start();
            var solve = graph.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphDepthFirstSearch.png");
        }

        public static void testGraphPathFinderGreedySearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            var maze = Maze.LoadMazeJSON(mazePath);
            var graph = new Graph(maze.Width, maze.Height);
            graph.SetWalls(maze.Walls).Wait();
            sw.Start();
            var solve = graph.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphGreedySearch.png");
        }

        public static void testGraphPathFinderAllSearch(string mazePath, string? savePath = null)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("GraphPathFinder");
            var maze = Maze.LoadMazeJSON(mazePath);

            var graph = new Graph(maze.Width, maze.Height);
            graph.SetWalls(maze.Walls).Wait();
            sw.Start();
            var solveBreadthFirstSearch = graph.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"BreadthFirstSearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphBreadthFirstSearch.png");

            graph.ClearNodeEpoch();
            sw.Restart();
            var solveDepthFirstSearch = graph.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Console.WriteLine($"DepthFirstSearch time - {sw.ElapsedMilliseconds}");
            sw.Stop();
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphDepthFirstSearch.png");

            graph.ClearNodeEpoch();
            sw.Restart();
            var solveGreedySearch = graph.GreedySearch(maze.StartPoint, maze.FinishPoint);
            sw.Stop();
            Console.WriteLine($"GreedySearch time - {sw.ElapsedMilliseconds}");
            Display.CreateSolveGraphImage(graph.Nodes, maze.Width, maze.Height, maze.StartPoint, maze.FinishPoint, $"{savePath ?? ""}solveGraphGreedySearch.png");
        }
    }
}

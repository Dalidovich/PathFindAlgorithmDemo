using PathFindAlgorithmDemo.HelpFullTools;
using PathFindAlgorithmDemo.TestExecuted;

namespace PathFindAlgorithmDemo
{
    public class Program
    {
        delegate void MyDelegate();
        static void Main(string[] args)
        {
            //TestMatrixPathFinderExecuted.testMatrixPathFinderAllSearch(@"testMaze.json");
            //TestGraphPathFinderExecuted.testGraphPathFinderAllSearch(@"testMaze.json");
            //TestMatrixPathFinderExecuted.testMatrixDepthFirstPathFinderQueueNeighbor(@"testMaze.json");

            var maze = Maze.LoadMazeJSON(@"testMaze.json");
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Display.StartInSFML(solve, matrix.WeightMap);

            TestMazeGeneratorExecuted.testMazeGeneratorDepthFirstGenerate();
            TestMazeGeneratorExecuted.testMazeGeneratorBreadthFirstGenerate();
        }
    }
}
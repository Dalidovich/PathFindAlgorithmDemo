using PathFindAlgorithmDemo.HelpFullTools;
using PathFindAlgorithmDemo.TestPathFinders;

namespace PathFindAlgorithmDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            TestMatrixPathFinderExecuted.testMatrixPathFinderAllSearch(@"testMaze.json");
            TestGraphPathFinderExecuted.testGraphPathFinderAllSearch(@"testMaze.json");
            TestMatrixPathFinderExecuted.testMatrixDepthFirstPathFinderQueueNeighbor(@"testMaze.json");

            var maze = Maze.LoadMazeJSON(@"testMaze.json");
            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.GreedySearch(maze.StartPoint, maze.FinishPoint);
            Display.StartInSFML(solve, matrix.WeightMap);
        }
    }
}
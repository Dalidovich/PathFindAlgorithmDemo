using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.Json;

namespace PathFindAlgorithmDemo
{
    public class Program
    {
        static void Main(string[] args)
        {

            Stopwatch stopwatch = Stopwatch.StartNew();
            var matrix = new Matrix(TestMaze.height, TestMaze.width);
            var walls = TestMaze.GetTestMazeWalls();
            matrix.SetWalls(walls);

            var solveMap = matrix.BreadthFirstSearch(TestMaze.StartPoint, TestMaze.FinishPoint);
            stopwatch.Stop();
            Display.CreateSolveMazeImage(solveMap,matrix.WeightMap, TestMaze.StartPoint, TestMaze.FinishPoint, null);
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
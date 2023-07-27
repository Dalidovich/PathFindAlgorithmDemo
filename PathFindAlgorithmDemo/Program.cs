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
            var jsonString = string.Empty;
            var wallsPath = Directory.GetCurrentDirectory().Split('\\');
            var jsonPath = String.Join("\\", wallsPath.ToList().GetRange(0, wallsPath.Length - 3)) + @"\mazeWalls.txt";
            using (FileStream fstream = File.OpenRead(jsonPath))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                jsonString = Encoding.Default.GetString(buffer);
            }

            Point[]? walls = JsonSerializer.Deserialize<Point[]>(jsonString);

            var h = 656;
            var w = 448;
            var startPoint = new Point(1, 1);
            var finishPoint = new Point(w - 1, h - 1);

            var matrix = new Matrix(h, w);
            matrix.SetWalls(walls.ToArray());

            var solveMap = matrix.BreadthFirstSearch(startPoint, finishPoint);
            stopwatch.Stop();
            Display.CreateSolveMazeImage(solveMap,matrix.WeightMap, startPoint, finishPoint, null, walls.ToArray());
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
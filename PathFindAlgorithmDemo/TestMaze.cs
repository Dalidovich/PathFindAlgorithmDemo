using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;

namespace PathFindAlgorithmDemo
{
    public static class TestMaze
    {
        public static int height = 656;
        public static int width = 448;

        public static Point StartPoint = new Point(1, 1);
        public static Point FinishPoint = new Point(width - 1, height - 1);

        public static Point[] GetTestMazeWalls()
        {
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

            return walls;
        }
    }
}
using System.Text;
using System.Text.Json;
using PathFindAlgorithmDemo.HelpFullStructures;

namespace PathFindAlgorithmDemo.HelpFullTools
{
    public class Maze
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Point StartPoint { get; set; }
        public Point FinishPoint { get; set; }
        public Point[] Walls { get; set; }

        public Maze(int height, int width, Point startPoint, Point finishPoint, Point[] walls)
        {
            Height = height;
            Width = width;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            Walls = walls;
        }

        public Maze()
        {
        }

        public static Maze LoadMazeJSON(string path)
        {
            var jsonString = string.Empty;
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                jsonString = Encoding.Default.GetString(buffer);
            }

            Maze? maze = JsonSerializer.Deserialize<Maze>(jsonString);

            return maze ?? new Maze();
        }

        public string SaveMazeJSON(string path)
        {
            var jsonString = JsonSerializer.Serialize(this);
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(jsonString);
                fstream.Write(buffer, 0, buffer.Length);
            }

            return jsonString;
        }
    }
}
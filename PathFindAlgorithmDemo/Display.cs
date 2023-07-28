using System.Drawing;
using Point = System.Drawing.Point;

namespace PathFindAlgorithmDemo
{
    public static class Display
    {
        public static void Matrix(int [,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    Console.Write($"{matrix[i, k]}\t");
                }
                Console.WriteLine();
            }
        }

        public static IEnumerable<Point> GetWallsPointFromImage(string path) 
        {
            var walls = new List<Point>();
            var image = new Bitmap(path);

            for (int i = 0; i < image.Height; i++)
            {
                for (int k = 0; k < image.Width; k++)
                {
                    if (image.GetPixel(k,i).GetBrightness() < 0.3)
                    {
                        walls.Add(new Point(i, k));
                    }
                }
            }

            return walls;
        }

        public static void CreateSolveMazeImage(int[,] map, int[,] weightMap, Point start, Point finish, string? savePath=null)
        {
            var image = new Bitmap(map.GetLength(1), map.GetLength(0));

            for (int i = 0; i < image.Height; i++)
            {
                for (int k = 0; k < image.Width; k++)
                {
                    if (map[i,k] == 0)
                    {
                        image.SetPixel(k, i, Color.White);
                    }

                    if(map[i, k] !=0)
                    {
                        image.SetPixel(k, i, Color.Blue);
                    }

                    if (weightMap[i, k] != 0 && map[i, k] == 0)
                    {
                        image.SetPixel(k, i, Color.Yellow);
                    }

                    if (weightMap[i, k] == -1)
                    {
                        image.SetPixel(k, i, Color.Black);
                    }
                }
            }

            image.SetPixel(start.X, start.Y, Color.Green);
            image.SetPixel(finish.X, finish.Y, Color.DarkRed);            

            image.Save(savePath??"solveMaze.png");
        }
    }
}
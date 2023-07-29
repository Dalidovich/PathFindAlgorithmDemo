using PathFindAlgorithmDemo.Consts;
using PathFindAlgorithmDemo.NodePathFinderComponents;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Drawing;
using Color = System.Drawing.Color;
using Point = PathFindAlgorithmDemo.HelpFullStructures.Point;

namespace PathFindAlgorithmDemo.HelpFullTools
{
    public static class Display
    {
        public static void Matrix(int[,] matrix)
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

        public static void Graph(Node[] nodes, int width, int height)
        {

            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    var node = nodes[i * width + k];
                    if (node.ForSolve)
                    {
                        Console.Write("-\t");
                    }
                    else
                    {
                        Console.Write($"{node.EpochValue}\t");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static IEnumerable<Point> GetWallsPointFromImage(string path)
        {
            var walls = new List<Point>();
            var image = new Bitmap(path);

            for (int i = 0; i < image.Height; i++)
            {
                for (int k = 0; k < image.Width; k++)
                {
                    if (image.GetPixel(k, i).GetBrightness() < DisplayConst.WallMaxBrightness)
                    {
                        walls.Add(new Point(k, i));
                    }
                }
            }

            return walls;
        }

        public static void CreateSolveMazeImage(Point[] map, int[,] weightMap, Point start, Point finish, string? savePath = null)
        {
            var image = new Bitmap(weightMap.GetLength(1), weightMap.GetLength(0));

            for (int i = 0; i < image.Height; i++)
            {
                for (int k = 0; k < image.Width; k++)
                {
                    if (weightMap[i, k] == MazeDesignationsConsts.notVisited)
                    {
                        image.SetPixel(k, i, Color.White);
                    }

                    if (weightMap[i, k] != MazeDesignationsConsts.notVisited)
                    {
                        image.SetPixel(k, i, Color.Yellow);
                    }

                    if (weightMap[i, k] == MazeDesignationsConsts.wall)
                    {
                        image.SetPixel(k, i, Color.Black);
                    }
                }
            }

            map.ToList().ForEach(x => 
            {
                image.SetPixel(x.X, x.Y, Color.Blue);
            });

            image.SetPixel(start.X, start.Y, Color.Green);
            image.SetPixel(finish.X, finish.Y, Color.DarkRed);

            image.Save(savePath ?? "solveMaze.png");
        }

        public static void CreateSolveGraphImage(Node[] nodes, int width, int height, Point start, Point finish, string? savePath = null)
        {
            var image = new Bitmap(width, height);

            for (int i = 0; i < image.Height; i++)
            {
                for (int k = 0; k < image.Width; k++)
                {
                    var coordinate = i * width + k;
                    if (nodes[coordinate].EpochValue == MazeDesignationsConsts.notVisited)
                    {
                        image.SetPixel(k, i, Color.White);
                    }

                    if (nodes[coordinate].EpochValue != MazeDesignationsConsts.notVisited)
                    {
                        image.SetPixel(k, i, Color.Yellow);
                    }

                    if (nodes[coordinate].ForSolve)
                    {
                        image.SetPixel(k, i, Color.Blue);
                    }


                    if (nodes[coordinate].isWall())
                    {
                        image.SetPixel(k, i, Color.Black);
                    }
                }
            }

            image.SetPixel(start.X, start.Y, Color.Green);
            image.SetPixel(finish.X, finish.Y, Color.DarkRed);

            image.Save(savePath ?? "solveMaze.png");
        }

        public static void StartInSFML(Point[] solveWay, int[,] map)
        {
            RenderWindow window = new RenderWindow(new VideoMode((uint)map.GetLength(1), (uint)map.GetLength(0)), "matrix path finder");
            window.SetVerticalSyncEnabled(true);

            List<Vertex> walls = new List<Vertex>();
            List<Vertex> visited = new List<Vertex>();
            List<Vertex> solveMap = new List<Vertex>();
            var solveEpoch = solveWay.Count();
            solveWay.ToList().ForEach(x => solveMap.Add(new Vertex(new Vector2f(x.X, x.Y), SFML.Graphics.Color.Blue)));

            var maxEpoch = 0;
            var epoch = 1;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int k = 0; k < map.GetLength(1); k++)
                {
                    if (maxEpoch < map[i, k])
                    {
                        maxEpoch = map[i, k];
                    }
                    if (map[i, k] == -1)
                    {
                        walls.Add(new Vertex(new Vector2f(k, i), SFML.Graphics.Color.Magenta));
                    }
                }
            }

            window.Closed += (object? sender, EventArgs e) => window.Close();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                var vanguard = new List<Vertex>();

                if (epoch != maxEpoch)
                {
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int k = 0; k < map.GetLength(1); k++)
                        {
                            if (map[i, k] == epoch)
                            {
                                visited.Add(new Vertex(new Vector2f(k, i), SFML.Graphics.Color.Yellow));
                                vanguard.Add(new Vertex(new Vector2f(k, i), SFML.Graphics.Color.Red));
                            }
                        }
                    }

                    epoch++;
                }
                else if (solveEpoch != 0)
                {
                    solveEpoch--;
                }

                window.Draw(walls.ToArray(), PrimitiveType.Points);
                window.Draw(visited.ToArray(), PrimitiveType.Points);
                window.Draw(vanguard.ToArray(), PrimitiveType.Points);
                window.Draw(solveMap.GetRange(solveEpoch, solveMap.Count - solveEpoch).ToArray(), PrimitiveType.Points);
                window.Display();
            }
        }
    }
}
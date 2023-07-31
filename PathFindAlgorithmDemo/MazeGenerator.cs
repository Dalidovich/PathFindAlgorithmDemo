using PathFindAlgorithmDemo.HelpFullTools.SideCheckers;
using Point = PathFindAlgorithmDemo.HelpFullStructures.Point;

namespace PathFindAlgorithmDemo
{
    public class MazeGenerator
    {
        public const bool wall = false;

        private readonly int _width;
        private readonly int _height;

        private static Random _random = new Random();

        public MazeGenerator(int width, int height, double clearProcent = 10)
        {
            _width = width;
            _height = height;
        }

        public bool[,] BreadthFirstGenerate()
        {
            var maze = new bool[_height, _width];

            int x = _random.Next(0, _width);
            int y = _random.Next(0, _height);

            List<Point> pointToVisit = new List<Point>
            {
                new Point(x, y)
            };
            maze[y, x] = !wall;

            while (pointToVisit.Count > 0)
            {
                var nextPointToVisit = new List<Point>();
                foreach (var item in pointToVisit)
                {
                    List<Point> n = new List<Point>();
                    SideCheckerForMazeGenerator.defaultSideCheckerDelegateForMazeGenerator(item, _width, _height, n, wall, maze);
                    n.ToList().ForEach(x => maze[x.Y, x.X] = !wall);
                    nextPointToVisit.AddRange(n);
                }

                foreach (var item in nextPointToVisit)
                {
                    List<Point> n = new List<Point>();
                    SideCheckerForMazeGenerator.defaultSideCheckerDelegateForMazeGenerator(item, _width, _height, n, !wall, maze);
                    var startPointToConnect = n.ToArray()[_random.Next(0, n.Count())];
                    maze = _connectPoint(item, startPointToConnect, maze);
                }
                pointToVisit = nextPointToVisit;
            }

            return maze;
        }

        public bool[,] DepthFirstGenerate()
        {
            var maze = new bool[_height, _width];

            int x = _random.Next(0, _width);
            int y = _random.Next(0, _height);

            var sideChecker=SideCheckerForMazeGenerator.GetShuffledDefaultSideCheckerDelegateForMazeGenerator();
             
            List<Point> pointToVisit = new List<Point>
            {
                new Point(x, y)
            };
            maze[y, x] = !wall;

            while (pointToVisit.Count > 0)
            {
                List<Point> nextPointToVisit = new List<Point>();
                sideChecker(pointToVisit.First(), _width, _height, nextPointToVisit, wall, maze);

                nextPointToVisit.ToList().ForEach(x => maze[x.Y, x.X] = !wall);

                foreach (var item in nextPointToVisit)
                {
                    List<Point> n = new List<Point>();
                    sideChecker(item, _width, _height, n, !wall, maze);
                    var startPointToConnect = n.ToArray()[_random.Next(0, n.Count())];
                    maze = _connectPoint(item, startPointToConnect, maze);
                }
                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, nextPointToVisit);
            }

            return maze;
        }

        private bool[,] _connectPoint(Point point, Point startPointToConnect, bool[,] maze)
        {
            if (point.X == startPointToConnect.X)
            {
                maze[startPointToConnect.Y > point.Y ? point.Y + 1 : point.Y - 1, point.X] = !wall;
            }
            else if (point.Y == startPointToConnect.Y)
            {
                maze[point.Y, startPointToConnect.X > point.X ? point.X + 1 : point.X - 1] = !wall;
            }

            return maze;
        }

        public Point[] GetWallsOfMaze(bool[,] maze)
        {
            var walls=new List<Point>();
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int k = 0; k < maze.GetLength(1); k++)
                {
                    if (maze[i,k] == wall)
                    {
                        walls.Add(new(k, i));
                    }
                }
            }

            return walls.ToArray();
        }
    }
}
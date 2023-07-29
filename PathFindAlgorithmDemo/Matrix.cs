using PathFindAlgorithmDemo.Consts;
using PathFindAlgorithmDemo.HelpFullStructures;
using PathFindAlgorithmDemo.HelpFullTools;

namespace PathFindAlgorithmDemo
{
    public class Matrix
    {
        public int[,] WeightMap { get; set; }

        private readonly int _width;
        private readonly int _height;

        public Matrix(int height, int width)
        {
            WeightMap = new int[height, width];

            _width = width;
            _height = height;
        }

        public void ClearMapEpoch()
        {
            for (int i = 0; i < WeightMap.GetLength(0); i++)
            {
                for (int k = 0; k < WeightMap.GetLength(1); k++)
                {
                    if (WeightMap[i, k] != MazeDesignationsConsts.wall)
                    {
                        WeightMap[i, k] = MazeDesignationsConsts.notVisited;
                    }
                }
            }
        }

        public void SetWalls(params Point[] walls) => walls.ToList().ForEach(wall => WeightMap[wall.Y, wall.X] = MazeDesignationsConsts.wall);

        public int[,] BreadthFirstSearch(Point startCoordinate, Point finishCoordinate)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = WeightMap.Length;
            var pointToVisit = GetNeighborsPoints(startCoordinate, MazeDesignationsConsts.notVisited, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);

            while (true)
            {
                var nextPointToVisit = new List<Point>();
                foreach (var item in pointToVisit)
                {
                    var n = GetNeighborsPoints(item, MazeDesignationsConsts.notVisited, WeightMap);
                    if (n.Contains(finishCoordinate))
                    {

                        return FillFinishMap(epoch + 1, WeightMap, finishCoordinate);
                    }
                    n.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch + 1);
                    nextPointToVisit.AddRange(n);
                }
                pointToVisit = nextPointToVisit;
                epoch++;
            }
        }

        public int[,] DepthFirstSearch(Point startCoordinate, Point finishCoordinate, SideChecker.SideCheckerDelegate? sideCheckerQueue = null)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = WeightMap.Length;
            var pointToVisit = GetNeighborsPoints(startCoordinate, MazeDesignationsConsts.notVisited, WeightMap, sideCheckerQueue).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);

            while (true)
            {
                var n = GetNeighborsPoints(pointToVisit.First(), MazeDesignationsConsts.notVisited, WeightMap, sideCheckerQueue).ToList();
                if (n.Contains(finishCoordinate))
                {

                    return FillFinishMap(epoch + 1, WeightMap, finishCoordinate);
                }
                n.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch + 1);
                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, n);
                epoch++;
            }
        }

        public int[,] GreedySearch(Point startCoordinate, Point finishCoordinate)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = WeightMap.Length;
            var pointToVisit = GetNeighborsPoints(startCoordinate, MazeDesignationsConsts.notVisited, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);
            pointToVisit.Sort(startCoordinate, finishCoordinate);

            while (true)
            {
                var n = GetNeighborsPoints(pointToVisit.First(), MazeDesignationsConsts.notVisited, WeightMap).ToList();
                if (n.Contains(finishCoordinate))
                {

                    return FillFinishMap(epoch + 1, WeightMap, finishCoordinate);
                }
                n.ForEach(x => WeightMap[x.Y, x.X] = epoch + 1);
                n.Sort(pointToVisit.First(), finishCoordinate);

                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, n);
                epoch++;
            }
        }

        private int[,] FillFinishMap(int start, int[,] weightMap, Point finishPoint)
        {
            var wayMap = new int[_height, _width];
            wayMap[finishPoint.Y, finishPoint.X] = start--;
            while (start != MazeDesignationsConsts.notVisited)
            {
                var fitPoint = GetNeighborsPoints(finishPoint, start, weightMap);
                if (fitPoint.Count() == 0)
                {
                    start--;
                }
                else
                {
                    wayMap[fitPoint.First().Y, fitPoint.First().X] = start--;
                    finishPoint = fitPoint.First();
                }
            }

            return wayMap;
        }

        private IEnumerable<Point> GetNeighborsPoints(Point selected, int fitValue, int[,] matrix, SideChecker.SideCheckerDelegate? sideCheckerQueue = null)
        {
            var neighbors = new List<Point>();

            if (sideCheckerQueue != null)
            {
                sideCheckerQueue(selected, _width, _height, neighbors, fitValue, matrix);
            }
            else
            {
                SideChecker.defaultSideCheckerDelegate(selected, _width, _height, neighbors, fitValue, matrix);
            }

            return neighbors;
        }
    }
}
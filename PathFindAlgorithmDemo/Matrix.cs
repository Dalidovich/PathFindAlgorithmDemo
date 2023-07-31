using PathFindAlgorithmDemo.Consts;
using PathFindAlgorithmDemo.HelpFullStructures;
using PathFindAlgorithmDemo.HelpFullTools;
using PathFindAlgorithmDemo.HelpFullTools.SideCheckers;

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

        public Point[] BreadthFirstSearch(Point startCoordinate, Point finishCoordinate)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = MazeDesignationsConsts.start;
            var pointToVisit = _getNeighborsPoints(startCoordinate, MazeDesignationsConsts.notVisited, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);

            while (true)
            {
                var nextPointToVisit = new List<Point>();
                foreach (var item in pointToVisit)
                {
                    var n = _getNeighborsPoints(item, MazeDesignationsConsts.notVisited, WeightMap);
                    if (n.Contains(finishCoordinate))
                    {

                        return _fillFinishMap(epoch + 1, WeightMap, finishCoordinate);
                    }
                    n.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch + 1);
                    nextPointToVisit.AddRange(n);
                }
                pointToVisit = nextPointToVisit;
                epoch++;
            }
        }

        public Point[] DepthFirstSearch(Point startCoordinate, Point finishCoordinate, SideCheckerForPathFinder.SideCheckerDelegateForPathFinder? sideCheckerQueue = null)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = MazeDesignationsConsts.start;
            var pointToVisit = _getNeighborsPoints(startCoordinate, MazeDesignationsConsts.notVisited, WeightMap, sideCheckerQueue).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);

            while (true)
            {
                var n = _getNeighborsPoints(pointToVisit.First(), MazeDesignationsConsts.notVisited, WeightMap, sideCheckerQueue).ToList();
                if (n.Contains(finishCoordinate))
                {

                    return _fillFinishMap(epoch + 1, WeightMap, finishCoordinate);
                }
                if (n.Count != 0)
                {

                    n.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch + 1);
                    epoch++;
                }
                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, n);
            }
        }

        public Point[] GreedySearch(Point startCoordinate, Point finishCoordinate)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = MazeDesignationsConsts.start;
            var pointToVisit = _getNeighborsPoints(startCoordinate, MazeDesignationsConsts.notVisited, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);
            pointToVisit.Sort(startCoordinate, finishCoordinate);
            while (true)
            {
                var n = _getNeighborsPoints(pointToVisit.First(), MazeDesignationsConsts.notVisited, WeightMap).ToList();
                if (n.Contains(finishCoordinate))
                {

                    return _fillFinishMap(epoch + 1, WeightMap, finishCoordinate);
                }
                if (n.Count != 0)
                {

                    n.ForEach(x => WeightMap[x.Y, x.X] = epoch + 1);
                    n.Sort(pointToVisit.First(), finishCoordinate);
                    epoch++;
                }

                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, n);
            }
        }

        private Point[]  _fillFinishMap(int start, int[,] weightMap, Point finishPoint)
        {
            var wayMap = new List<Point> 
            {
                finishPoint 
            };
            while (start != MazeDesignationsConsts.notVisited)
            {
                var fitPoint = _getNeighborsPoints(finishPoint, start, weightMap);
                if (fitPoint.Count() == 0)
                {
                    start--;
                }
                else
                {
                    wayMap.Add(fitPoint.First());
                    finishPoint = fitPoint.First();
                }
            }

            return wayMap.ToArray();
        }

        private IEnumerable<Point> _getNeighborsPoints(Point selected, int fitValue, int[,] matrix, SideCheckerForPathFinder.SideCheckerDelegateForPathFinder? sideCheckerQueue = null)
        {
            var neighbors = new List<Point>();

            if (sideCheckerQueue != null)
            {
                sideCheckerQueue(selected, _width, _height, neighbors, fitValue, matrix);
            }
            else
            {
                SideCheckerForPathFinder.defaultSideCheckerDelegateForPathFinder(selected, _width, _height, neighbors, fitValue, matrix);
            }

            return neighbors;
        }
    }
}
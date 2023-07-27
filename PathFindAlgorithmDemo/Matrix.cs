using System.Drawing;

namespace PathFindAlgorithmDemo
{
    public class Matrix
    {
        public int[,] WeightMap { get; set; }

        private readonly int _width;
        private readonly int _height;

        public Matrix(int height,int width)
        {
            WeightMap = new int[height, width];

            _width = width;
            _height = height;
        }

        public void SetWalls(params Point[] walls) => walls.ToList().ForEach(wall => WeightMap[wall.X, wall.Y] = -1);

        public int[,] BreadthFirstSearch(Point startCoordinate, Point finishCoordinate)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = WeightMap.Length;
            var pointToVisit = GetNeighborsPoints(startCoordinate, 0, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);

            while (true)
            {
                var nextPointToVisit = new List<Point>();
                foreach (var item in pointToVisit)
                {
                    var n = GetNeighborsPoints(item, 0, WeightMap);
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

        public int[,] DepthFirstSearch(Point startCoordinate, Point finishCoordinate)
        {
            WeightMap[startCoordinate.Y, startCoordinate.X] = WeightMap.Length;
            var pointToVisit = GetNeighborsPoints(startCoordinate, 0, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);

            while (true)
            {
                var n = GetNeighborsPoints(pointToVisit.First(), 0, WeightMap);
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
            var pointToVisit = GetNeighborsPoints(startCoordinate, 0, WeightMap).ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x => WeightMap[x.Y, x.X] = epoch);
            pointToVisit.Sort(startCoordinate,finishCoordinate);

            while (true)
            {
                var n = GetNeighborsPoints(pointToVisit.First(), 0, WeightMap).ToList();
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
            while (start != 0)
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

        private IEnumerable<Point> GetNeighborsPoints(Point selected, int fitValue, int[,] matrix)
        {
            var neighbors = new List<Point>();
            if (selected.X != _width - 1 && matrix[selected.Y, selected.X + 1] == fitValue)
                neighbors.Add(selected with { X = selected.X + 1 });

            if (selected.Y != _height - 1 && matrix[selected.Y + 1, selected.X] == fitValue)
                neighbors.Add(selected with { Y = selected.Y + 1 });

            if (selected.X != 0 && matrix[selected.Y, selected.X - 1] == fitValue)
                neighbors.Add(selected with { X = selected.X - 1 });

            if (selected.Y != 0 && matrix[selected.Y - 1, selected.X] == fitValue)
                neighbors.Add(selected with { Y = selected.Y - 1 });

            return neighbors;
        }
    }
}
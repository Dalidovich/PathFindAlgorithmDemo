using PathFindAlgorithmDemo.HelpFullStructures;

namespace PathFindAlgorithmDemo.NodePathFinderComponents
{
    public class Graph
    {
        private readonly int _width;
        private readonly int _height;

        public Node[] Nodes { get; private set; }

        public Graph(int width, int height)
        {
            _width = width;
            _height = height;

            Nodes = new Node[width * height];
            for (int i = 0; i < _height; i++)
            {
                for (int k = 0; k < _width; k++)
                {
                    Nodes[i * _width + k] = new Node(k, i);
                }
            }
        }

        public void ClearNodeEpoch()
        {
            foreach (Node node in Nodes)
            {
                node.EpochValue = node.EpochValue == -1 ? -1 : 0;
                node.ForSolve = false;
                node.NodeFromStart = null;
            }
        }

        public async Task SetWalls(params Point[] walls)
        {
            walls.ToList().ForEach(wall => Nodes[wall.Y * _width + wall.X].EpochValue = -1);

            Task[] tasks = new Task[_height];

            for (int i = 0; i < _height; i++)
            {
                var ti = i;
                tasks[ti] = Task.Factory.StartNew(() =>
                {
                    for (int k = 0; k < _width; k++)
                    {
                        var neighbors = new List<Node>();
                        var coordinate = ti * _width + k;
                        if (walls.Contains(new Point(k, i)))
                            continue;

                        if (ti + 1 != _height && !Nodes[coordinate + _width].isWall())
                        {
                            neighbors.Add(Nodes[coordinate + _width]);
                        }
                        if (ti != 0 && !Nodes[coordinate - _width].isWall())
                        {
                            neighbors.Add(Nodes[coordinate - _width]);
                        }

                        if (k + 1 != _width && !Nodes[coordinate + 1].isWall())
                        {
                            neighbors.Add(Nodes[coordinate + 1]);
                        }
                        if (k != 0 && !Nodes[coordinate - 1].isWall())
                        {
                            neighbors.Add(Nodes[coordinate - 1]);
                        }

                        Nodes[coordinate].Neighbors = neighbors.ToArray();
                    }
                });
            }

            await Task.WhenAll(tasks);
        }

        public Node[] BreadthFirstSearch(Point startCoordinate, Point finishCoordinate)
        {
            Nodes[startCoordinate.Y * _width + startCoordinate.X].EpochValue = Nodes.Length;
            var pointToVisit = Nodes[startCoordinate.Y * _width + startCoordinate.X].Neighbors;
            var epoch = 1;
            pointToVisit.ToList().ForEach(x =>
            {
                x.EpochValue = epoch;
                x.NodeFromStart = Nodes[startCoordinate.Y * _width + startCoordinate.X];
            });
            epoch++;

            while (true)
            {
                var nextPointToVisit = new List<Node>();
                foreach (var item in pointToVisit)
                {
                    var n = item.Neighbors.Where(x => x.EpochValue == 0).ToList();
                    if (n.Where(x => x.X == finishCoordinate.X && x.Y == finishCoordinate.Y).Count() != 0)
                    {
                        Nodes[finishCoordinate.Y * _width + finishCoordinate.X].NodeFromStart = item;
                        return GetSolveNodeGraph(finishCoordinate, Nodes, _width);
                    }
                    n.ToList().ForEach(x =>
                    {
                        x.EpochValue = epoch;
                        x.NodeFromStart = item;
                    });
                    nextPointToVisit.AddRange(n);
                }
                pointToVisit = nextPointToVisit.ToArray();
                epoch++;
            }
        }

        public Node[] DepthFirstSearch(Point startCoordinate, Point finishCoordinate)
        {
            Nodes[startCoordinate.Y * _width + startCoordinate.X].EpochValue = Nodes.Length;
            var pointToVisit = Nodes[startCoordinate.Y * _width + startCoordinate.X].Neighbors.ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x =>
            {
                x.EpochValue = epoch;
                x.NodeFromStart = Nodes[startCoordinate.Y * _width + startCoordinate.X];
            });

            while (true)
            {
                var n = pointToVisit.First().Neighbors.Where(x => x.EpochValue == 0).ToList();
                if (n.Where(x => x.X == finishCoordinate.X && x.Y == finishCoordinate.Y).Count() != 0)
                {
                    Nodes[finishCoordinate.Y * _width + finishCoordinate.X].NodeFromStart = pointToVisit.First();
                    return GetSolveNodeGraph(finishCoordinate, Nodes, _width);
                }
                n.ForEach(x =>
                {
                    x.EpochValue = epoch;
                    x.NodeFromStart = pointToVisit.First();
                });
                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, n);
                epoch++;
            }
        }

        public Node[] GreedySearch(Point startCoordinate, Point finishCoordinate)
        {
            Nodes[startCoordinate.Y * _width + startCoordinate.X].EpochValue = Nodes.Length;
            var pointToVisit = Nodes[startCoordinate.Y * _width + startCoordinate.X].Neighbors.ToList();
            var epoch = 1;
            pointToVisit.ToList().ForEach(x =>
            {
                x.EpochValue = epoch;
                x.NodeFromStart = Nodes[startCoordinate.Y * _width + startCoordinate.X];
            });
            pointToVisit.Sort(startCoordinate, finishCoordinate);

            while (true)
            {
                var n = pointToVisit.First().Neighbors.Where(x => x.EpochValue == 0).ToList();
                if (n.Where(x => x.X == finishCoordinate.X && x.Y == finishCoordinate.Y).Count() != 0)
                {
                    Nodes[finishCoordinate.Y * _width + finishCoordinate.X].NodeFromStart = pointToVisit.First();
                    return GetSolveNodeGraph(finishCoordinate, Nodes, _width);
                }
                n.ForEach(x =>
                {
                    x.EpochValue = epoch;
                    x.NodeFromStart = pointToVisit.First();
                });
                n.Sort(new Point(pointToVisit.First().X, pointToVisit.First().Y), finishCoordinate);

                pointToVisit.RemoveAt(0);
                pointToVisit.InsertRange(0, n);
                epoch++;
            }
        }

        private Node[] GetSolveNodeGraph(Point finishCoordinate, Node[] nodes, int width)
        {
            var soleveNodeWay = new List<Node>();
            var finishNode = nodes[finishCoordinate.Y * width + finishCoordinate.X];
            while (finishNode.NodeFromStart != null)
            {
                finishNode.ForSolve = true;
                soleveNodeWay.Add(finishNode);
                finishNode = finishNode.NodeFromStart;
            }

            return soleveNodeWay.ToArray();
        }
    }
}

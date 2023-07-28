namespace PathFindAlgorithmDemo.NodePathFinderComponents
{
    public class Node
    {
        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int EpochValue { get; set; }
        public Node[] Neighbors { get; set; } = new Node[0];
        public Node NodeFromStart { get; set; }
        public bool ForSolve { get; set; }

        public bool isWall() => EpochValue == -1;
    }
}

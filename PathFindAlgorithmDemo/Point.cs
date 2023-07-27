namespace PathFindAlgorithmDemo
{
    public struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
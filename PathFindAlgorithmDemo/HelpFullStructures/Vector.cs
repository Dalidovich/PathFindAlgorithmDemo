namespace PathFindAlgorithmDemo.HelpFullStructures
{
    public struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public float Dot(Vector value2)
        {
            return X * value2.X + Y * value2.Y;
        }
        public float Length()
        {
            return MathF.Sqrt(Dot(this));
        }
    }
}
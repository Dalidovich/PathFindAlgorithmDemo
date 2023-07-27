using System.Numerics;

namespace PathFindAlgorithmDemo
{
    public static class SortPointExtension
    {
        public static void Sort(this List<Point> points, Point start, Point finish)
        {
            points.Sort((x, y) =>
            {
                var vx = new Vector2(x.X - start.X, x.Y - start.Y);
                var vy = new Vector2(y.X - start.X, y.Y - start.Y);
                var vf = new Vector2(finish.X - start.X, finish.Y - start.Y);

                return Vector2.Dot(vx, vf) / (vx.Length() * vf.Length()) < Vector2.Dot(vy, vf) / (vy.Length() * vf.Length()) ? 1 : 0;
            });
        }
    }
}
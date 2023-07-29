using PathFindAlgorithmDemo.HelpFullStructures;
using PathFindAlgorithmDemo.NodePathFinderComponents;

namespace PathFindAlgorithmDemo.HelpFullTools
{
    public static class SortExtension
    {
        public static void Sort(this List<Point> points, Point start, Point finish)
        {
            points.Sort((x, y) =>
            {
                var vx = new Vector(x.X - start.X, x.Y - start.Y);
                var vy = new Vector(y.X - start.X, y.Y - start.Y);
                var vf = new Vector(finish.X - start.X, finish.Y - start.Y);

                return vx.Dot(vf) / (vx.Length() * vf.Length()) < vy.Dot(vf) / (vy.Length() * vf.Length()) ? 1 : 0;
            });
        }

        public static void Sort(this List<Node> points, Point start, Point finish)
        {
            points.Sort((x, y) =>
            {
                var vx = new Vector(x.X - start.X, x.Y - start.Y);
                var vy = new Vector(y.X - start.X, y.Y - start.Y);
                var vf = new Vector(finish.X - start.X, finish.Y - start.Y);

                return vx.Dot(vf) / (vx.Length() * vf.Length()) < vy.Dot(vf) / (vy.Length() * vf.Length()) ? 1 : 0;
            });
        }
    }
}
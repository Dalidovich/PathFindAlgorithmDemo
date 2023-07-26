using System.Diagnostics;
using System.Drawing;

namespace PathFindAlgorithmDemo
{

    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var m = new Matrix(new Size(5, 5));
            var walls = new List<Point>
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(1, 2),
                new Point(1, 3),

                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3),
            };
            m.SetWalls(walls.ToArray());
            Display<int>.Matrix(m.WeightMap);
            Console.WriteLine("__________________________");
            //var wayMap = m.BreadthFirstSearch(new Point(0, 2), new Point(2, 0));//new Point(4, 3));
            //var wayMap = m.DepthFirstSearch(new Point(0, 2), new Point(4, 3));
            var wayMap = m.GreedySearch(new Point(0, 2), new Point(2, 0));//new Point(4, 3));
            Display<int>.Matrix(m.WeightMap);
            Console.WriteLine("________");
            Display<int>.Matrix(wayMap);
            stopwatch.Stop();
            Console.WriteLine($"\n{stopwatch.ElapsedMilliseconds}");

            //var s = new Point(2, 4);
            //var p1 = new Point(3, 4);
            //var p2 = new Point(2, 0);

            //var v1 = new Vector2(p1.X - s.X, p1.Y - s.Y);
            //var v2 = new Vector2(p2.X - s.X, p2.Y - s.Y);

            //var similarity = Vector2.Dot(v1, v2) / (v1.Length() * v2.Length());

            //var cur = new Point(0, 2);
            //var fin = new Point(2, 0);
            //var l = new List<Point>()
            //{
            //    new Point(0,3),
            //    new Point(0,1),
            //};

            //l.Sort((x, y) =>
            //{
            //    var vx = new Vector2(x.X - cur.X, x.Y - cur.Y);
            //    var vy = new Vector2(y.X - cur.X, y.Y - cur.Y);
            //    var vf = new Vector2(fin.X - cur.X, fin.Y - cur.Y);

            //    return Vector2.Dot(vx, vf) / (vx.Length() * vf.Length()) < Vector2.Dot(vy, vf) / (vy.Length() * vf.Length()) ? 1 : 0;
            //});
        }
    }
}
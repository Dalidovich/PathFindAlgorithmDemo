﻿using PathFindAlgorithmDemo.HelpFullStructures;

namespace PathFindAlgorithmDemo
{
    public static class SortPointExtension
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
    }
}
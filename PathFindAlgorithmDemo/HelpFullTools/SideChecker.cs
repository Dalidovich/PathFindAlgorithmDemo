using PathFindAlgorithmDemo.HelpFullStructures;

namespace PathFindAlgorithmDemo.HelpFullTools
{
    public static class SideChecker
    {
        public enum Sides
        {
            bottom,
            up,
            left,
            right,
        }

        public static readonly SideCheckerDelegate defaultSideCheckerDelegate;

        public delegate void SideCheckerDelegate(Point selected, int width, int height, List<Point> points, int fitValue, int[,] matrix);

        public static readonly Dictionary<Sides, SideCheckerDelegate> dictionary;

        static SideChecker()
        {
            SideCheckerDelegate bottom = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.Y != height - 1 && matrix[selected.Y + 1, selected.X] == fitValue)
                    points.Add(selected with { Y = selected.Y + 1 });
            };
            SideCheckerDelegate up = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.Y != 0 && matrix[selected.Y - 1, selected.X] == fitValue)
                    points.Add(selected with { Y = selected.Y - 1 });
            };
            SideCheckerDelegate left = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.X != width - 1 && matrix[selected.Y, selected.X + 1] == fitValue)
                    points.Add(selected with { X = selected.X + 1 });
            };
            SideCheckerDelegate right = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.X != 0 && matrix[selected.Y, selected.X - 1] == fitValue)
                    points.Add(selected with { X = selected.X - 1 });
            };

            defaultSideCheckerDelegate = bottom;
            defaultSideCheckerDelegate += up;
            defaultSideCheckerDelegate += left;
            defaultSideCheckerDelegate += right;

            dictionary = new Dictionary<Sides, SideCheckerDelegate>
            {
                { Sides.bottom, bottom },
                { Sides.up, up },
                { Sides.left, left },
                { Sides.right, right }
            };
        }
    }
}
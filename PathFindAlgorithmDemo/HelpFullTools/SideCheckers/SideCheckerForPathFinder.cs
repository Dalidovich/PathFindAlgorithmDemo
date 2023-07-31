using PathFindAlgorithmDemo.HelpFullStructures;

namespace PathFindAlgorithmDemo.HelpFullTools.SideCheckers
{
    public static partial class SideCheckerForPathFinder
    {
        public static readonly SideCheckerDelegateForPathFinder defaultSideCheckerDelegateForPathFinder;
        public static readonly Dictionary<Sides, SideCheckerDelegateForPathFinder> sideCheckerDelegateDictionaryForPathFinder;
        public delegate void SideCheckerDelegateForPathFinder(Point selected, int width, int height, List<Point> points, int fitValue, int[,] matrix);

        static SideCheckerForPathFinder()
        {
            SideCheckerDelegateForPathFinder bottom = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.Y != height - 1 && matrix[selected.Y + 1, selected.X] == fitValue)
                    points.Add(selected with { Y = selected.Y + 1 });
            };
            SideCheckerDelegateForPathFinder up = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.Y != 0 && matrix[selected.Y - 1, selected.X] == fitValue)
                    points.Add(selected with { Y = selected.Y - 1 });
            };
            SideCheckerDelegateForPathFinder left = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.X != width - 1 && matrix[selected.Y, selected.X + 1] == fitValue)
                    points.Add(selected with { X = selected.X + 1 });
            };
            SideCheckerDelegateForPathFinder right = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.X != 0 && matrix[selected.Y, selected.X - 1] == fitValue)
                    points.Add(selected with { X = selected.X - 1 });
            };

            defaultSideCheckerDelegateForPathFinder = bottom;
            defaultSideCheckerDelegateForPathFinder += up;
            defaultSideCheckerDelegateForPathFinder += left;
            defaultSideCheckerDelegateForPathFinder += right;

            sideCheckerDelegateDictionaryForPathFinder = new Dictionary<Sides, SideCheckerDelegateForPathFinder>
            {
                { Sides.bottom, bottom },
                { Sides.up, up },
                { Sides.left, left },
                { Sides.right, right }
            };
        }
    }
}
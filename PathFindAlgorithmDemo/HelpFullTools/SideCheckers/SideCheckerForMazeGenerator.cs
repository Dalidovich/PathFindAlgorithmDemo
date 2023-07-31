using PathFindAlgorithmDemo.HelpFullStructures;
using System;
using static PathFindAlgorithmDemo.HelpFullTools.SideCheckers.SideCheckerForPathFinder;

namespace PathFindAlgorithmDemo.HelpFullTools.SideCheckers
{
    public static class SideCheckerForMazeGenerator
    {
        public delegate void SideCheckerDelegateForMazeGenerator(Point selected, int width, int height, List<Point> points, bool fitValue, bool[,] matrix);
        public static readonly Dictionary<Sides, SideCheckerDelegateForMazeGenerator> sideCheckerDelegateDictionaryForMazeGenerator;
        public static readonly SideCheckerDelegateForMazeGenerator defaultSideCheckerDelegateForMazeGenerator;

        private static Random _random = new Random();

        static SideCheckerForMazeGenerator()
        {
            SideCheckerDelegateForMazeGenerator bottom = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.Y != height - 1 && selected.Y != height - 2 && matrix[selected.Y + 2, selected.X] == fitValue)
                    points.Add(selected with { Y = selected.Y + 2 });
            };
            SideCheckerDelegateForMazeGenerator up = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.Y != 0 && selected.Y != 1 && matrix[selected.Y - 2, selected.X] == fitValue)
                    points.Add(selected with { Y = selected.Y - 2 });
            };
            SideCheckerDelegateForMazeGenerator left = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.X != width - 1 && selected.X != width - 2 && matrix[selected.Y, selected.X + 2] == fitValue)
                    points.Add(selected with { X = selected.X + 2 });
            };
            SideCheckerDelegateForMazeGenerator right = (selected, width, height, points, fitValue, matrix) =>
            {
                if (selected.X != 0 && selected.X != 1 && matrix[selected.Y, selected.X - 2] == fitValue)
                    points.Add(selected with { X = selected.X - 2 });
            };

            defaultSideCheckerDelegateForMazeGenerator = bottom;
            defaultSideCheckerDelegateForMazeGenerator += up;
            defaultSideCheckerDelegateForMazeGenerator += left;
            defaultSideCheckerDelegateForMazeGenerator += right;

            sideCheckerDelegateDictionaryForMazeGenerator = new Dictionary<Sides, SideCheckerDelegateForMazeGenerator>
            {
                { Sides.bottom, bottom },
                { Sides.up, up },
                { Sides.left, left },
                { Sides.right, right }
            };
        }

        public static SideCheckerDelegateForMazeGenerator GetShuffledDefaultSideCheckerDelegateForMazeGenerator()
        {
            var invocationList = defaultSideCheckerDelegateForMazeGenerator.GetInvocationList();

            for (int i = 0; i < invocationList.Length - 1; i++)
            {
                int swapIndex = _random.Next(i + 1, invocationList.Length);
                var temp = invocationList[i];
                invocationList[i] = invocationList[swapIndex];
                invocationList[swapIndex] = temp;
            }

            SideCheckerDelegateForMazeGenerator shuffledDelegate = (SideCheckerDelegateForMazeGenerator)invocationList[0];
            invocationList.ToList().GetRange(1, invocationList.Length - 1).ForEach(x => shuffledDelegate += (SideCheckerDelegateForMazeGenerator)x);

            return shuffledDelegate;
        }
    }
}
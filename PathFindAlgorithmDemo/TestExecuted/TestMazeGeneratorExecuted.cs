using PathFindAlgorithmDemo.HelpFullStructures;
using PathFindAlgorithmDemo.HelpFullTools;

namespace PathFindAlgorithmDemo.TestExecuted
{
    public class TestMazeGeneratorExecuted
    {
        public static void testMazeGeneratorBreadthFirstGenerate()
        {
            var width = 500;
            var height = 500;
            var mg = new MazeGenerator(width, height);
            var mazeMap = mg.BreadthFirstGenerate();

            Point sPoint = new Point();
            Point fPoint = new Point();
            bool findStart = false;
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (mazeMap[i, k] != MazeGenerator.wall && !findStart)
                    {
                        sPoint = new Point(k, i);
                        findStart = true;
                    }
                    if (mazeMap[i, k] != MazeGenerator.wall)
                    {
                        fPoint = new Point(k, i);
                    }
                }
            }

            var maze = new Maze(height, width, sPoint, fPoint, mg.GetWallsOfMaze(mazeMap));

            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.DepthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Display.StartInSFML(solve, matrix.WeightMap);
        }

        public static void testMazeGeneratorDepthFirstGenerate()
        {
            var width = 500;
            var height = 500;
            var mg = new MazeGenerator(width, height);
            var mazeMap = mg.DepthFirstGenerate();

            Point sPoint = new Point();
            Point fPoint = new Point();
            bool findStart = false;
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (mazeMap[i, k] != MazeGenerator.wall && !findStart)
                    {
                        sPoint = new Point(k, i);
                        findStart = true;
                    }
                    if (mazeMap[i, k] != MazeGenerator.wall)
                    {
                        fPoint = new Point(k, i);
                    }
                }
            }

            var maze = new Maze(height, width, sPoint, fPoint, mg.GetWallsOfMaze(mazeMap));

            var matrix = new Matrix(maze.Height, maze.Width);
            matrix.SetWalls(maze.Walls);
            var solve = matrix.BreadthFirstSearch(maze.StartPoint, maze.FinishPoint);
            Display.StartInSFML(solve, matrix.WeightMap);
        }
    }
}

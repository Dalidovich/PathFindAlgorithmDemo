namespace PathFindAlgorithmDemo
{
    public static class Display<T>
    {
        public static void Matrix(T[,] matrix) 
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    Console.Write($"{matrix[i,k]}\t");
                }
                Console.WriteLine();
            }
        }
    }
}
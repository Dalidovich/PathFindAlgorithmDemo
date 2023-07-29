namespace PathFindAlgorithmDemo.HelpFullTools
{
    public static class PermutationCombinations<T>
    {
        public static List<T[]> GenerateCombinations(List<T> words, int start, int end)
        {
            var combinations = new List<T[]>();

            if (start == end)
            {
                combinations.Add(words.ToArray());
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    Swap(words, start, i);
                    combinations.AddRange(GenerateCombinations(words, start + 1, end));
                    Swap(words, start, i);
                }
            }

            return combinations;
        }

        static void Swap(List<T> words, int i, int j)
        {
            T temp = words[i];
            words[i] = words[j];
            words[j] = temp;
        }
    }
}
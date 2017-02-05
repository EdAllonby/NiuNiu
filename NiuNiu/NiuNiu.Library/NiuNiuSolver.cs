using System.Collections.Generic;

namespace NiuNiu.Library
{
    public class NiuNiuSolver
    {
        private List<List<int>> results;

        public IEnumerable<List<int>> Solve(IEnumerable<int> elements)
        {
            results = new List<List<int>>();
            RecursiveSolve(0, new List<int>(), new List<int>(elements), 0);
            return results;
        }

        private void RecursiveSolve(int currentSum, IReadOnlyCollection<int> included, IReadOnlyList<int> notIncluded, int startIndex)
        {
            for (int index = startIndex; index < notIncluded.Count; index++)
            {
                int nextValue = notIncluded[index];
                if ((currentSum + nextValue) % 10 == 0 && included.Count == 2)
                {
                    var newResult = new List<int>(included) { nextValue };
                    results.Add(newResult);
                }
                else if (included.Count < 3)
                {
                    var nextIncluded = new List<int>(included) { nextValue };

                    var nextNotIncluded = new List<int>(notIncluded);
                    nextNotIncluded.Remove(nextValue);
                    RecursiveSolve(currentSum + nextValue, nextIncluded, nextNotIncluded, startIndex++);
                }
            }
        }
    }
}
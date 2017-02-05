using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    public class NiuNiuSolver
    {
        private List<NiuNiuResult> results;
        private Hand currentHand;

        public NiuNiuResult Solve(Hand hand)
        {
            currentHand = hand;
            results = new List<NiuNiuResult> { new NiuNiuResult(currentHand) };
            RecursiveSolve(0, new List<Card>(), hand.Cards, 0);
            return results.Max();
        }

        private void RecursiveSolve(int currentSum, IReadOnlyCollection<Card> included, IReadOnlyList<Card> notIncluded, int startIndex)
        {
            for (int index = startIndex; index < notIncluded.Count; index++)
            {
                Card nextCard = notIncluded[index];
                if ((currentSum + nextCard.FaceValue) % 10 == 0 && included.Count == 2)
                {
                    var newResult = new List<Card>(included) { nextCard };
                    results.Add(new NiuNiuResult(currentHand, newResult));
                }
                else if (included.Count < 3)
                {
                    var nextIncluded = new List<Card>(included) { nextCard };

                    var nextNotIncluded = new List<Card>(notIncluded);
                    nextNotIncluded.Remove(nextCard);
                    RecursiveSolve(currentSum + nextCard.FaceValue, nextIncluded, nextNotIncluded, startIndex++);
                }
            }
        }
    }
}
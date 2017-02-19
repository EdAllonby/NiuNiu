using System.Collections.Generic;
using System.Linq;
using NiuNiu.Library.Domain;

namespace NiuNiu.Library.Solver
{
    /// <summary>
    /// Finds the best value from a hand for NiuNiu.
    /// </summary>
    public class HandSolver
    {
        private Hand currentHand;
        private List<HandValue> results;

        /// <summary>
        /// Gets the best hand value from a hand.
        /// </summary>
        /// <param name="hand">The <see cref="Hand" /> to solve.</param>
        /// <returns></returns>
        public HandValue Solve(Hand hand)
        {
            currentHand = hand;
            results = new List<HandValue> { new HandValue(currentHand) };
            RecursiveSolve(0, new List<Card>(), currentHand.Cards, 0);
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
                    results.Add(new HandValue(currentHand, newResult));
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
using System.Collections.Generic;

namespace NiuNiu.Library
{
    /// <summary>
    /// A hand is a collection of cards which can be added to, but only cleared.
    /// </summary>
    public sealed class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public IReadOnlyList<Card> Cards => cards.AsReadOnly();

        /// <summary>
        /// Total cards in this hand.
        /// </summary>
        public int TotalCards => cards.Count;

        /// <summary>
        /// Add a card to the current hand.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Clear the hand.
        /// </summary>
        public void EmptyHand()
        {
            cards.Clear();
        }
    }
}
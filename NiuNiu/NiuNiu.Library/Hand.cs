using System.Collections.Generic;

namespace NiuNiu.Library
{
    public sealed class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public IReadOnlyList<Card> Cards => cards.AsReadOnly();

        /// <summary>
        /// Total cards in this hand.
        /// </summary>
        public int TotalCards => cards.Count;

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void EmptyHand()
        {
            cards.Clear();
        }
    }
}
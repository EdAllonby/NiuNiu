using System.Collections.Generic;

namespace NiuNiu.Library
{
    public sealed class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Total cards in this hand.
        /// </summary>
        public int TotalCards => cards.Count;
    }
}
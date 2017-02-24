using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// A hand is a collection of cards which can be added to, but only cleared.
    /// </summary>
    public sealed class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public Hand()
        {
        }

        public Hand(IEnumerable<Card> cards)
        {
            this.cards.AddRange(cards);
        }

        public IReadOnlyList<Card> Cards => cards.AsReadOnly();

        /// <summary>
        /// Total cards in this hand.
        /// </summary>
        public int TotalCards => cards.Count;

        public int Count => cards.Count;

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

        public bool All(Func<object, bool> predicate)
        {
            return cards.All(predicate);
        }
    }
}
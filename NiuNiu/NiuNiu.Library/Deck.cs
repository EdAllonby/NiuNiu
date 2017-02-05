using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// The deck of 52 French playing cards is the most common deck of playing cards used today.
    /// The deck contains all suit and face combinations.
    /// </summary>
    public class Deck
    {
        private readonly List<Card> cards = new List<Card>();
        private readonly Random random = new Random();
        private readonly IShuffler shuffler = new FischerYatesShuffle();

        /// <summary>
        /// Create a new, unshuffled, deck of cards.
        /// </summary>
        public Deck()
        {
            foreach (Suit suit in EnumExtensions.GetValues<Suit>())
            {
                foreach (Face face in EnumExtensions.GetValues<Face>())
                {
                    cards.Add(new Card(suit, face));
                }
            }
        }

        /// <summary>
        /// The remaining cards in the deck.
        /// </summary>
        public int RemainingCards => cards.Count;

        /// <summary>
        /// Takes (and returns) a card from the deck.
        /// </summary>
        /// <returns></returns>
        public Card TakeCard()
        {
            Card topCard = cards.First();
            cards.Remove(topCard);
            return topCard;
        }

        /// <summary>
        /// Shuffle the deck.
        /// </summary>
        public void Shuffle()
        {
            shuffler.Shuffle(cards);
        }

        /// <summary>
        /// The action of taking (and removing) a number of cards from the top of the deck.
        /// </summary>
        /// <returns></returns>
        public List<Card> TakeTopHalfOfCardsByRandomSplit()
        {
            int randomSplit = random.Next(0, RemainingCards);
            var topHalf = new List<Card>();

            for (var cardCount = 0; cardCount <= randomSplit; cardCount++)
            {
                topHalf.Add(TakeCard());
            }

            return topHalf;
        }

        /// <summary>
        /// Push cards onto bottom of the deck, preserving order.
        /// </summary>
        /// <param name="cardsToAddToBottom"></param>
        public void PutCardsOnBottomOfDeck(IEnumerable<Card> cardsToAddToBottom)
        {
            foreach (Card card in cardsToAddToBottom)
            {
                cards.Add(card);
            }
        }
        
        public override string ToString()
        {
            return $"Remaining Cards: {RemainingCards}";
        }


    }
}
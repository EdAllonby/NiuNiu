using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// Used to store a hand's result.
    /// </summary>
    public sealed class HandValue : IComparable<HandValue>, IComparable
    {
        private readonly Hand hand;
        private readonly IEnumerable<Card> handTriple;

        /// <summary>
        /// A hand which has a triple.
        /// </summary>
        /// <param name="hand">The hand.</param>
        /// <param name="handTriple">The hand's triple.</param>
        public HandValue(Hand hand, IEnumerable<Card> handTriple)
        {
            this.hand = hand;
            this.handTriple = handTriple;
        }

        /// <summary>
        /// A hand without a triple.
        /// </summary>
        /// <param name="hand">The hand.</param>
        public HandValue(Hand hand) : this(hand, new List<Card>())
        {
        }

        /// <summary>
        /// The highest ranked card in the hand.
        /// </summary>
        public Card HighestSingleCard => hand.Cards.Max();

        /// <summary>
        /// The sum of the two cards outside the triple, where it's wrapped around modulus 10.
        /// </summary>
        private int DoubleCardSum
        {
            get
            {
                if (!HasTriple)
                {
                    return 0;
                }

                IEnumerable<Card> cardsOutsideTriple = CardsOutsideTriple();
                int sum = cardsOutsideTriple.Sum(card => card.FaceValue) % 10;
                return sum == 0 ? 10 : sum; // equaling modulo 10 is the best score (for example, 2 kings (10+10) equals 10, not 0).
            }
        }

        /// <summary>
        /// Returns if the result has a triple.
        /// </summary>
        public bool HasTriple => handTriple != null && handTriple.Count() == 3;

        public int CompareTo(object other)
        {
            var result = other as HandValue;

            if (result == null)
            {
                throw new ArgumentException($"Object must be of type {GetType()}.");
            }

            return CompareTo(result);
        }

        public int CompareTo(HandValue other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            // Having a triple when the other hand doesn't have a triple automatically makes this hand better.
            if (HasTriple && !other.HasTriple) return 1;
            if (!HasTriple && other.HasTriple) return -1;

            // If neither hand has a triple, then whoever has the highest single card wins.
            if (!HasTriple && !other.HasTriple) return HighestSingleCard.CompareTo(other.HighestSingleCard);

            // At this point, both hands have a triple. If the double card sum is the same, we compare against their highest card.
            if (DoubleCardSum.CompareTo(other.DoubleCardSum) == 0)
            {
                return HighestSingleCard.CompareTo(other.HighestSingleCard);
            }

            // At this point , both hands have a triple. If someone has a higher double card sum, their hand is automatically better.
            return DoubleCardSum.CompareTo(other.DoubleCardSum);
        }

        public override string ToString()
        {
            return HasTriple
                ? $"Result has a triple, double card sum is {DoubleCardSum}, highest single card is {HighestSingleCard}"
                : $"Result does not have a triple, highest single card is {HighestSingleCard}";
        }

        private IEnumerable<Card> CardsOutsideTriple()
        {
            return hand.Cards.Where(card1 => handTriple.All(card2 => !Equals(card2, card1)));
        }
    }
}
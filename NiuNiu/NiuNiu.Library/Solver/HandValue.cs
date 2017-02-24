using System;
using System.Collections.Generic;
using System.Linq;
using NiuNiu.Library.Domain;
using NiuNiu.Library.Gambling;

namespace NiuNiu.Library.Solver
{
    /// <summary>
    /// Used to store a hand's result.
    /// </summary>
    public sealed class HandValue : IPayoutValue, IComparable<HandValue>, IComparable
    {
        private readonly Hand hand;
        private readonly Hand handTriple;

        /// <summary>
        /// A hand which has a triple.
        /// </summary>
        /// <param name="hand">The hand.</param>
        /// <param name="handTriple">The hand's triple.</param>
        public HandValue(Hand hand, Hand handTriple)
        {
            this.hand = hand;
            this.handTriple = handTriple;
        }

        /// <summary>
        /// A hand without a triple.
        /// </summary>
        /// <param name="hand">The hand.</param>
        public HandValue(Hand hand) : this(hand, new Hand())
        {
        }

        /// <summary>
        /// The highest ranked card in the hand.
        /// </summary>
        private Card HighestCard => hand.Cards.Max();

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
            if (!HasTriple && !other.HasTriple) return HighestCard.CompareTo(other.HighestCard);

            // At this point, both hands have a triple. If the double card sum is the same, we compare against their highest card.
            if (DoubleCardSum.CompareTo(other.DoubleCardSum) == 0)
            {
                return HighestCard.CompareTo(other.HighestCard);
            }

            // At this point, both hands have a triple. If someone has a higher double card sum, their hand is automatically better.
            return DoubleCardSum.CompareTo(other.DoubleCardSum);
        }

        /// <summary>
        /// The face of the highest card.
        /// </summary>
        public Face HighestCardFace => HighestCard.Face;

        /// <summary>
        /// Returns if the result has a triple.
        /// </summary>
        public bool HasTriple => handTriple != null && handTriple.Count == 3;

        public bool IsUltimate => hand.Cards.All(x => x.IsFaceCard);

        public override string ToString()
        {
            return HasTriple
                ? $"Result has a triple, double card sum is {DoubleCardSum}, highest single card is {HighestCard}"
                : $"Result does not have a triple, highest single card is {HighestCard}";
        }

        public static bool operator <(HandValue left, HandValue right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator >(HandValue left, HandValue right)
        {
            return Compare(left, right) > 0;
        }

        private static int Compare(HandValue left, HandValue right)
        {
            if (ReferenceEquals(left, right))
            {
                return 0;
            }
            if (ReferenceEquals(left, null))
            {
                return -1;
            }
            return left.CompareTo(right);
        }


        private IEnumerable<Card> CardsOutsideTriple()
        {
            return hand.Cards.Where(card1 => handTriple.All(card2 => !Equals(card2, card1)));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    public sealed class NiuNiuResult : IComparable<NiuNiuResult>, IComparable
    {
        private readonly Hand hand;
        private readonly IEnumerable<Card> modulusOfTen;

        public NiuNiuResult(Hand hand, IEnumerable<Card> modulusOfTen)
        {
            this.hand = hand;
            this.modulusOfTen = modulusOfTen;
        }

        public NiuNiuResult(Hand hand) : this(hand, new List<Card>())
        {
        }

        public Card HighestSingleCard
        {
            get
            {
                if (!HasTriple)
                {
                    return hand.Cards.Max();
                }

                IEnumerable<Card> cardsOutsideModulus = hand.Cards.Where(card1 => modulusOfTen.All(card2 => !Equals(card2, card1)));
                return cardsOutsideModulus.Max();
            }
        }

        public bool HasTriple => modulusOfTen != null && modulusOfTen.Count() == 3;

        public Card HighestTriple => modulusOfTen?.Max();

        public int CompareTo(object other)
        {
            var result = other as NiuNiuResult;

            if (result == null)
            {
                throw new ArgumentException($"Object must be of type {GetType()}.");
            }

            return CompareTo(result);
        }

        public int CompareTo(NiuNiuResult other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (HasTriple && !other.HasTriple) return 1;
            if (!HasTriple && other.HasTriple) return -1;

            if (!HasTriple && !other.HasTriple) return HighestSingleCard.CompareTo(other.HighestSingleCard);

            if (HighestSingleCard.CompareTo(other.HighestSingleCard) == 0)
            {
                return HighestTriple.CompareTo(other.HighestTriple);
            }

            return HighestSingleCard.CompareTo(other.HighestSingleCard);
        }

        public override string ToString()
        {
            return HasTriple
                ? $"Result has a triple, highest single card is {HighestSingleCard}, highest triple card is {HighestTriple}"
                : $"Result does not have a triple, highest single card is {HighestSingleCard}";
        }
    }
}
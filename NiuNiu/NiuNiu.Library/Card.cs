using System;

namespace NiuNiu.Library
{
    /// <summary>
    /// A card holds a combination of a face and suit. It is comparable to other cards.
    /// </summary>
    public class Card : IEquatable<Card>, IComparable<Card>, IComparable
    {
        public Card(Face face, Suit suit)
        {
            Face = face;
            Suit = suit;
        }

        /// <summary>
        /// The face of the playing card.
        /// </summary>
        public Face Face { get; }

        /// <summary>
        /// The suit of the playing card.
        /// </summary>
        public Suit Suit { get; }

        /// <summary>
        /// The value of the card face.
        /// This is useful, as niuniu considers all royal cards as a value of '10'.
        /// </summary>
        public int FaceValue
        {
            get
            {
                if (Face > Face.Ten)
                {
                    return 10;
                }

                return (int) Face;
            }
        }

        public int CompareTo(object other)
        {
            var card = other as Card;

            if (card == null)
            {
                throw new ArgumentException("Object must be of type Card.");
            }

            return CompareTo(card);
        }

        public int CompareTo(Card other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            int faceComparison = Face.CompareTo(other.Face);
            if (faceComparison != 0) return faceComparison;
            return Suit.CompareTo(other.Suit);
        }

        public bool Equals(Card other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Suit == other.Suit && Face == other.Face;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Card) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Suit * 397) ^ (int) Face;
            }
        }

        public override string ToString()
        {
            return $"{Face} of {Suit}";
        }
    }
}
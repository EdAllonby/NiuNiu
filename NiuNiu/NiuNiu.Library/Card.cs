namespace NiuNiu.Library
{
    /// <summary>
    /// A card holds a combination of a suit and face.
    /// </summary>
    public class Card
    {
        public Card(Suit cardSuit, Face cardFace)
        {
            Suit = cardSuit;
            Face = cardFace;
        }

        /// <summary>
        /// The suit of the playing card.
        /// </summary>
        public Suit Suit { get; }

        /// <summary>
        /// The face of the playing card.
        /// </summary>
        public Face Face { get; }

        public override string ToString()
        {
            return $"{Face} of {Suit}";
        }
    }
}
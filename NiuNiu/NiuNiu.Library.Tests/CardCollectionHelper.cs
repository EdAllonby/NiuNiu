using System.Collections.Generic;

namespace NiuNiu.Library.Tests
{
    public static class CardCollectionHelper
    {
        public static Hand AllRoyalsHand
        {
            get
            {
                var hand = new Hand();
                hand.AddCard(new Card(Suit.Clubs, Face.King));
                hand.AddCard(new Card(Suit.Spades, Face.King));
                hand.AddCard(new Card(Suit.Clubs, Face.Queen));
                hand.AddCard(new Card(Suit.Hearts, Face.King));
                hand.AddCard(new Card(Suit.Diamonds, Face.King));
                return hand;
            }
        }

        public static IEnumerable<Card> AllRoyalsTriple => new List<Card> { new Card(Suit.Clubs, Face.King), new Card(Suit.Spades, Face.King), new Card(Suit.Diamonds, Face.King) };
    }
}
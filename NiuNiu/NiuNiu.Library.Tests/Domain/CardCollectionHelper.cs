using System.Collections.Generic;
using NiuNiu.Library.Domain;

namespace NiuNiu.Library.Tests.Domain
{
    public static class CardCollectionHelper
    {
        public static Hand AllRoyalsHand
        {
            get
            {
                var hand = new Hand();
                hand.AddCard(new Card(Face.King, Suit.Clubs));
                hand.AddCard(new Card(Face.King, Suit.Spades));
                hand.AddCard(new Card(Face.Queen, Suit.Clubs));
                hand.AddCard(new Card(Face.King, Suit.Hearts));
                hand.AddCard(new Card(Face.King, Suit.Diamonds));
                return hand;
            }
        }

        public static Hand AllRoyalsTriple => new Hand(new List<Card>
        {
            new Card(Face.King, Suit.Clubs),
            new Card(Face.King, Suit.Spades),
            new Card(Face.King, Suit.Diamonds)
        });
    }
}
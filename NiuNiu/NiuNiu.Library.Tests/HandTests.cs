using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class HandTests
    {
        [Test]
        public void AddingCardToHandIncreasesTotal()
        {
            var hand = new Hand();
            hand.AddCard(new Card(Face.King, Suit.Clubs));
            Assert.AreEqual(hand.TotalCards, 1);
        }
    }
}
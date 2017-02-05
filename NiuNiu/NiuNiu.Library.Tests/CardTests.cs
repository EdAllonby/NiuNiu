using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void CardCanHaveASuit()
        {
            const Suit expectedSuit = Suit.Clubs;

            var card = new Card(expectedSuit, Face.Ace);

            Assert.AreEqual(Suit.Clubs, card.Suit);
        }

        [Test]
        public void CardCanHaveAValue()
        {
            const Face expectedValue = Face.Eight;

            var card = new Card(Suit.Hearts, expectedValue);

            Assert.AreEqual(expectedValue, card.Face);
        }

        [TestCase(Face.Jack, ExpectedResult = 10)]
        [TestCase(Face.Queen, ExpectedResult = 10)]
        [TestCase(Face.King, ExpectedResult = 10)]
        public int RoyalCardsAreConsideredAValueOf10(Face cardFace)
        {
            var card = new Card(Suit.Hearts, cardFace);

            return card.FaceValue;
        }

        [Test]
        public void TwoCardsWithSameFaceAndSuitShouldBeEqual()
        {
            const Suit suit = Suit.Clubs;
            const Face face = Face.Four;

            var firstCard = new Card(suit, face);
            var secondCard = new Card(suit, face);

            Assert.AreEqual(firstCard,secondCard);
        }

        [Test]
        public void SpadesAreHigherValueThanHearts()
        {
            var firstCard = new Card(Suit.Spades, Face.Four);
            var secondCard = new Card(Suit.Hearts, Face.Four);

            Assert.Greater(firstCard, secondCard);
        }

        [Test]
        public void KingssAreHigherValueThanQueens()
        {
            var firstCard = new Card(Suit.Hearts, Face.King);
            var secondCard = new Card(Suit.Spades, Face.Queen);

            Assert.Greater(firstCard, secondCard);
        }
    }
}
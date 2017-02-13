using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class CardTests
    {
        [TestCase(Face.Jack, ExpectedResult = 10)]
        [TestCase(Face.Queen, ExpectedResult = 10)]
        [TestCase(Face.King, ExpectedResult = 10)]
        public int RoyalCardsAreConsideredAValueOf10(Face cardFace)
        {
            var card = new Card(cardFace, Suit.Hearts);

            return card.FaceValue;
        }

        [Test]
        public void CardCanHaveASuit()
        {
            const Suit expectedSuit = Suit.Clubs;

            var card = new Card(Face.Ace, expectedSuit);

            Assert.AreEqual(Suit.Clubs, card.Suit);
        }

        [Test]
        public void CardCanHaveAValue()
        {
            const Face expectedValue = Face.Eight;

            var card = new Card(expectedValue, Suit.Hearts);

            Assert.AreEqual(expectedValue, card.Face);
        }

        [Test]
        public void KingssAreHigherValueThanQueens()
        {
            var firstCard = new Card(Face.King, Suit.Hearts);
            var secondCard = new Card(Face.Queen, Suit.Spades);

            Assert.Greater(firstCard, secondCard);
        }

        [Test]
        public void SpadesAreHigherValueThanHearts()
        {
            var firstCard = new Card(Face.Four, Suit.Spades);
            var secondCard = new Card(Face.Four, Suit.Hearts);

            Assert.Greater(firstCard, secondCard);
        }

        [Test]
        public void TwoCardsWithSameFaceAndSuitShouldBeEqual()
        {
            const Suit suit = Suit.Clubs;
            const Face face = Face.Four;

            var firstCard = new Card(face, suit);
            var secondCard = new Card(face, suit);

            Assert.AreEqual(firstCard, secondCard);
        }
    }
}
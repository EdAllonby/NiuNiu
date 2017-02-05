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
    }
}
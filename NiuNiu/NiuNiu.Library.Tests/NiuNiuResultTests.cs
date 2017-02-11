using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class NiuNiuResultTests
    {
        [Test]
        public void AllTensShouldHaveResultOf10()
        {
            Hand hand = CardCollectionHelper.AllRoyalsHand;

            var niuNiuResult = new HandValue(hand, CardCollectionHelper.AllRoyalsTriple);

            Assert.AreEqual(new Card(Suit.Spades, Face.King), niuNiuResult.HighestSingleCard);
        }
    }
}
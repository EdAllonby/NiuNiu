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

            var niuNiuResult = new NiuNiuResult(hand, CardCollectionHelper.AllRoyalsTriple);

            Assert.AreEqual(new Card(Suit.Hearts, Face.King), niuNiuResult.HighestSingleCard);
        }
    }
}
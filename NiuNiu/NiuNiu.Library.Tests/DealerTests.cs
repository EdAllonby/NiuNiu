using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public sealed class DealerTests
    {
        [Test]
        public void DealerCanSplitDeck()
        {
            var dealer = new Dealer(new Player("Steve", 1000));
            dealer.SplitDeck();

            Assert.IsTrue(dealer.HasSplitDeck);
        }

        [Test]
        public void PuttingSplitBackOntoDeckRemovesDealerSplit()
        {
            var dealer = new Dealer(new Player("Steve", 1000));
            dealer.SplitDeck();
            dealer.PutSplitTopHalfOnBottomOfDeck();
            Assert.IsFalse(dealer.HasSplitDeck);
        }
    }
}
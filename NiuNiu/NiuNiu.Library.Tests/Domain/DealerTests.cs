using System.Collections.Generic;
using Moq;
using NiuNiu.Library.Domain;
using NiuNiu.Library.Gambling;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Domain
{
    [TestFixture]
    public sealed class DealerTests
    {
        private static Dealer DefaultDealer()
        {
            return new Dealer(new Player("Steve", 1000, new DefaultGamblingStrategy()));
        }

        [Test]
        public void DealerCanSplitDeck()
        {
            Dealer dealer = DefaultDealer();
            dealer.SplitDeck();

            Assert.IsTrue(dealer.HasSplitDeck);
        }

        [Test]
        public void DealerGivesTheCorrectAmountOfCardsToPlayers()
        {
            const int cardsToDealToPlayer = 4;
            Dealer dealer = DefaultDealer();
            var firstPlayer = new Mock<ICardHandler>();
            var secondPlayer = new Mock<ICardHandler>();

            var cardHandlers = new List<ICardHandler> { firstPlayer.Object, secondPlayer.Object };

            dealer.DealCards(cardHandlers, cardsToDealToPlayer);

            firstPlayer.Verify(x => x.ReceiveCard(It.IsAny<Card>()), Times.Exactly(cardsToDealToPlayer));
            secondPlayer.Verify(x => x.ReceiveCard(It.IsAny<Card>()), Times.Exactly(cardsToDealToPlayer));
        }

        [Test]
        public void IfDealerHasNotSplitDeckDoNotDoAnything()
        {
            Dealer dealer = DefaultDealer();
            dealer.PutSplitTopHalfOnBottomOfDeck();
            Assert.IsFalse(dealer.HasSplitDeck);
        }

        [Test]
        public void PuttingSplitBackOntoDeckRemovesDealerSplit()
        {
            Dealer dealer = DefaultDealer();
            dealer.SplitDeck();
            dealer.PutSplitTopHalfOnBottomOfDeck();
            Assert.IsFalse(dealer.HasSplitDeck);
        }
    }
}
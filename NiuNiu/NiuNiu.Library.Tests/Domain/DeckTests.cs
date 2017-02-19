using System.Collections.Generic;
using NiuNiu.Library.Domain;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Domain
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void DeckHas52Cards()
        {
            var cardDeck = new Deck();

            cardDeck.Shuffle();

            Assert.AreEqual(cardDeck.RemainingCards, 52);
        }

        [Test]
        public void ShufflingDeckWillPreserveRemainingCards()
        {
            var cardDeck = new Deck();

            cardDeck.TakeCard();
            cardDeck.TakeCard();

            cardDeck.Shuffle();

            Assert.AreEqual(cardDeck.RemainingCards, 50);
        }

        [Test]
        public void SplitingDeckInHalfRemovesThatNumberOfCardsFromDeck()
        {
            var cardDeck = new Deck();
            int deckTotal = cardDeck.RemainingCards;
            List<Card> cards = cardDeck.TakeTopHalfOfCardsByRandomSplit();
            int expectedRemainingCardsInDeck = deckTotal - cards.Count;

            Assert.AreEqual(expectedRemainingCardsInDeck, cardDeck.RemainingCards);
        }

        [Test]
        public void TakingCardFromANewDeckWillRemoveACard()
        {
            var cardDeck = new Deck();

            cardDeck.TakeCard();

            Assert.AreEqual(cardDeck.RemainingCards, 51);
        }
    }
}
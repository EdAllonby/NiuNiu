using System.Collections.Generic;
using System.Linq;
using NiuNiu.Library.Utility;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// A dealer is a <see cref="Player" /> of the game who controls the flow of the deck for the current round.
    /// </summary>
    public sealed class Dealer : Player
    {
        private readonly Deck deck;
        private List<Card> splitTopHalfOfDeck;

        public Dealer(Player player) : base(player)
        {
            deck = new Deck();
        }

        /// <summary>
        /// Determines if the dealer has already split the deck into 2 parts.
        /// </summary>
        public bool HasSplitDeck => splitTopHalfOfDeck != null && splitTopHalfOfDeck.Any();

        /// <summary>
        /// Number of times the dealer has dealt cards.
        /// </summary>
        public int TimesDealt { get; private set; }

        public void TakeHandFromPlayers(IEnumerable<ICardHandler> players)
        {
            foreach (ICardHandler player in players)
            {
                IEnumerable<Card> playerHand = player.ReturnCards();
                deck.AddHandToDeck(playerHand);
            }
        }

        public void SplitDeck()
        {
            splitTopHalfOfDeck = deck.TakeTopHalfOfCardsByRandomSplit();
        }

        /// <summary>
        /// Deal cards around the table for a new round.
        /// </summary>
        /// <param name="players">The players to deal cards to.</param>
        /// <param name="cardsPerPlayer">The cards to deal per player.</param>
        public void DealCards(IList<ICardHandler> players, int cardsPerPlayer)
        {
            // We want to capture how many times this dealer has dealt cards because there are some game rules around this number.
            TimesDealt++;

            ICardHandler firstPlayer = GetFirstPlayer(players);

            foreach (ICardHandler player in players.CycleTo(firstPlayer, cardsPerPlayer * players.Count))
            {
                player.ReceiveCard(deck.TakeCard());
            }
        }

        public void PutSplitTopHalfOnBottomOfDeck()
        {
            if (!HasSplitDeck)
            {
                return;
            }

            deck.PutCardsOnBottomOfDeck(splitTopHalfOfDeck);
            splitTopHalfOfDeck.Clear();
        }

        public void CutDeck()
        {
            SplitDeck();
            PutSplitTopHalfOnBottomOfDeck();
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        private ICardHandler GetFirstPlayer(IList<ICardHandler> players)
        {
            SplitDeck();
            Card bottomCard = ShowBottomOfSplitDeck();
            var faceValue = (int) bottomCard.Face;
            ICardHandler player = players.ElementAtLoopedIndex(faceValue);
            PutSplitTopHalfOnBottomOfDeck();

            return player;
        }

        private Card ShowBottomOfSplitDeck()
        {
            return splitTopHalfOfDeck.LastOrDefault();
        }
    }
}
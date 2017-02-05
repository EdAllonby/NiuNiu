using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// A dealer controls the flow of the deck for the current game.
    /// </summary>
    public class Dealer
    {
        private readonly Deck deck;
        private List<Card> splitTopHalfOfDeck;

        public Dealer(Player player)
        {
            deck = new Deck();
            Player = player;
        }

        public bool HasSplitDeck => splitTopHalfOfDeck != null && splitTopHalfOfDeck.Any();

        public Player Player { get; }

        public void SplitDeck()
        {
            splitTopHalfOfDeck = deck.TakeTopHalfOfCardsByRandomSplit();
        }

        public void DealCards(List<Player> players, int cardsPerPlayer)
        {
            Player firstPlayer = GetFirstPlayer(players);

            foreach (Player player in players.Cycle(firstPlayer).Take(cardsPerPlayer * players.Count))
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

        public void SplitDeckShuffle()
        {
            SplitDeck();
            PutSplitTopHalfOnBottomOfDeck();
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }

        private Player GetFirstPlayer(List<Player> players)
        {
            SplitDeck();
            Card bottomCard = ShowBottomOfSplitDeck();
            var faceValue = (int) bottomCard.Face;
            Player player = players.ElementAtLoopedIndex(faceValue);
            PutSplitTopHalfOnBottomOfDeck();

            return player;
        }

        private Card ShowBottomOfSplitDeck()
        {
            return splitTopHalfOfDeck.LastOrDefault();
        }
    }
}
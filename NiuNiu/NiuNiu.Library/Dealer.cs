using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// A dealer is a <see cref="Player" /> of the game who controls the flow of the deck for the current round.
    /// </summary>
    public class Dealer : Player
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

        public void TakeHandFromPlayer(Player player)
        {
            IEnumerable<Card> playerHand = player.ReturnAllCardsInHand();
            deck.AddHandToDeck(playerHand);
        }

        public void SplitDeck()
        {
            splitTopHalfOfDeck = deck.TakeTopHalfOfCardsByRandomSplit();
        }

        public void DealCards(List<Player> players, int cardsPerPlayer)
        {
            TimesDealt++;

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

        private Player GetFirstPlayer(IList<Player> players)
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
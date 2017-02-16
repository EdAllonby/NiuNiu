using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// A dealer controls the flow of the deck for the current game.
    /// </summary>
    public class Dealer : IMoneyReceiver, IMoneyGiver
    {
        private readonly Bank bank;
        private readonly Deck deck;
        private List<Card> splitTopHalfOfDeck;

        public Dealer(Player player)
        {
            deck = new Deck();
            Player = player;
            bank = player.Bank;
        }

        public bool HasSplitDeck => splitTopHalfOfDeck != null && splitTopHalfOfDeck.Any();

        public Player Player { get; }

        /// <summary>
        /// Number of times the dealer has dealt cards.
        /// </summary>
        public int TimesDealt { get; private set; }

        public void GiveMoney(IMoneyReceiver receiver, int amount)
        {
            bank.Withdraw(receiver, amount);
        }

        public void ReceiveMoney(int amount)
        {
            bank.Deposit(amount);
        }

        /// <summary>
        /// Whether the dealer should take the current pot.
        /// </summary>
        /// <param name="potValue">The current pot value.</param>
        /// <returns>Whether the dealer should take the pot.</returns>
        public bool ShouldTakePot(int potValue)
        {
            return Player.ShouldTakePot(potValue);
        }

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
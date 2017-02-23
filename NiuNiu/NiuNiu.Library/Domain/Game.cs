using System.Collections.Generic;
using System.Linq;
using NiuNiu.Library.Gambling;
using NiuNiu.Library.Solver;
using NiuNiu.Library.Utility;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// Models a NiuNiu game.
    /// </summary>
    public sealed class Game
    {
        private readonly IPayout payout = new StandardPayout();
        private readonly List<Player> players;
        private readonly Pot pot = new Pot();
        private Dealer dealer;

        public Game(List<Player> players)
        {
            this.players = players;
            AssignNewDealer();
        }

        public Player CurrentBest => players.OrderByDescending(x => x.Money).First();

        /// <summary>
        /// Total rounds played in this game.
        /// </summary>
        public int Round { get; private set; }

        /// <summary>
        /// If players still remain in this game.
        /// </summary>
        public bool IsInProgress => players.Count > 1;

        /// <summary>
        /// Play a round of NiuNiu.
        /// </summary>
        public void PlayRound()
        {
            Round++;

            dealer.Shuffle();
            dealer.CutDeck();
            PlaceBets();
            dealer.DealCards(players.ToList<ICardHandler>(), GameRules.CardsPerHand);
            DealMoney();
            dealer.TakeHandFromPlayers(players);

            if (!pot.HasMoney)
            {
                AssignNewDealer();
            }

            if (CanDealerTakePot() && dealer.ShouldTakePot(pot.Value))
            {
                pot.GiveToDealer(dealer);
                AssignNewDealer();
            }

            RemoveSpentPlayers();

            if (!IsInProgress)
            {
                // Final round, no more players left. Dealer gets the pot.
                pot.GiveToDealer(dealer);
            }
        }

        private bool CanDealerTakePot()
        {
            // In NiuNiu, the dealer must have dealt at least 3 times before being able to take the pot.
            return dealer.TimesDealt >= 3;
        }

        private void PlaceBets()
        {
            // The current dealer doesn't place a bet, as they created the pot with an initial bet.
            foreach (Player player in players.Where(player => !player.Equals(dealer)))
            {
                player.PlaceBet(pot);
            }
        }

        private void RemoveSpentPlayers()
        {
            // Don't try to remove the dealer, as they might win some money at the end of the round.
            players.RemoveAll(player => player.Money == 0 && !player.Equals(dealer));
        }

        private void DealMoney()
        {
            HandValue dealerHand = dealer.HandValue;

            // Players who have lost pays the dealer first.
            foreach (Player player in players.Where(player => player.HandValue < dealerHand))
            {
                payout.Payout(dealer.HandValue, player.LastBet, player, pot);
            }

            // Then players who have higher hand value than the dealer get paid. The order in which players get paid out is by their hand value.
            foreach (Player player in players.OrderByDescending(player => player.HandValue).Where(player => player.HandValue > dealerHand))
            {
                payout.Payout(player.HandValue, player.LastBet, pot, player);
            }
        }

        private void AssignNewDealer()
        {
            Player newDealer = dealer == null
                ? players.PickRandom() // If this is a new game, we'll start with a random dealer
                : players.NextInLoop(dealer); // Otherwise, go around the table to the next player.

            dealer = new Dealer(newDealer);
            players.ReplaceFirstOccurrence(newDealer, dealer);
            dealer.GiveMoney(pot, GameRules.PotSize);
        }
    }
}
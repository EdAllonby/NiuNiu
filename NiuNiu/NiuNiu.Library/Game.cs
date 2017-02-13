using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// Models a NiuNiu game.
    /// </summary>
    public sealed class Game
    {
        private readonly List<Player> players;
        private readonly Pot pot = new Pot();
        private Dealer dealer;

        public Game(List<Player> players)
        {
            this.players = players;
            AssignNewDealer();
        }

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
            dealer.SplitDeckShuffle();
            PlaceBets();
            dealer.DealCards(players, GameRules.CardsPerHand);
            DealMoney();

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
            foreach (Player player in players.Where(player => dealer.Player != player))
            {
                player.PlaceBet(pot);
            }
        }

        private void RemoveSpentPlayers()
        {
            // Don't try to remove the dealer, as they might win some money at the end of the round.
            players.RemoveAll(player => player.Money == 0 && dealer.Player != player);
        }

        private void DealMoney()
        {
            List<Player> playersOrderedByHand = players.OrderByDescending(player => player.HandValue).ToList();

            int dealerRank = playersOrderedByHand.IndexOf(dealer.Player);

            foreach (Player player in playersOrderedByHand)
            {
                int playerRank = playersOrderedByHand.IndexOf(player);

                if (playerRank < dealerRank)
                {
                    // Player had a better hand than the dealer. Give the correct amount from the current pot.
                    int multiplier = GetMoneyMultiplier(player);
                    pot.GiveMoney(player, player.LastBet * multiplier);
                }
                else if (playerRank > dealerRank)
                {
                    // The dealer has won. Add money to the pot from losing player.
                    int multiplier = GetMoneyMultiplier(dealer.Player);
                    player.GiveMoney(pot, player.LastBet * multiplier);
                }

                dealer.TakeHandFromPlayer(player);
            }
        }

        private static int GetMoneyMultiplier(Player player)
        {
            var multiplier = 1;

            if (!player.HandValue.HasTriple)
            {
                return multiplier;
            }

            if (player.ShowHand.All(card => card.Face >= Face.Jack))
            {
                multiplier = 5;
            }

            if (player.HandValue.HighestSingleCard.Face >= Face.Ten && player.HandValue.HighestSingleCard.Face <= Face.King)
            {
                multiplier = 3;
            }

            if (player.HandValue.HighestSingleCard.Face >= Face.Seven && player.HandValue.HighestSingleCard.Face <= Face.Nine)
            {
                multiplier = 2;
            }

            return multiplier;
        }

        private void AssignNewDealer()
        {
            Player newDealer = dealer == null
                ? players.PickRandom() // If this is a new game, we'll start with a random dealer
                : players.NextInLoop(dealer.Player); // Otherwise, go around the table to the next player.

            dealer = new Dealer(newDealer);
            dealer.GiveMoney(pot, GameRules.PotSize);
        }
    }
}
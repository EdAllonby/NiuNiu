using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// Models a NiuNiu game.
    /// </summary>
    public sealed class Game
    {
        private const int DefaultBet = 100;

        private readonly List<Player> players = new List<Player>();
        private Dealer dealer;

        public Game()
        {
            const int startingMoney = 1000000;

            foreach (int player in Enumerable.Range(0, GameRules.TotalPlayers))
            {
                players.Add(new Player(startingMoney));
            }

            AssignNewDealer();
        }

        public int Round { get; private set; }

        public bool PlayersRemain => players.Count > 1;

        public void PlayRound()
        {
            Round++;
            RemoveSpentPlayers();

            if (dealer.Money == 0)
            {
                AssignNewDealer();
            }

            dealer.Shuffle();
            dealer.SplitDeckShuffle();
            dealer.DealCards(players, GameRules.CardsPerHand);
            DealMoney();
        }

        private void RemoveSpentPlayers()
        {
            players.RemoveAll(player => player.Money == 0);
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
                    // Player had a better hand than the dealer. Give the correct amount from the dealer's pot.
                    int multiplier = GetMoneyMultiplier(player);
                    dealer.GiveMoney(player, DefaultBet * multiplier);
                }
                else if (playerRank > dealerRank)
                {
                    // The dealer has won. Give money to dealer from losing player.
                    int multiplier = GetMoneyMultiplier(dealer.Player);
                    player.GiveMoney(dealer, DefaultBet * multiplier);
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
        }
    }
}
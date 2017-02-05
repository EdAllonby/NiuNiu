using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    public sealed class Game
    {

        private readonly List<Player> players = new List<Player>();
        private Dealer dealer;

        public Game()
        {
            const int startingMoney = 1000;

            foreach (int player in Enumerable.Range(0, GameRules.TotalPlayers))
            {
                players.Add(new Player(startingMoney + player));
            }
        }

        public void PlayRound()
        {
            AssignNewDealer();
            dealer.Shuffle();
            dealer.SplitDeckShuffle();
            dealer.DealCards(players, GameRules.CardsPerHand);
            FindBestPlayer();
        }

        private void FindBestPlayer()
        {
            IOrderedEnumerable<Player> playersOrderedByHand = players.OrderBy(player => player.CalculateHandValue());
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
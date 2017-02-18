using System.Collections.Generic;
using NiuNiu.Library;

namespace NiuNiu.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int startingMoney = 1000;

            var gamblingStrategy = new DefaultGamblingStrategy();

            var players = new List<Player>
            {
                new Player("Ed", startingMoney, gamblingStrategy),
                new Player("Nige", startingMoney, gamblingStrategy),
                new Player("Steve", startingMoney, gamblingStrategy),
                new Player("John", startingMoney, gamblingStrategy)
            };

            var niuniu = new Game(players);

            while (niuniu.IsInProgress)
            {
                niuniu.PlayRound();
            }

            int totalRounds = niuniu.Round;

            System.Console.WriteLine($"Total rounds played: {totalRounds}");
        }
    }
}
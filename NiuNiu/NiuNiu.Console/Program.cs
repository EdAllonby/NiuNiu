using System.Collections.Generic;
using NiuNiu.Library.Domain;
using NiuNiu.Library.Gambling;

namespace NiuNiu.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int startingMoney = 1000;

            while (true)
            {
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

                System.Console.WriteLine($"{niuniu.CurrentBest} won in {totalRounds} rounds.");
            }
        }
    }
}
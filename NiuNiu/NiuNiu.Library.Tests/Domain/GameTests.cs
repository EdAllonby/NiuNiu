using System.Collections.Generic;
using System.Linq;
using NiuNiu.Library.Domain;
using NiuNiu.Library.Gambling;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Domain
{
    [TestFixture]
    internal class GameTests
    {
        [Test]
        public void AfterGameEndNoMoneyShouldBeMissing()
        {
            var gamblingStrategy = new DefaultGamblingStrategy();

            const int startingMoney = 1000;

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

            Assert.AreEqual(4000, players.Sum(x => x.Money));
        }
    }
}
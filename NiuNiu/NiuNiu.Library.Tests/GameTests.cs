using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    internal class GameTests
    {
        [Test]
        public void AfterGameEndNoMoneyShouldBeMissing()
        {
            var players = new List<Player> { new Player("Ed", 1000), new Player("Steve", 1000), new Player("Terry", 1000), new Player("Nige", 1000) };

            var niuniu = new Game(players);
            while (niuniu.IsInProgress)
            {
                niuniu.PlayRound();
            }

            Assert.AreEqual(4000, players.Sum(x => x.Money));
        }
    }
}
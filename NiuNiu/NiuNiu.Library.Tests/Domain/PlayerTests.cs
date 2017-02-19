using Moq;
using NiuNiu.Library.Domain;
using NiuNiu.Library.Gambling;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Domain
{
    [TestFixture]
    public class PlayerTests
    {
        [TestCase(100, 120, ExpectedResult = false)]
        [TestCase(110, 90, ExpectedResult = true)]
        [TestCase(160, 120, ExpectedResult = true)]
        [TestCase(100, 120, ExpectedResult = false)]
        public bool PlayerShouldTakePotAtValue(int potValue, int valueWhenShouldTake)
        {
            var pot = new Pot();

            pot.ReceiveMoney(potValue);

            Player player = CreatePlayerForTaking(1000, valueWhenShouldTake);
            return player.ShouldTakePot(pot.Value);
        }

        private static Player CreatePlayer(int startingMoney)
        {
            var gamblingStrategy = new Mock<IGamblingStrategy>();
            return new Player("Steve", startingMoney, gamblingStrategy.Object);
        }

        private static Player CreatePlayerForBetting(int startingMoney, int betAmount)
        {
            var gamblingStrategy = new Mock<IGamblingStrategy>();
            gamblingStrategy.Setup(strategy => strategy.CurrentBet).Returns(betAmount);
            return new Player("Steve", startingMoney, gamblingStrategy.Object);
        }

        private static Player CreatePlayerForTaking(int startingMoney, int valueWhenShouldTake)
        {
            var gamblingStrategy = new Mock<IGamblingStrategy>();
            gamblingStrategy.Setup(strategy => strategy.ShouldTakePot(It.IsInRange(valueWhenShouldTake, int.MaxValue, Range.Inclusive))).Returns(true);
            return new Player("Steve", startingMoney, gamblingStrategy.Object);
        }

        [Test]
        public void PlacingABetAddsMoneyToPot()
        {
            const int betAmount = 100;

            var pot = new Pot();

            Player player = CreatePlayerForBetting(1000, betAmount);
            player.PlaceBet(pot);

            Assert.AreEqual(pot.Value, betAmount);
        }

        [Test]
        public void PlacingABetRemovesMoneyFromPlayer()
        {
            const int betAmount = 100;
            const int startingMoney = 1000;
            const int expectedAmount = startingMoney - betAmount;

            var pot = new Pot();

            Player player = CreatePlayerForBetting(1000, betAmount);
            player.PlaceBet(pot);

            Assert.AreEqual(player.Money, expectedAmount);
        }

        [Test]
        public void ReceivingMoneyAddsToPlayerMoney()
        {
            const int startingMoney = 1000;
            const int moneyToReceive = 120;
            const int expectedAmount = startingMoney + moneyToReceive;

            Player player = CreatePlayer(startingMoney);
            player.ReceiveMoney(moneyToReceive);

            Assert.AreEqual(player.Money, expectedAmount);
        }
    }
}
using Moq;
using NiuNiu.Library.Domain;
using NiuNiu.Library.Gambling;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Gambling
{
    [TestFixture]
    public sealed class StandardPayoutTests
    {
        [Test]
        public void UltimateHandTest()
        {
            const int lastBet = 1;
            const int expectedPayout = 6;
            var highCardMock = new Mock<IPayoutValue>();
            highCardMock.Setup(x => x.HasTriple).Returns(true);
            highCardMock.Setup(x => x.IsUltimate).Returns(true);

            PayoutTest(highCardMock, lastBet, expectedPayout);
        }

        [Test]
        public void NiuNiuHandTest()
        {
            const int lastBet = 1;
            const int expectedPayout = 4;
            var highCardMock = new Mock<IPayoutValue>();
            highCardMock.Setup(x => x.HasTriple).Returns(true);
            highCardMock.Setup(x => x.IsUltimate).Returns(false);
            highCardMock.Setup(x => x.HighestCardFace).Returns(Face.Queen);

            PayoutTest(highCardMock, lastBet, expectedPayout);
        }

        [Test]
        public void BigPointHandTest()
        {
            const int lastBet = 1;
            const int expectedPayout = 3;
            var highCardMock = new Mock<IPayoutValue>();
            highCardMock.Setup(x => x.HasTriple).Returns(true);
            highCardMock.Setup(x => x.IsUltimate).Returns(false);
            highCardMock.Setup(x => x.HighestCardFace).Returns(Face.Eight);

            PayoutTest(highCardMock, lastBet, expectedPayout);
        }
        
        [Test]
        public void TripleHandTest()
        {
            const int lastBet = 1;
            const int expectedPayout = 2;
            var highCardMock = new Mock<IPayoutValue>();
            highCardMock.Setup(x => x.HasTriple).Returns(true);
            highCardMock.Setup(x => x.IsUltimate).Returns(false);
            highCardMock.Setup(x => x.HighestCardFace).Returns(Face.Five);

            PayoutTest(highCardMock, lastBet, expectedPayout);
        }

        [Test]
        public void HighCardTest()
        {
            const int lastBet = 1;
            const int expectedPayout = 1;
            var highCardMock = new Mock<IPayoutValue>();
            highCardMock.Setup(x => x.HasTriple).Returns(false);

            PayoutTest(highCardMock, lastBet, expectedPayout);
        }

        private static void PayoutTest(IMock<IPayoutValue> highCardMock, int lastBet, int expectedPayout)
        {
            var payout = new StandardPayout();

            var giverMock = new Mock<IMoneyGiver>();
            var receiverMock = new Mock<IMoneyReceiver>();
            
            payout.Payout(highCardMock.Object, lastBet, giverMock.Object, receiverMock.Object);

            giverMock.Verify(x => x.GiveMoney(receiverMock.Object, expectedPayout));
        }
    }
}
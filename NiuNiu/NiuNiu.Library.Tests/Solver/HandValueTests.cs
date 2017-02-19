using NiuNiu.Library.Domain;
using NiuNiu.Library.Solver;
using NiuNiu.Library.Tests.Domain;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Solver
{
    [TestFixture]
    public class HandValueTests
    {
        [Test]
        public void AllTensShouldHaveResultOf10()
        {
            Hand hand = CardCollectionHelper.AllRoyalsHand;

            var niuNiuResult = new HandValue(hand, CardCollectionHelper.AllRoyalsTriple);

            Assert.AreEqual(Face.King, niuNiuResult.HighestCardFace);
        }
    }
}
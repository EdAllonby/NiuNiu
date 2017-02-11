using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class NiuNiuSolverTests
    {
        [Test]
        public void NumbersShouldMakeModulus10()
        {
            Hand hand = CardCollectionHelper.AllRoyalsHand;

            var solver = new HandSolver();
            HandValue values = solver.Solve(hand);
        }
    }
}
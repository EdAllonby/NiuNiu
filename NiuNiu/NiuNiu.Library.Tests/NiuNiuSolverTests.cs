using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class NiuNiuSolverTests
    {
        [Test]
        public void NumbersShouldMakeModulus10()
        {
            var hand = CardCollectionHelper.AllRoyalsHand;
            
            var solver = new NiuNiuSolver();
            var results = solver.Solve(hand);
        }
    }
}
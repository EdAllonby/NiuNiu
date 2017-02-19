using NiuNiu.Library.Domain;
using NiuNiu.Library.Solver;
using NiuNiu.Library.Tests.Domain;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Solver
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
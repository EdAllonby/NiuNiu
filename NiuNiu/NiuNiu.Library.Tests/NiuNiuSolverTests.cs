
using System.Collections.Generic;
using NUnit.Framework;

namespace NiuNiu.Library.Tests
{
    [TestFixture]
    public class NiuNiuSolverTests
    {
        [Test]
        public void NumbersShouldMakeModulus10()
        {
            var numbers = new List<int> { 3, 10, 7, 3, 4, 1, 9};
            var solver = new NiuNiuSolver();
            var test = solver.Solve(numbers);
        }
    }
}
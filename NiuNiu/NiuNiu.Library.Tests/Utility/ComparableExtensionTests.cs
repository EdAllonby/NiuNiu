using NiuNiu.Library.Utility;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Utility
{
    [TestFixture]
    public class ComparableExtensionTests
    {
        [TestCase(1, 1, 3, ExpectedResult = 1)]
        [TestCase(-1, 1, 3, ExpectedResult = 1)]
        [TestCase(5, 1, 3, ExpectedResult = 3)]
        [TestCase(2, 1, 3, ExpectedResult = 2)]
        public int ClampTests(int val, int min, int max)
        {
            return val.Clamp(min, max);
        }
    }
}
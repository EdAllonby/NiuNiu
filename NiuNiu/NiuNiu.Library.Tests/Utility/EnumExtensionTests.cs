using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NiuNiu.Library.Utility;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Utility
{
    [TestFixture]
    public class EnumExtensionTests
    {
        private enum TestEnum
        {
            [UsedImplicitly] First,
            [UsedImplicitly] Second,
            [UsedImplicitly] Third
        }

        [Test]
        public void EnumeratingAnEnumShouldHaveAllValues()
        {
            List<TestEnum> testEnums = EnumExtensions.GetValues<TestEnum>().ToList();
            Assert.That(testEnums, Has.Count.EqualTo(3));
        }
    }
}
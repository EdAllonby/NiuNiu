using System;
using System.Collections.Generic;
using NiuNiu.Library.Utility;
using NUnit.Framework;

namespace NiuNiu.Library.Tests.Utility
{
    [TestFixture]
    public class ListExtensionTests
    {
        [Test]
        public void ReplaceElementInListShouldReplace()
        {
            var list = new List<int> { 1, 2, 3, 4 };
            var expectedList = new List<int> { 1, 3, 3, 4 };

            list.ReplaceFirstOccurrence(2, 3);

            CollectionAssert.AreEqual(expectedList, list);
        }

        [Test]
        public void ReplaceElementOnlyReplacesFirstOccurrence()
        {
            var list = new List<int> { 1, 2, 2, 4 };
            var expectedList = new List<int> { 1, 3, 2, 4 };

            list.ReplaceFirstOccurrence(2, 3);

            CollectionAssert.AreEqual(expectedList, list);
        }

        [Test]
        public void ThrowArgumentNullExceptionIfListIsNull()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ListExtensions.ReplaceFirstOccurrence(null, 1, 2));
        }
    }
}
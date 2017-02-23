using System;
using System.Collections.Generic;
using System.Linq;
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
        public void ItemInPickRandomReturnsItemInList()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            int item = list.PickRandom();
            Assert.Contains(item, list);
        }

        [TestCase(1, 2, 1, ExpectedResult = 2)]
        [TestCase(1, 2, 2, ExpectedResult = 1)]
        public int NextInLoopGetsNextItem(int firstItem, int secondItem, int current)
        {
            var list = new List<int> { firstItem, secondItem };
            return list.NextInLoop(current);
        }

        [TestCase(1, 2, 1, ExpectedResult = 2)]
        [TestCase(1, 2, 2, ExpectedResult = 1)]
        public int ElementAtLoopedIndexGetsNextItem(int firstItem, int secondItem, int current)
        {
            var list = new List<int> { firstItem, secondItem };
            return list.ElementAtLoopedIndex(current);
        }

        [Test]
        public void CycleKeepsGoingAfterListEnd()
        {
            const int totalPasses = 7;
            var list = new List<int> { 1, 2, 3, 4 };
            var expectedList = new List<int> { 3, 4, 1, 2, 3, 4, 1 };
            List<int> actualList = list.CycleTo(3, totalPasses).ToList();

            Assert.That(actualList, Has.Count.EqualTo(totalPasses));
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [Test]
        public void ThrowArgumentNullExceptionIfListInReplaceIsNull()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ListExtensions.ReplaceFirstOccurrence(null, 1, 2));
        }
    }
}
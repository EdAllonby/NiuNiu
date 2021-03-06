using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NiuNiu.Library.Utility
{
    /// <summary>
    /// Extend Enumerable classes.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Replaces a value in a List with a new one.
        /// </summary>
        /// <typeparam name="TElement">The type of list.</typeparam>
        /// <param name="source">The list to replace a value.</param>
        /// <param name="oldValue">The old value to replace.</param>
        /// <param name="newValue">The replacement value.</param>
        public static void ReplaceFirstOccurrence<TElement>([NotNull] this IList<TElement> source, TElement oldValue, TElement newValue)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int index = source.IndexOf(oldValue);
            if (index != -1)
            {
                source[index] = newValue;
            }
        }

        /// <summary>
        /// Pick a random element from an enumerable.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TElement PickRandom<TElement>(this IEnumerable<TElement> source)
        {
            return source.PickRandom(1).Single();
        }

        /// <summary>
        /// Continuously repeat over a collection.
        /// </summary>
        /// <typeparam name="TElement">The element type to cycle.</typeparam>
        /// <param name="list">The collection to cycle.</param>
        /// <param name="startingElement">Which element in the collection to start the cycle at.</param>
        /// <param name="passes">The maximum elements to enumerate.</param>
        /// <returns>An enumerable that will cycle.</returns>
        public static IEnumerable<TElement> CycleTo<TElement>(this IList<TElement> list, TElement startingElement, int passes)
        {
            yield return startingElement;

            var currentYields = 1;

            while (currentYields < passes)
            {
                startingElement = NextInLoop(list, startingElement);
                yield return startingElement;
                currentYields++;
            }
        }

        /// <summary>
        /// Find the next value in a collection.
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="list">The loop.</param>
        /// <param name="current">The current element in the loop.</param>
        /// <returns>The next element in the loop.</returns>
        public static TElement NextInLoop<TElement>(this IList<TElement> list, TElement current)
        {
            int index = list.IndexOf(current);
            int nextIndex = (index + 1) % list.Count;
            return list[nextIndex];
        }

        /// <summary>
        /// Find an element in a loop at an index (which may be higher than the length of the loop).
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="list">The loop.</param>
        /// <param name="index">The index for the loop.</param>
        /// <returns>The element at the index in the loop.</returns>
        public static TElement ElementAtLoopedIndex<TElement>(this IList<TElement> list, int index)
        {
            int loopedIndex = index % list.Count;
            return list[loopedIndex];
        }

        private static IEnumerable<TElement> PickRandom<TElement>(this IEnumerable<TElement> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        private static IEnumerable<TElement> Shuffle<TElement>(this IEnumerable<TElement> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// Extend Enumerable classes.
    /// </summary>
    public static class ListExtensions
    {
        public static int Replace<TElement>(this IList<TElement> source, TElement oldValue, TElement newValue)
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

            return index;
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
        /// <returns>An enumerable that will cycle.</returns>
        public static IEnumerable<TElement> Cycle<TElement>(this IList<TElement> list, TElement startingElement)
        {
            while (true)
            {
                startingElement = NextInLoop(list, startingElement);
                yield return startingElement;
            }
            // This enumerates an ienumerable.
            // ReSharper disable once IteratorNeverReturns
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
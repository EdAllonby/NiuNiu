using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> Cycle<T>(this List<T> list, T startingElement)
        {
            while (true)
            {
                startingElement = NextInLoop(list, startingElement);
                yield return startingElement;
            }
        }
        
        public static T NextInLoop<T>(this List<T> list, T current)
        {
            int index = list.IndexOf(current);
            int nextIndex = (index + 1) % list.Count;
            return list[nextIndex];
        }

        public static T ElementAtLoopedIndex<T>(this List<T> list, int index)
        {
            int loopedIndex = index % list.Count;
            return list[loopedIndex];
        }

        private static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// A shuffle implementation which orders a collection using a new guid.
    /// </summary>
    public sealed class GuidShuffle : IShuffler
    {
        /// <summary>
        /// Shuffle using a guid. This is good for cards because there are 52! permutations. This means a standard Random()
        /// function isn't fully fit for purpose.
        /// </summary>
        /// <typeparam name="TElement">The item type that will be shuffled.</typeparam>
        /// <param name="collection">The collection of items to shuffle.</param>
        public void Shuffle<TElement>(List<TElement> collection)
        {
            List<TElement> shuffledCollection = collection.OrderBy(a => Guid.NewGuid()).ToList();
            collection.Clear();
            collection.AddRange(shuffledCollection);
        }
    }
}
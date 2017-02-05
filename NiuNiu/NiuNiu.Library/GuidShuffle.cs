using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    public sealed class GuidShuffle : IShuffler
    {

        public void Shuffle<T>(List<T> collection)
        {
            IOrderedEnumerable<T> shuffledCollection = collection.OrderBy(a => Guid.NewGuid());
            collection.Clear();
            collection.AddRange(shuffledCollection);
        }
    }
}
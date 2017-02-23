using System.Collections.Generic;

namespace NiuNiu.Library.Utility
{

    /// <summary>
    /// A shuffler for a list.
    /// </summary>
    public interface IShuffler
    {
        /// <summary>
        /// Shuffler a list in-place.
        /// </summary>
        /// <typeparam name="T">The type of list.</typeparam>
        /// <param name="list">The list to shuffle in-place.</param>
        void Shuffle<T>(List<T> list);
    }
}
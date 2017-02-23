using System;
using System.Collections.Generic;

namespace NiuNiu.Library.Utility
{
    /// <summary>
    /// Taken from https://www.dotnetperls.com/fisher-yates-shuffle
    /// </summary>
    public sealed class FischerYatesShuffle : IShuffler
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Shuffle the list.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="array">Array to shuffle.</param>
        public void Shuffle<T>(List<T> array)
        {
            int n = array.Count;
            for (var index = 0; index < n; index++)
            {
                // NextDouble returns a random number between 0 and 1.
                int randomClampedNumber = index + (int) (Random.NextDouble() * (n - index));
                T element = array[randomClampedNumber];
                array[randomClampedNumber] = array[index];
                array[index] = element;
            }
        }
    }
}
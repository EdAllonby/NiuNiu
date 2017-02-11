using System;

namespace NiuNiu.Library
{
    /// <summary>
    /// Extensions for comparing items.
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Clamp an item between a defined minimum and maximum value.
        /// </summary>
        /// <typeparam name="T">The item to clamp.</typeparam>
        /// <param name="val">The item.</param>
        /// <param name="min">The minimum this item can be.</param>
        /// <param name="max">The maximum this item can be.</param>
        /// <returns>A clamped value between the minimum and maximum.</returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            if (val.CompareTo(max) > 0) return max;
            return val;
        }
    }
}
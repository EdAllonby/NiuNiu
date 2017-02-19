using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library.Utility
{
    /// <summary>
    /// Extends the enum type.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Enumerate all of the values in an enum.
        /// </summary>
        /// <typeparam name="TEnumElement">Items in the enum.</typeparam>
        /// <returns>The enum values</returns>
        public static IEnumerable<TEnumElement> GetValues<TEnumElement>() where TEnumElement : IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnumElement)).Cast<TEnumElement>();
        }
    }
}
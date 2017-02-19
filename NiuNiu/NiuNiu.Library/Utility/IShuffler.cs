using System.Collections.Generic;

namespace NiuNiu.Library.Utility
{
    public interface IShuffler
    {
        void Shuffle<T>(List<T> array);
    }
}
using System.Collections.Generic;

namespace NiuNiu.Library
{
    public interface IShuffler
    {
        void Shuffle<T>(List<T> array);
    }
}
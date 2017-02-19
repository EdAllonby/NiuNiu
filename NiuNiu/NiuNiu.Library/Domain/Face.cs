using JetBrains.Annotations;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// The face value of a playing card.
    /// </summary>
    public enum Face
    {
        [UsedImplicitly] Ace = 1,
        [UsedImplicitly] Two,
        [UsedImplicitly] Three,
        [UsedImplicitly] Four,
        [UsedImplicitly] Five,
        [UsedImplicitly] Six,
        [UsedImplicitly] Seven,
        [UsedImplicitly] Eight,
        [UsedImplicitly] Nine,
        [UsedImplicitly] Ten,
        [UsedImplicitly] Jack,
        [UsedImplicitly] Queen,
        [UsedImplicitly] King
    }
}
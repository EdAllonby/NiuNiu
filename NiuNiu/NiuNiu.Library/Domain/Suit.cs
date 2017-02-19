using JetBrains.Annotations;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// A suit is one of several categories into which the cards of a deck are divided.
    /// As suit is important in NiuNiu, this enum orders suit from worst value to best (Spades).
    /// </summary>
    public enum Suit
    {
        [UsedImplicitly] Diamonds,
        [UsedImplicitly] Clubs,
        [UsedImplicitly] Hearts,
        [UsedImplicitly] Spades
    }
}
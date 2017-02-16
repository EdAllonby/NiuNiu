namespace NiuNiu.Library
{
    /// <summary>
    /// For NiuNiu, we can determine how much to pay with a few patterns.
    /// This encapsulates those patterns to determine a payout.
    /// </summary>
    public interface IPayoutValue
    {
        /// <summary>
        /// The highest card in the value.
        /// </summary>
        Face HighestCardFace { get; }

        /// <summary>
        /// If the value is 'ultimate'.
        /// </summary>
        bool IsUltimate { get; }

        /// <summary>
        /// If the value has a triple.
        /// </summary>
        bool HasTriple { get; }
    }
}
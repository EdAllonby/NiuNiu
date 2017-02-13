namespace NiuNiu.Library
{
    /// <summary>
    /// A type of gamlbing strategy to use.
    /// </summary>
    public interface IGamblingStrategy
    {
        /// <summary>
        /// How much to bet.
        /// </summary>
        int CurrentBet { get; }

        /// <summary>
        /// Should the current pot value be taken?
        /// </summary>
        /// <param name="value">The value of the pot.</param>
        /// <returns>If it should be taken.</returns>
        bool ShouldTakePot(int value);
    }
}
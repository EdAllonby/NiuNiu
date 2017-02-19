namespace NiuNiu.Library.Gambling
{
    /// <summary>
    /// A simple implementation of a gambling strategy with no real logic.
    /// </summary>
    public class DefaultGamblingStrategy : IGamblingStrategy
    {
        /// <summary>
        /// The bet value.
        /// </summary>
        public int CurrentBet => 20;

        /// <summary>
        /// Whether the current pot value should be taken..
        /// </summary>
        /// <param name="potValue">The value of the pot.</param>
        /// <returns>Whether the pot value should be taken at this value.</returns>
        public bool ShouldTakePot(int potValue)
        {
            return potValue > 100;
        }
    }
}
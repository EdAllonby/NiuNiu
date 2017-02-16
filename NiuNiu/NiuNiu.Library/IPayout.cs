namespace NiuNiu.Library
{
    /// <summary>
    /// How to pay participants in a game.
    /// </summary>
    public interface IPayout
    {
        /// <summary>
        /// Payout for the hand value given.
        /// </summary>
        /// <param name="handValue">The value of the hand.</param>
        /// <param name="lastBet">The last bet to be taken by the player.</param>
        /// <param name="giver">The participant to give money for this payout.</param>
        /// <param name="receiver">The participant to receive money for this payout.</param>
        void Payout(IPayoutValue handValue, int lastBet, IMoneyGiver giver, IMoneyReceiver receiver);
    }
}
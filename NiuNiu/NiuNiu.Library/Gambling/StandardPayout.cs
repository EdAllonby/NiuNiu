using NiuNiu.Library.Domain;

namespace NiuNiu.Library.Gambling
{
    /// <summary>
    /// A standard payout implementation for NiuNiu.
    /// </summary>
    public sealed class StandardPayout : IPayout
    {
        /// <summary>
        /// Payout the current round using the standard NiuNiu rules.
        /// </summary>
        /// <param name="handValue">The value of the hand.</param>
        /// <param name="lastBet">The last bet to be taken by the player.</param>
        /// <param name="giver">The participant to give money for this payout.</param>
        /// <param name="receiver">The participant to receive money for this payout.</param>
        public void Payout(IPayoutValue handValue, int lastBet, IMoneyGiver giver, IMoneyReceiver receiver)
        {
            int payoutMultiplier = GetPayoutMultiplier(handValue);
            giver.GiveMoney(receiver, lastBet * payoutMultiplier);
        }

        private static int GetPayoutMultiplier(IPayoutValue handValue)
        {
            var multiplier = 1;

            if (IsHighCard(handValue))
            {
                return multiplier;
            }
            if (IsUltimate(handValue))
            {
                multiplier = 5;
            }
            if (IsNiuNiu(handValue))
            {
                multiplier = 3;
            }
            if (IsBigPoint(handValue))
            {
                multiplier = 2;
            }

            return multiplier;
        }

        private static bool IsBigPoint(IPayoutValue handValue)
        {
            return handValue.HighestCardFace >= Face.Seven && handValue.HighestCardFace <= Face.Nine;
        }

        private static bool IsNiuNiu(IPayoutValue handValue)
        {
            return handValue.HighestCardFace >= Face.Ten && handValue.HighestCardFace <= Face.King;
        }

        private static bool IsHighCard(IPayoutValue handValue)
        {
            return !handValue.HasTriple;
        }

        private static bool IsUltimate(IPayoutValue handValue)
        {
            return handValue.IsUltimate;
        }
    }
}
namespace NiuNiu.Library
{
    /// <summary>
    /// The pot refers to the sum of money that players wager during a round of NiuNiu.
    /// </summary>
    public class Pot : IMoneyReceiver, IMoneyGiver
    {
        private readonly Bank pot = new Bank();

        /// <summary>
        /// Is there any money in this pot?
        /// </summary>
        public bool HasMoney => pot.Balance > 0;

        /// <summary>
        /// The current value of the pot.
        /// </summary>
        public int Value => pot.Balance;

        /// <summary>
        /// Take money from the pot and give to an <see cref="IMoneyReceiver" />.
        /// </summary>
        /// <param name="receiver">The receiver of pot money.</param>
        /// <param name="amount">The amount to give to the <see cref="IMoneyReceiver" />.</param>
        public void GiveMoney(IMoneyReceiver receiver, int amount)
        {
            pot.Withdraw(receiver, amount);
        }

        /// <summary>
        /// Add bet to the pot.
        /// </summary>
        /// <param name="bet">The amount to add to the pot.</param>
        public void ReceiveMoney(int bet)
        {
            pot.Deposit(bet);
        }

        /// <summary>
        /// Give whole pot to the dealer.
        /// </summary>
        /// <param name="dealer">The dealer to give pot amount to.</param>
        public void GiveToDealer(Dealer dealer)
        {
            GiveMoney(dealer.Player, pot.Balance);
        }
    }
}
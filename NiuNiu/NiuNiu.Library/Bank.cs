namespace NiuNiu.Library
{
    /// <summary>
    /// A bank is able to store, give and receive money.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// Create a new bank with an initial amount of money.
        /// </summary>
        /// <param name="balance"></param>
        public Bank(int balance)
        {
            Balance = balance;
        }

        /// <summary>
        /// Create a bank with no initial value.
        /// </summary>
        public Bank() : this(0)
        {
        }

        /// <summary>
        /// The current balance of the bank.
        /// </summary>
        public int Balance { get; private set; }

        /// <summary>
        /// Withdraw some money from the bank.
        /// </summary>
        /// <param name="receiver">The person to receive the money.</param>
        /// <param name="amountToWithdraw">The amount to withdraw from the bank.</param>
        public void Withdraw(IMoneyReceiver receiver, int amountToWithdraw)
        {
            int amountAvailable = amountToWithdraw.Clamp(0, Balance);

            receiver.ReceiveMoney(amountAvailable);
            Balance -= amountAvailable;
        }

        /// <summary>
        /// Deposit some money into the bank.
        /// </summary>
        /// <param name="amountToReceive"></param>
        public void Deposit(int amountToReceive)
        {
            Balance += amountToReceive;
        }
    }
}
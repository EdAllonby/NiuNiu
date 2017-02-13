namespace NiuNiu.Library
{
    public class Pot : IMoneyReceiver
    {
        private readonly Bank pot = new Bank();

        public bool HasMoney => pot.Balance > 0;

        public int Value => pot.Balance;

        public void ReceiveMoney(int bet)
        {
            pot.Deposit(bet);
        }

        public void GiveToDealer(Dealer dealer)
        {
            GiveMoney(dealer.Player, pot.Balance);
        }

        public void GiveMoney(IMoneyReceiver player, int amount)
        {
            pot.Withdraw(player, amount);
        }
    }
}
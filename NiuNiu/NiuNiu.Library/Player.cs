namespace NiuNiu.Library
{
    public class Player
    {
        NiuNiuSolver brain = new NiuNiuSolver();

        private readonly Hand hand = new Hand();

        public Player(int money)
        {
            Money = money;
        }

        public int Money { get; }

        public void CalculateHandValue()
        {
        }

        public void ReceiveCard(Card card)
        {
            hand.AddCard(card);
        }
    }
}
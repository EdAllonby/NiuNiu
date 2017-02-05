namespace NiuNiu.Library
{
    public class Player
    {
        private readonly NiuNiuSolver brain = new NiuNiuSolver();

        private readonly Hand hand = new Hand();

        public Player(int money)
        {
            Money = money;
        }

        public int Money { get; }

        public NiuNiuResult CalculateHandValue()
        {
            return brain.Solve(hand);
        }

        public void ReceiveCard(Card card)
        {
            hand.AddCard(card);
        }
    }
}
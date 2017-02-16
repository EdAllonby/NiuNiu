using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// A player of NiuNiu.
    /// </summary>
    public class Player : IMoneyReceiver, IMoneyGiver
    {
        private readonly HandSolver brain = new HandSolver();

        private readonly IGamblingStrategy gamblingStrategy = new DefaultGamblingStrategy();

        private readonly Hand hand = new Hand();

        /// <summary>
        /// Creates a new NiuNiu player with some money.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="money"></param>
        public Player(string name, int money)
        {
            Name = name;
            Bank = new Bank(money);
        }

        public string Name { get; set; }

        public int LastBet { get; private set; }

        /// <summary>
        /// The money the player has.
        /// </summary>
        public Bank Bank { get; }

        /// <summary>
        /// The value of the current player's hand..
        /// </summary>
        public HandValue HandValue => brain.Solve(hand);

        /// <summary>
        /// How much money the player has.
        /// </summary>
        public int Money => Bank.Balance;

        /// <summary>
        /// Show the current hand of the player.
        /// </summary>
        public IEnumerable<Card> ShowHand => hand.Cards;

        /// <summary>
        /// Give money to a receiver.
        /// </summary>
        /// <param name="receiver">The receiver to give money to.</param>
        /// <param name="amount">The amount to give.</param>
        public void GiveMoney(IMoneyReceiver receiver, int amount)
        {
            Bank.Withdraw(receiver, amount);
        }

        /// <summary>
        /// Receive money.
        /// </summary>
        /// <param name="amount">The amount to receive.</param>
        public void ReceiveMoney(int amount)
        {
            Bank.Deposit(amount);
        }

        /// <summary>
        /// Receive a card.
        /// </summary>
        /// <param name="card">The card received.</param>
        public void ReceiveCard(Card card)
        {
            hand.AddCard(card);
        }

        public bool ShouldTakePot(int potValue)
        {
            return gamblingStrategy.ShouldTakePot(potValue);
        }

        public void PlaceBet(Pot pot)
        {
            int currentBet = gamblingStrategy.CurrentBet;
            LastBet = currentBet;
            GiveMoney(pot, currentBet);
        }

        /// <summary>
        /// Give back all of the cards in the player's hand.
        /// </summary>
        /// <returns>The cards to give back.</returns>
        public IEnumerable<Card> ReturnAllCardsInHand()
        {
            List<Card> playerHand = hand.Cards.ToList();
            hand.EmptyHand();
            return playerHand;
        }

        public override string ToString()
        {
            return $"{Name} has {Bank.Balance} money available in this game.";
        }
    }
}
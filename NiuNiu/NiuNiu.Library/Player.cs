using System.Collections.Generic;
using System.Linq;

namespace NiuNiu.Library
{
    /// <summary>
    /// A player of NiuNiu.
    /// </summary>
    public class Player : IMoneyReceiver
    {
        private readonly HandSolver brain = new HandSolver();

        private readonly Hand hand = new Hand();

        /// <summary>
        /// Creates a new NiuNiu player with some money.
        /// </summary>
        /// <param name="money"></param>
        public Player(int money)
        {
            Bank = new Bank(money);
        }

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
        /// Receive money.
        /// </summary>
        /// <param name="amount"></param>
        public void ReceiveMoney(int amount)
        {
            Bank.Deposit(amount);
        }

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
        /// Receive a card.
        /// </summary>
        /// <param name="card">The card received.</param>
        public void ReceiveCard(Card card)
        {
            hand.AddCard(card);
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
            return $"Player has {Bank.Balance} money available in this game.";
        }
    }
}
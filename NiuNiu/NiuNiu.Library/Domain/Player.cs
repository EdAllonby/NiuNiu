using System;
using System.Collections.Generic;
using System.Linq;
using NiuNiu.Library.Gambling;
using NiuNiu.Library.Solver;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// A player of NiuNiu.
    /// </summary>
    public class Player : ICardHandler, IMoneyReceiver, IMoneyGiver, IEquatable<Player>
    {
        private static int nextId = 1;
        private readonly HandSolver brain = new HandSolver();

        private readonly IGamblingStrategy gamblingStrategy;

        private readonly Hand hand = new Hand();

        /// <summary>
        /// Creates a new NiuNiu player with some money.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="money">The initial money this player has.</param>
        /// <param name="strategy">The gambling strategy this player should use.</param>
        public Player(string name, int money, IGamblingStrategy strategy)
        {
            Id = nextId;
            nextId++;
            Name = name;
            Bank = new Bank(money);
            gamblingStrategy = strategy;
        }

        /// <summary>
        /// Copy a player, which preserves the original Id for equality.
        /// </summary>
        /// <param name="player">The player to copy.</param>
        protected Player(Player player)
        {
            Id = player.Id;
            Name = player.Name;
            Bank = player.Bank;
            gamblingStrategy = player.gamblingStrategy;
        }

        /// <summary>
        /// Internally used to find equality in player entities.
        /// </summary>
        private int Id { get; }

        private string Name { get; }

        public int LastBet { get; private set; }

        /// <summary>
        /// The money the player has.
        /// </summary>
        private Bank Bank { get; }

        /// <summary>
        /// The value of the current player's hand..
        /// </summary>
        public HandValue HandValue => brain.Solve(hand);

        /// <summary>
        /// How much money the player has.
        /// </summary>
        public int Money => Bank.Balance;

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
        public IEnumerable<Card> ReturnCards()
        {
            List<Card> playerHand = hand.Cards.ToList();
            hand.EmptyHand();
            return playerHand;
        }

        public bool Equals(Player other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
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
        /// Receive money.
        /// </summary>
        /// <param name="amount">The amount to receive.</param>
        public void ReceiveMoney(int amount)
        {
            Bank.Deposit(amount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// If the player should take the pot at its current value.
        /// </summary>
        /// <param name="potValue">The current value of the pot.</param>
        /// <returns>If the player should take the pot at its current value.</returns>
        public bool ShouldTakePot(int potValue)
        {
            return gamblingStrategy.ShouldTakePot(potValue);
        }

        /// <summary>
        /// Place a bet in the current pot.
        /// </summary>
        /// <param name="pot">The pot to place a bet.</param>
        public void PlaceBet(Pot pot)
        {
            int currentBet = gamblingStrategy.CurrentBet;
            LastBet = currentBet;
            GiveMoney(pot, currentBet);
        }

        public override string ToString()
        {
            return $"{Name} has {Bank.Balance} money available in this game.";
        }
    }
}
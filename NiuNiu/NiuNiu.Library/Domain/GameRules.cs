namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// A place to store rules of the game.
    /// </summary>
    public static class GameRules
    {
        /// <summary>
        /// Maximum players allowed to enter the game at one time.
        /// </summary>
        public const int TotalPlayers = 4;

        /// <summary>
        /// The amount of cards to deal per player's hand.
        /// </summary>
        public const int CardsPerHand = 5;

        /// <summary>
        /// The size of a pot.
        /// </summary>
        public const int PotSize = 100;
    }
}
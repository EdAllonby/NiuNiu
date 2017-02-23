using System.Collections.Generic;

namespace NiuNiu.Library.Domain
{
    /// <summary>
    /// The ability to receive and return cards.
    /// </summary>
    public interface ICardHandler
    {
        /// <summary>
        /// Receive a card.
        /// </summary>
        /// <param name="card"></param>
        void ReceiveCard(Card card);

        /// <summary>
        /// Return all cards.
        /// </summary>
        /// <returns>The returned cards.</returns>
        IEnumerable<Card> ReturnCards();
    }
}
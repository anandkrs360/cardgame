using System.Collections.Generic;
using CardGame.Models;

namespace CardGame.Contracts
{
    /// <summary>
    /// The Base Deck.
    /// </summary>
    internal abstract class BaseDeck
    {
        /// <summary>
        /// Gets Collection of Cards
        /// </summary>
        public abstract List<Card> Cards { get; }

        /// <summary>
        /// Create Deck.
        /// </summary>
        /// <param name="deckSize">
        /// The Deck Size.
        /// </param>
        /// <param name="suit">
        /// The suit.
        /// </param>
        public abstract void CreateDeck(int deckSize, string[] suit = null);

        /// <summary>
        /// Shuffle Deck.
        /// </summary>
        public abstract void ShuffleDeck();
    }
}
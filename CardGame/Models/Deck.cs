using System;
using System.Collections.Generic;
using CardGame.Common;
using CardGame.Contracts;

namespace CardGame.Models
{
    /// <summary>
    /// The Deck.
    /// </summary>
    internal class Deck : BaseDeck
    {
        private readonly List<Card> _cards;

        /// <summary>
        /// Gets Collection of Cards
        /// </summary>
        public override List<Card> Cards { get { return _cards; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class.
        /// </summary>
        public Deck()
        {
            _cards = new List<Card>();
        }

        /// <summary>
        /// Create Deck.
        /// </summary>
        /// <param name="deckSize">
        /// The Deck Size.
        /// </param>
        /// <param name="suit">
        /// The suit.
        /// </param>
        public override void CreateDeck(int deckSize, string[] suit = null)
        {
            int suitSize = suit == null || (suit.Length <= 0 && suit.Length >= 4) ? 4 : suit.Length;

            int cardSize = deckSize / suitSize;

            int totalCardsInDeck = cardSize * 4;

            for (int i = 0; i < suitSize; i++)
            {
                for (int j = 1; j <= cardSize; j++)
                {
                    string suitName = suit != null && suit.Length > 0 ? suit[i] : string.Empty;
                    Card card = new Card(j, suitName);
                    _cards.Add(card);
                }
            }

            if (totalCardsInDeck < deckSize)
            {
                ConsoleExtension.WriteLine($"Discarding {deckSize - totalCardsInDeck} cards from Deck. Total Card in Deck is {totalCardsInDeck}.", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Shuffle Deck.
        /// </summary>
        public override void ShuffleDeck()
        {
            Utilities.ShuffleCards(_cards);
        }
    }
}

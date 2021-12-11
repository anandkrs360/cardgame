using CardGame.Models;
using CardGame.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGame.Tests
{
    /// <summary>
    /// Deck Tests.
    /// </summary>
    [TestClass]
    public class DeckTests
    {
        /// <summary>
        /// A new Should contain 40 Cards (Default).
        /// </summary>
        [TestMethod]
        public void New_Deck_Returns_Default_Cards()
        {
            // Arrange
            int deckSize = 40;
            Deck deck = new Deck();

            // Act
            deck.CreateDeck(deckSize);

            // Assert
            Assert.AreEqual(deckSize, deck.Cards.Count);
        }

        /// <summary>
        /// Update Deck size to 52.
        /// </summary>
        [TestMethod]
        public void Update_Deck_Size()
        {

            int deckSize = 52;
            Deck deck = new Deck();

            // Create Deck
            deck.CreateDeck(deckSize);

            // Assert Updated Size
            Assert.AreEqual(deckSize, deck.Cards.Count);
        }

        /// <summary>
        /// Update deck size discarding extra cards.
        /// </summary>
        [TestMethod]
        public void Update_Deck_Size_Discard_Extra_Cards()
        {

            int deckSize = 50;
            int expectedDeckSize = 48;
            Deck deck = new Deck();

            // Create Deck
            deck.CreateDeck(deckSize);

            // Assert Updated Size exluding extra cards
            Assert.AreEqual(expectedDeckSize, deck.Cards.Count);
        }

        /// <summary>
        /// Shuffle deck.
        /// </summary>
        [TestMethod]
        public void Deck_Shuffle_Returns_Shuffled_Cards()
        {
            int deckSize = 40;
            Deck deck = new Deck();

            // Create Deck
            deck.CreateDeck(deckSize);

            // Before Shuffle
            var orderCards = deck.Cards.DeepCopy();

            // Shuffle
            deck.ShuffleDeck();

            // After Shuffle
            var shuffledCard = deck.Cards;

            // Order of Cards should change
            CollectionAssert.AreNotEqual(orderCards, shuffledCard);

            // Count Remains Same
            Assert.AreEqual(orderCards.Count, shuffledCard.Count);
        }

    }
}

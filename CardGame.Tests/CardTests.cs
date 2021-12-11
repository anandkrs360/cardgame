using CardGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGame.Tests
{
    /// <summary>
    /// The Card Tests.
    /// </summary>
    [TestClass]
    public class CardTests
    {
        /// <summary>
        /// Create new Card without Suit.
        /// </summary>
        
        [TestMethod]
        public void New_Card_Without_Suit()
        {
            // Arrange
            int cardValue = 5;
            string suitName = null;

            // Act
            Card card = new Card(cardValue, suitName);

            // Assert
            Assert.AreEqual(cardValue, card.Value);
            Assert.AreEqual(cardValue.ToString(), card.DisplayName);
            Assert.AreEqual(suitName, card.Suit);
        }

        /// <summary>
        /// Create new Card with Suit.
        /// </summary>
        [TestMethod]
        public void New_Card_With_Suit()
        {
            // Arrange
            int cardValue = 5;
            string suitName = "Diamond";

            // Act
            Card card = new Card(cardValue, suitName);

            // Assert
            Assert.AreEqual(cardValue, card.Value);
            Assert.AreEqual(suitName + cardValue, card.DisplayName);
            Assert.AreEqual(suitName, card.Suit);
        }
    }
}

using CardGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CardGame.Tests
{
    /// <summary>
    /// Player Tests.
    /// </summary>
    [TestClass]
    public class PlayerTests
    {
        /// <summary>
        /// Test new player creation.
        /// </summary>
        [TestMethod]
        public void Player_Create_New_Player()
        {
            // Arrange
            string playerName = "Player 1";

            // Act
            Player player = new Player(playerName);

            // Assert
            Assert.AreEqual(playerName, player.Name);
            Assert.AreEqual(0, player.TotalCards);
        }

        /// <summary>
        /// Set draw pile for player.
        /// </summary>
        [TestMethod]
        public void Player_Create_Set_Draw_Pile()
        {
            // Arrange
            string playerName = "Player 1";
            Stack<Card> drawPile = new Stack<Card>();
            drawPile.Push(new Card(7, string.Empty));
            drawPile.Push(new Card(5, string.Empty));

            // Act
            Player player = new Player(playerName);
            player.DrawPile = drawPile;

            // Assert
            Assert.AreEqual(playerName, player.Name);
            Assert.AreEqual(drawPile, player.DrawPile);
            Assert.AreEqual(drawPile.Count, player.DrawPile.Count);
        }

        /// <summary>
        /// Set discard pile for player.
        /// </summary>
        [TestMethod]
        public void Player_Create_Set_Discard_Pile()
        {
            // Arrange
            string playerName = "Player 1";
            List<Card> discardPile = new List<Card>();
            discardPile.Add(new Card(7, string.Empty));
            discardPile.Add(new Card(5, string.Empty));

            // Act
            Player player = new Player(playerName);
            player.DiscardPile = discardPile;

            // Assert
            Assert.AreEqual(playerName, player.Name);
            Assert.AreEqual(discardPile, player.DiscardPile);
            Assert.AreEqual(discardPile.Count, player.DiscardPile.Count);
        }

        /// <summary>
        /// Calculate total card for player.
        /// </summary>
        [TestMethod]
        public void Player_Create_Calculate_TotalCard()
        {
            // Arrange
            string playerName = "Player 1";

            Stack<Card> drawPile = new Stack<Card>();
            drawPile.Push(new Card(7, string.Empty));
            drawPile.Push(new Card(5, string.Empty));

            List<Card> discardPile = new List<Card>();
            discardPile.Add(new Card(2, string.Empty));
            discardPile.Add(new Card(8, string.Empty));

            // Act
            Player player = new Player(playerName);
            player.DrawPile = drawPile;
            player.DiscardPile = discardPile;

            int totalCard = drawPile.Count + discardPile.Count;

            // Assert
            Assert.AreEqual(playerName, player.Name);
            Assert.AreEqual(totalCard, player.TotalCards);
        }
    }
}

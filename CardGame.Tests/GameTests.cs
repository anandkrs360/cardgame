using CardGame.Configuration;
using CardGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System;
using System.IO;

namespace CardGame.Tests
{
    /// <summary>
    /// Game Tests.
    /// </summary>
    [TestClass]
    public class GameTests
    {
        /// <summary>
        /// If a player with an empty draw pile tries to draw a card, the discard pile is shuffled into draw pile
        /// </summary>
        [TestMethod]
        public void GameStatus_Discard_Pile_Shuffled_Empty_Draw_Pile()
        {
            // Arrange
            Stack<Card> player1DrawPile = new Stack<Card>();
            List<Card> player1DiscardPile = new List<Card>();
            player1DiscardPile.Add(new Card(1, string.Empty));
            player1DiscardPile.Add(new Card(2, string.Empty));
            player1DiscardPile.Add(new Card(3, string.Empty));
            player1DiscardPile.Add(new Card(4, string.Empty));
            player1DiscardPile.Add(new Card(5, string.Empty));

            Player player1 = new Player("Player1");

            Player player2 = new Player("Player2");

            List<Player> players = new List<Player>
            {
                player1,
                player2
            };

            Setting defaultSetting = new Setting
            {
                DeckSize = 40,
                NumberOfPlayers = 2
            };
            Mock<IGameSettings> settings = new Mock<IGameSettings>();
            settings.SetupGet(x => x.GetCurrentSetting).Returns(defaultSetting);

            Game game = new Game(settings.Object);
            game.Initialize(players);

            player1.DrawPile = player1DrawPile;
            player1.DiscardPile = player1DiscardPile;

            game.GameStatus();

            Assert.AreEqual(5, player1.DrawPile.Count);
            Assert.AreEqual(0, player1.DiscardPile.Count);
        }

        /// <summary>
        /// When comparing two cards, the higher card should win.
        /// </summary>
        [TestMethod]
        public void PlayingTurn_Higher_Card_Wins()
        {
            // Arrange
            Stack<Card> player1DrawPile = new Stack<Card>();
            player1DrawPile.Push(new Card(5, string.Empty));
            player1DrawPile.Push(new Card(9, string.Empty));

            Stack<Card> player2DrawPile = new Stack<Card>();
            player2DrawPile.Push(new Card(4, string.Empty));
            player2DrawPile.Push(new Card(2, string.Empty));

            Player player1 = new Player("Player1");

            Player player2 = new Player("Player2");

            List<Player> players = new List<Player>
            {
                player1,
                player2
            };

            Setting defaultSetting = new Setting
            {
                DeckSize = 10,
                NumberOfPlayers = 2
            };
            Mock<IGameSettings> settings = new Mock<IGameSettings>();
            settings.SetupGet(x => x.GetCurrentSetting).Returns(defaultSetting);

            IGame game = new Game(settings.Object);
            game.Initialize(players);

            player1.DrawPile = player1DrawPile;
            player2.DrawPile = player2DrawPile;

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                game.PlayingTurn();

                string expected = string.Format("Player1 wins this round", Environment.NewLine);
                StringAssert.Contains(sw.ToString(), expected);
            }
        }

        /// <summary>
        /// When comparing two cards of the same value, the winner of the next round should win 4 cards.
        /// </summary>
        [TestMethod]
        public void PlayingTurn_Tie_Next_Higher_Card_Wins()
        {
            // Arrange
            Stack<Card> player1DrawPile = new Stack<Card>();
            player1DrawPile.Push(new Card(5, string.Empty));
            player1DrawPile.Push(new Card(9, string.Empty));

            Stack<Card> player2DrawPile = new Stack<Card>();
            player2DrawPile.Push(new Card(4, string.Empty));
            player2DrawPile.Push(new Card(9, string.Empty));

            Player player1 = new Player("Player1");

            Player player2 = new Player("Player2");

            List<Player> players = new List<Player>
            {
                player1,
                player2
            };

            Setting defaultSetting = new Setting
            {
                DeckSize = 10,
                NumberOfPlayers = 2
            };
            Mock<IGameSettings> settings = new Mock<IGameSettings>();
            settings.SetupGet(x => x.GetCurrentSetting).Returns(defaultSetting);

            IGame game = new Game(settings.Object);
            game.Initialize(players);

            player1.DrawPile = player1DrawPile;
            player2.DrawPile = player2DrawPile;

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                game.PlayingTurn();

                string expectedNoWinner = string.Format("No winner in this round", Environment.NewLine);
                StringAssert.Contains(sw.ToString(), expectedNoWinner);

                game.PlayingTurn();

                string expectedWinner = string.Format("Player1 wins this round", Environment.NewLine);
                StringAssert.Contains(sw.ToString(), expectedWinner);
            }
        }

        /// <summary>
        /// Test Game initialization.
        /// </summary>
        [TestMethod]
        public void Inititalize_Game()
        {
            // Arrange
            Stack<Card> player1DrawPile = new Stack<Card>();
            player1DrawPile.Push(new Card(5, string.Empty));
            player1DrawPile.Push(new Card(9, string.Empty));

            Stack<Card> player2DrawPile = new Stack<Card>();
            player2DrawPile.Push(new Card(4, string.Empty));
            player2DrawPile.Push(new Card(2, string.Empty));

            Player player1 = new Player("Player1");

            Player player2 = new Player("Player2");

            List<Player> players = new List<Player>
            {
                player1,
                player2
            };

            Setting defaultSetting = new Setting
            {
                DeckSize = 10,
                NumberOfPlayers = 2
            };
            Mock<IGameSettings> settings = new Mock<IGameSettings>();
            settings.SetupGet(x => x.GetCurrentSetting).Returns(defaultSetting);

            IGame game = new Game(settings.Object);
            bool isInitialized = game.Initialize(players);

            Assert.IsTrue(isInitialized);
        }

        /// <summary>
        /// Reset Game.
        /// </summary>
        [TestMethod]
        public void Reset_Game()
        {
            Setting defaultSetting = new Setting
            {
                DeckSize = 10,
                NumberOfPlayers = 2
            };
            Mock<IGameSettings> settings = new Mock<IGameSettings>();
            settings.SetupGet(x => x.GetCurrentSetting).Returns(defaultSetting);

            IGame game = new Game(settings.Object);
            try
            {
                game.Reset();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}

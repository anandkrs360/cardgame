using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Common;
using CardGame.Contracts;
using CardGame.Configuration;
using CardGame.Models;

namespace CardGame
{
    /// <summary>
    /// The Game class.
    /// </summary>
    internal class Game : IGame
    {
        private readonly IGameSettings _setting;

        private bool _isInitialized = false;

        private readonly BaseDeck _deck;

        private readonly List<Card> _board;

        private List<Player> _players;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="setting">
        /// The Game Setting.
        /// </param>
        public Game(IGameSettings setting)
        {
            _setting = setting;
            _deck = new Deck();
            _board = new List<Card>();
        }

        /// <summary>
        /// Initializes Game.
        /// </summary>
        /// <param name="players">
        /// The Players.
        /// </param>
        public bool Initialize(List<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            _players = players;

            // Create/Initialize Deck
            _deck.CreateDeck(_setting.GetCurrentSetting.DeckSize, _setting.GetCurrentSetting.Suit);

            // Suffle Deck
            _deck.ShuffleDeck();

            // Deal
            Deal();

            // Game initialized
            _isInitialized = true;

            return _isInitialized;
        }

        /// <summary>
        /// Playing Turn.
        /// </summary>
        public void PlayingTurn()
        {
            if (_isInitialized == false)
            {
                throw new InvalidOperationException("Game is not initalized.");
            }

            int max = int.MinValue;
            int winingPlayerIndex = -1;
            int tie = 0;
            for (int i = 0; i < _players.Count; i++)
            {
                int totalCards = _players[i].TotalCards;
                var showCard = _players[i].DrawPile.Pop();
                Console.WriteLine($"{_players[i].Name} ({totalCards} cards): {showCard.DisplayName}");
                _board.Add(showCard);

                if (showCard.Value > max)
                {
                    max = showCard.Value;
                    winingPlayerIndex = i;
                }
                else if (showCard.Value == max)
                {
                    tie = max;
                }
            }

            if (tie == max)
            {
                Console.WriteLine("No winner in this round");
                Console.WriteLine();
            }
            else
            {
                _players[winingPlayerIndex].DiscardPile.AddRange(_board);
                _board.Clear();
                Console.WriteLine($"{_players[winingPlayerIndex].Name} wins this round");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Game Status.
        /// </summary>
        public bool GameStatus()
        {
            // Check if game is intialized
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Game is not initalized.");
            }

            for (int i = _players.Count - 1; i >= 0; i--)
            {
                // Check if Draw Pile is Empty
                if (!_players[i].DrawPile.Any())
                {
                    // Check if Discard Pile is Empty
                    if (_players[i].DiscardPile.Any())
                    {
                        // If Draw Pile is empty, shuffle discard pile
                        Utilities.ShuffleCards(_players[i].DiscardPile);

                        // Move Cards from discard pile to draw pile
                        foreach (var card in _players[i].DiscardPile)
                        {
                            _players[i].DrawPile.Push(card);
                        }

                        _players[i].DiscardPile.Clear();
                    }
                    else
                    {
                        // If both draw and discard pile is empty. Player is out from game
                        ConsoleExtension.WriteLine($"{_players[i].Name} is out of the game", ConsoleColor.Red);
                        _players.RemoveAt(i);
                        Console.WriteLine();
                    }
                }
            }

            // If only one player is left in the game, the player is winner
            if (_players.Count == 1)
            {
                ConsoleExtension.WriteLine($"{_players[0].Name} wins the game!", ConsoleColor.Green);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Reset Game.
        /// </summary>
        public void Reset()
        {
            _isInitialized = false;
        }

        /// <summary>
        /// Deal Equal cards to each player.
        /// </summary>
        private void Deal()
        {
            int noOfPlayers = _players.Count;

            int cardInDeck = _deck.Cards.Count;

            int cardsPerPlayer = cardInDeck / noOfPlayers;

            int totalCards = cardsPerPlayer * noOfPlayers;

            for (int i = 0; i < noOfPlayers; i++)
            {
                for (int j = cardsPerPlayer - 1; j >= 0; j--)
                {
                    // Add Cards to player's draw pile.
                    _players[i].DrawPile.Push(_deck.Cards[j]);

                    // Remove card from deck.
                    _deck.Cards.RemoveAt(j);
                }
            }

            // If there is extra card, will not be included in game
            if (totalCards < cardInDeck)
            {
                ConsoleExtension.WriteLine($"{cardInDeck - totalCards} cards are not assigned to any player.", ConsoleColor.Red);
            }
        }
    }
}

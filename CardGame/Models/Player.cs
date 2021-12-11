using System.Collections.Generic;

namespace CardGame.Models
{
    /// <summary>
    /// The Player.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">
        /// The Player name.
        /// </param>
        public Player(string name)
        {
            Name = name;
            DrawPile = new Stack<Card>();
            DiscardPile = new List<Card>();
        }

        /// <summary>
        /// Gets the player name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or Sets draw pile.
        /// </summary>
        public Stack<Card> DrawPile { get; set; }

        /// <summary>
        /// Gets or Sets discard pile.
        /// </summary>
        public List<Card> DiscardPile { get; set; }

        /// <summary>
        /// Gets the total number of cards.
        /// </summary>
        public int TotalCards { get { return (DrawPile.Count + DiscardPile.Count); } }
    }
}

using System;

namespace CardGame.Models
{
    /// <summary>
    /// The Setting.
    /// </summary>
    internal class Setting
    {
        /// <summary>
        /// Gets or Sets number of players.
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// Gets or Sets desk size.
        /// </summary>
        public int DeckSize { get; set; }

        /// <summary>
        /// Gets or Sets suit.
        /// </summary>
        public String[] Suit { get; set; }

    }
}

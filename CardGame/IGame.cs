using CardGame.Models;
using System.Collections.Generic;

namespace CardGame
{
    /// <summary>
    /// The Game Interface.
    /// </summary>
    interface IGame
    {
        /// <summary>
        /// Initilaize Game.
        /// </summary>
        /// <param name="players">
        /// The Players
        /// </param>
        bool Initialize(List<Player> players);

        /// <summary>
        /// Playing Turn.
        /// </summary>
        void PlayingTurn();

        /// <summary>
        /// Game Status.
        /// </summary>
        bool GameStatus();

        /// <summary>
        /// Game Reset.
        /// </summary>
        void Reset();
    }
}

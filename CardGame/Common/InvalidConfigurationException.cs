using System;

namespace CardGame.Common
{
    /// <summary>
    /// The Invalid Configuration Exception.
    /// </summary>
    
    [Serializable]
    internal class InvalidConfigurationException : Exception
    {
        /// <summary>
        /// The Invalid Configuration Exception.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public InvalidConfigurationException(string message) : base(message)
        {

        }

    }
}
